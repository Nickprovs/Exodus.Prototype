using Exodus.DesktopClient.Interfaces;
using Exodus.DesktopClient.PubSub.Args;
using Exodus.DesktopClient.PubSub.Events;
using Exodus.DesktopClient.Views.UserControls.Hub;
using Microsoft.AspNet.SignalR.Client;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.ViewModels.Shells
{   
    //The brain of the app
    public class ControlShellViewModel
    {
        #region Fields
        /// <summary>
        /// The window manager.
        /// </summary>
        private readonly IDcWindowManager _windowManager;

        /// <summary>
        /// The event aggregator.
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// The region manager.
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// This signalr proxy. This is how we communicate with the backend. 
        /// </summary>
        private readonly IDcSignalrProxy _signalrProxy;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="windowManager"></param>
        public ControlShellViewModel(IDcWindowManager windowManager, IEventAggregator eventAggregator, IRegionManager regionManager, IDcSignalrProxy signalrProxy)
        {
            this._windowManager = windowManager;
            this._eventAggregator = eventAggregator;

            this._eventAggregator.GetEvent<ConnectedEvent>().Subscribe(this.OnConnected);

            this._regionManager = regionManager;
            this._signalrProxy = signalrProxy;
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Once we're connected, we can navigate to the hub container (menu / content area)
        /// May want to conditionally navigate based on old connection state...
        /// </summary>
        /// <param name="args"></param>
        private void OnConnected(ConnectionEventArgs args)
        {
            //This event is not fired via the ui, so to get on the ui thread...
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                this._regionManager.RequestNavigate("ControlShellRegion", nameof(HubContainer), callback);
            });
        }

        private void callback (NavigationResult args)
        {

        }
        #endregion

    }
}
