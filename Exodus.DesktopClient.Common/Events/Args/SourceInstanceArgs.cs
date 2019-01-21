using Exodus.DesktopClient.Data.Types;
using System;


namespace Exodus.DesktopClient.Common.Events.Args
{
    public class SourceInstanceArgs : EventArgs
    {
        #region Properties

        public DcSourceInstance SourceInstance { get; set; }

        #endregion

        #region Constructors and Destructors

        public SourceInstanceArgs(DcSourceInstance sourceInstance)
        {
            this.SourceInstance = sourceInstance;
        }

        #endregion
    }
}
