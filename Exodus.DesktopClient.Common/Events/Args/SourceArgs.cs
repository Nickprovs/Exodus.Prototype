using Exodus.DesktopClient.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.Common.Events.Args
{
    public class SourceArgs : EventArgs
    {
        #region Properties

        public DcSource Source { get; set; }

        #endregion

        #region Constructors and Destructors

        public SourceArgs(DcSource source)
        {
            this.Source = source;
        }

        #endregion
    }
}
