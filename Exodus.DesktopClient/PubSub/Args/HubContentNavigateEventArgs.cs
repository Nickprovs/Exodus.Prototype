using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.PubSub.Args
{
    public class HubContentNavigateEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// The navigation string.
        /// </summary>
        public string NavigationPath { get; set; }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="navigationString"></param>
        public HubContentNavigateEventArgs(string navigationPath)
        {
            this.NavigationPath = navigationPath;
        }

        #endregion
    }
}
