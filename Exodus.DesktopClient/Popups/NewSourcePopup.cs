using Exodus.DesktopClient.Data.Types;
using Exodus.DesktopClient.Interfaces;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.Popups
{
    /// <summary>
    /// The custom popup for entering new sources. This is where you should put data that will actually be returned in the popup.
    /// </summary>
    public class NewSourcePopup : Confirmation, INewSourcePopup
    {

        #region Properties

        /// <summary>
        /// The new source... only gets set if the form data present in the viewmodel is valid.
        /// </summary>
        public DcSource NewSource { get; set; }

        #endregion


        #region Constructors and Destructors

        /// <summary>
        /// The constructor
        /// </summary>
        public NewSourcePopup()
        {
            this.NewSource = null;
        }

        #endregion
    }
}
