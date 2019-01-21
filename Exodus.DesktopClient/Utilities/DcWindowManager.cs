using Exodus.DesktopClient.Interfaces;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace Exodus.DesktopClient.Utilities
{
    /// <summary>
    /// Manages this apps windows
    /// </summary>
    public class DcWindowManager : IDcWindowManager
    {
        #region Fields

        /// <summary>
        ///     The _container
        /// </summary>
        private readonly IUnityContainer _container;

        /// <summary>
        ///     The _region manager
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        ///     The _windows
        /// </summary>
        private readonly IList<Window> _windows = new List<Window>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// The constructor for the window manager. Here we hook into this app's main window, subscribe to it's closed event, and call app.shutdown when it is closed.
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="container"></param>
        public DcWindowManager(IRegionManager regionManager, IUnityContainer container)
        {
            this._regionManager = regionManager;
            this._container = container;

            //When we create our window manager, grab the OHHH GEEE shell and put that ish in our windows collection as well
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Closed += this.MainWindow_Closed;
                this._windows.Add(Application.Current.MainWindow);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Creates the shell.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Window.</returns>
        [STAThread]
        public T CreateShell<T>() where T : Window
        {
            T shell = this._container.Resolve<T>();

            var rm = this._regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, rm);

            this._windows.Add(shell);

            shell.Closed += this.Shell_Closed;

            return shell;
        }

        /// <summary>
        ///     Gets the windows.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public IEnumerable<T> GetWindows<T>() where T : Window
        {
            return this._windows.OfType<T>();
        }

        /// <summary>
        /// Shutdowns this instance.
        /// </summary>
        public void Shutdown()
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region Private Methods
        /// <summary>
        ///     Closes the app when the main shell closes
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.Shutdown();
        }

        /// <summary>
        ///     Handles the Closed event of the Shell control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void Shell_Closed(object sender, EventArgs e)
        {
            this._windows.Remove(sender as Window);
        }

        #endregion
    }
}
