using Exodus.DesktopClient.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Exodus.DesktopClient.Common.Events.Args
{
    public class SourceDropArgs : SourceArgs
    {
        #region Properties

        public Point DropPoint { get; set; }

        #endregion

        #region Constructors and Destructors

        public SourceDropArgs(Point dropPoint, DcSource source) : base (source)
        {
            this.DropPoint = dropPoint;
        }

        #endregion

    }
}
