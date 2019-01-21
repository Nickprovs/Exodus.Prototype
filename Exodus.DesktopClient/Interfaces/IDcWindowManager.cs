using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Exodus.DesktopClient.Interfaces
{
    public interface IDcWindowManager
    {
        /// <summary>
        ///     Creates the shell.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Window.</returns>
        T CreateShell<T>() where T : Window;

        /// <summary>
        ///     Gets the windows.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> GetWindows<T>() where T : Window;

        /// <summary>
        /// Shuts the application down
        /// </summary>
        void Shutdown();
    }
}
