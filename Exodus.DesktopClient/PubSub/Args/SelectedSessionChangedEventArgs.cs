using Exodus.DesktopClient.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.PubSub.Args
{
    public class SelectedSessionChangedEventArgs : EventArgs
    {
        #region Properties

        public DcSpaceSession NewlySelectedSpaceSession { get; set; }

        #endregion

        #region Constructors and Destructors

        public SelectedSessionChangedEventArgs(DcSpaceSession newlySelectedSpaceSession)
        {
            this.NewlySelectedSpaceSession = newlySelectedSpaceSession;
        }
        
        #endregion

    }
}
