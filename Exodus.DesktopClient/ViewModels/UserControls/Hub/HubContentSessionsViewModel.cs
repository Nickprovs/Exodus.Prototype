using Exodus.DesktopClient.Interfaces;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Exodus.DesktopClient.Popups;
using Exodus.DesktopClient.Engine.Interfaces;
using Exodus.DesktopClient.Data.Types;
using Prism.Mvvm;
using Prism.Events;
using Exodus.DesktopClient.PubSub.Events;
using Exodus.DesktopClient.PubSub.Args;

namespace Exodus.DesktopClient.ViewModels.UserControls.Hub
{
    public class HubContentSessionsViewModel : BindableBase
    {
        #region Fields

        /// <summary>
        /// The selected space session
        /// </summary>
        private DcSpaceSession _selectedSpaceSession;

        /// <summary>
        /// This signalr proxy. This is how we communicate with the backend. 
        /// </summary>
        private IDcSignalrProxy _signalrProxy;

        #endregion

        #region Properties

        /// <summary>
        /// The profile
        /// </summary>
        public IDcProfile Profile { get; private set; }

        /// <summary>
        /// The event aggregator
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// The Add Source Command
        /// </summary>
        public DelegateCommand AddSessionCommand { get; set; }

        /// <summary>
        /// The Delete Source Command
        /// </summary>
        public DelegateCommand DeleteSessionCommand { get; set; }

        /// <summary>
        /// The Selected Session Changed command
        /// </summary>
        public DelegateCommand SelectedSessionChangedCommand { get; set; }

        /// <summary>
        /// The add new space session popup dialog
        /// </summary>
        public InteractionRequest<INewSpaceSessionPopup> NewSpaceSessionPopupRequest { get; set; }

        /// <summary>
        /// The selected space session
        /// </summary>
        public DcSpaceSession SelectedSpaceSession
        {
            get { return _selectedSpaceSession; }
            set { SetProperty(ref _selectedSpaceSession, value); }
        }

        #endregion

        #region Constructors and Destructors

        public HubContentSessionsViewModel(IDcProfile profile, IEventAggregator eventAggregator, IDcSignalrProxy signalrProxy)
        {
            this.Profile = profile;
            this._signalrProxy = signalrProxy;
            this._eventAggregator = eventAggregator;

            this.NewSpaceSessionPopupRequest = new InteractionRequest<INewSpaceSessionPopup>();
            this.AddSessionCommand = new DelegateCommand(this.OnAddSession);
            this.DeleteSessionCommand = new DelegateCommand(this.OnDeleteSession);
            this.SelectedSessionChangedCommand = new DelegateCommand(this.OnSelectedSessionChanged);
        }

        #endregion

        #region Private Methods

        private async void OnAddSession()
        {
            //A potential new space session returned from the form
            DcSpaceSession newSpaceSession = null;

            //Raise the new space session popup form... and if we get something from it... set it equal to this local var
            NewSpaceSessionPopupRequest.Raise(new NewSpaceSessionPopup { Title = "Add New Space Session" }, r =>
            {
                newSpaceSession = r.NewSpaceSession;
            });

            //TODO: If we don't have null... send it to the server for add
            if (newSpaceSession != null && newSpaceSession?.DigitalWall != null)
            {
                int newDigitalWallId = await this._signalrProxy.AddDigitalWall(newSpaceSession.DigitalWall);         
                int newSpaceSessionId = await this._signalrProxy.AddSpaceSession(newSpaceSession, newDigitalWallId);
            }
        }

        private void OnDeleteSession()
        {
            if (this.SelectedSpaceSession != null)
                this._signalrProxy.RemoveSpaceSession(this.SelectedSpaceSession.SessionId);
        }

        private void OnSelectedSessionChanged()
        {
            this._eventAggregator.GetEvent<SelectedSessionChangedEvent>().Publish(new SelectedSessionChangedEventArgs(this.SelectedSpaceSession));
        }

        #endregion
    }
}
