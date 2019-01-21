using Exodus.DesktopClient.Data.Types;
using Exodus.DesktopClient.Engine.Interfaces;
using Exodus.DesktopClient.Interfaces;
using Exodus.DesktopClient.Popups;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System.Linq;

namespace Exodus.DesktopClient.ViewModels.UserControls.Hub
{
    public class HubContentSourcesViewModel : BindableBase
    {

        #region Fields

        /// <summary>
        /// This signalr proxy. This is how we communicate with the backend. 
        /// </summary>
        private IDcSignalrProxy _signalrProxy;

        /// <summary>
        /// The selected source
        /// </summary>
        private DcSource _selectedSource;

        #endregion

        #region Properties

        /// <summary>
        /// The selected source
        /// </summary>
        public DcSource SelectedSource
        {
            get { return this._selectedSource; }
            set { SetProperty(ref this._selectedSource, value); }
        }

        /// <summary>
        /// The profile
        /// </summary>
        public IDcProfile Profile { get; private set; }

        /// <summary>
        /// The Add Source Command
        /// </summary>
        public DelegateCommand AddSourceCommand { get; set; }

        /// <summary>
        /// The Delete Source Command
        /// </summary>
        public DelegateCommand DeleteSourceCommand { get; set; }

        /// <summary>
        /// The add new source popup dialog
        /// </summary>
        public InteractionRequest<INewSourcePopup> NewSourcePopupRequest { get; set; }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="profile"></param>
        public HubContentSourcesViewModel(IDcProfile profile, IDcSignalrProxy signalrProxy)
        {
            this.Profile = profile;
            this._signalrProxy = signalrProxy;

            this.NewSourcePopupRequest = new InteractionRequest<INewSourcePopup>();
            this.AddSourceCommand = new DelegateCommand(this.OnAddSource);
            this.DeleteSourceCommand = new DelegateCommand(this.OnDeleteSource);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// The OnAddSource. Prompts a dialog to enter a new source.
        /// </summary>
        private async void OnAddSource()
        {
            //A potential new source returned from the form
            DcSource newSource = null;

            //Raise the new source popup form... and if we get something from it... set it equal to this local var
            NewSourcePopupRequest.Raise(new NewSourcePopup { Title = "Add New Source" }, r =>
            {
                newSource = r.NewSource;
            });

            if (newSource != null)
                await this._signalrProxy.AddSource(newSource);
        }

        /// <summary>
        /// The OnDeleteSource. Upon confirmation, removes the source.
        /// </summary>
        private void OnDeleteSource()
        {
            if (this.SelectedSource != null)
                this._signalrProxy.RemoveSource(this.SelectedSource.SourceId);
        }

        #endregion

    }
}
