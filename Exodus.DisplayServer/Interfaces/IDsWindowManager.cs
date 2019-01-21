using System.Collections.Generic;
using System.Windows;

namespace Exodus.DisplayServer.Interfaces
{
    public interface IDsWindowManager
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
