using Exodus.DesktopClient.Data.Types;
using Exodus.DesktopClient.Interfaces;
using Prism.Interactivity.InteractionRequest;

namespace Exodus.DesktopClient.Popups
{    
     /// <summary>
     /// The custom popup for entering new space sessions. This is where you should put data that will actually be returned in the popup.
     /// </summary>
    public class NewSpaceSessionPopup : Confirmation, INewSpaceSessionPopup
    {
        #region Properties

        /// <summary>
        /// The new space session... only gets set if the form data present in the viewmodel is valid.
        /// </summary>
        public DcSpaceSession NewSpaceSession { get; set; }

        #endregion


        #region Constructors and Destructors

        /// <summary>
        /// The constructor
        /// </summary>
        public NewSpaceSessionPopup()
        {
            this.NewSpaceSession = null;
        }

        #endregion
    }
}
