using Exodus.DesktopClient.Common.Events.Args;
using Exodus.DesktopClient.Common.Utilities;
using Exodus.DesktopClient.Data.Types;
using Exodus.DesktopClient.Engine.Interfaces;
using Exodus.DesktopClient.Interfaces;
using Exodus.DesktopClient.PubSub.Args;
using Exodus.DesktopClient.PubSub.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows;

namespace Exodus.DesktopClient.ViewModels.UserControls
{
    public class DisplayViewModel : BindableBase
    {
        #region Fields

        /// <summary>
        /// The profile.
        /// </summary>
        private readonly IDcProfile _profile;

        /// <summary>
        /// The event aggregator
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// The signalr proxy. This is how we talk to the backend.
        /// </summary>
        private readonly IDcSignalrProxy _signalrProxy;

        /// <summary>
        /// The space session
        /// </summary>
        private DcSpaceSession _spaceSession;

        #endregion

        #region Properties

        /// <summary>
        /// The space session currently powering this view
        /// </summary>
        public DcSpaceSession SpaceSession
        {
            get { return _spaceSession; }
            set { SetProperty(ref _spaceSession, value); }
        }

        /// <summary>
        /// The dropped on command
        /// </summary>
        public DelegateCommand<SourceDropArgs> DroppedOnCommand { get; set; }

        /// <summary>
        /// The source panned command
        /// </summary>
        public DelegateCommand<SourceInstanceArgs> SourcePannedCommand { get; set; }

        /// <summary>
        /// The source resized command
        /// </summary>
        public DelegateCommand<SourceInstanceArgs> SourceResizedCommand { get; set; }

        /// <summary>
        /// The source bring to front command
        /// </summary>
        public DelegateCommand<SourceInstanceArgs> SourceBringToFrontCommand { get; set; }

        /// <summary>
        /// The source send to back command
        /// </summary>
        public DelegateCommand<SourceInstanceArgs> SourceSendToBackCommand { get; set; }

        /// <summary>
        /// The source remove command
        /// </summary>
        public DelegateCommand<SourceInstanceArgs> SourceRemoveCommand { get; set; }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="profile"></param>
        public DisplayViewModel(IDcProfile profile, IEventAggregator eventAggregator, IDcSignalrProxy signalrProxy)
        {
            this._profile = profile;
            this._eventAggregator = eventAggregator;
            this._signalrProxy = signalrProxy;

            this.DroppedOnCommand = new DelegateCommand<SourceDropArgs>(this.OnDroppedOn);
            this.SourcePannedCommand = new DelegateCommand<SourceInstanceArgs>(this.OnSourcePanned);
            this.SourceResizedCommand = new DelegateCommand<SourceInstanceArgs>(this.OnSourceResized);
            this.SourceBringToFrontCommand = new DelegateCommand<SourceInstanceArgs>(this.OnSourceBringToFront);
            this.SourceSendToBackCommand = new DelegateCommand<SourceInstanceArgs>(this.OnSourceSendToBack);
            this.SourceRemoveCommand = new DelegateCommand<SourceInstanceArgs>(this.OnSourceRemove);

            this._eventAggregator.GetEvent<SelectedSessionChangedEvent>().Subscribe(this.OnSelectedSessionChanged);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Occurs when a new space is selected
        /// </summary>
        /// <param name="args"></param>
        private void OnSelectedSessionChanged(SelectedSessionChangedEventArgs args)
        {
            this.SpaceSession = args.NewlySelectedSpaceSession;
        }

        /// <summary>
        /// Occurs when a source is dropped on a wall. Tells the server a new source was added
        /// </summary>
        /// <param name="args"></param>
        private async void OnDroppedOn(SourceDropArgs args)
        {
            DcSourceInstance sourceInstance = new DcSourceInstance(args.Source, this.SpaceSession.DigitalWall.WallId, (int)args.DropPoint.X, (int)args.DropPoint.Y, args.Source.DefaultWidth, args.Source.DefaultHeight);
            await this._signalrProxy.AddSourceInstance(sourceInstance);
        }

        /// <summary>
        /// Tells the server about the source pan
        /// </summary>
        /// <param name="args"></param>
        private async void OnSourcePanned(SourceInstanceArgs args)
        {
            await this._signalrProxy.ModifySourceInstance(args.SourceInstance);
        }

        /// <summary>
        /// Tells the server about the source resize
        /// </summary>
        /// <param name="args"></param>
        private async void OnSourceResized(SourceInstanceArgs args)
        {
            await this._signalrProxy.ModifySourceInstance(args.SourceInstance);
        }

        /// <summary>
        /// Tells the server to bring the source to front
        /// </summary>
        /// <param name="args"></param>
        private void OnSourceBringToFront(SourceInstanceArgs args)
        {

        }


        /// <summary>
        /// Tells the server to send the source to back
        /// </summary>
        /// <param name="args"></param>
        private void OnSourceSendToBack(SourceInstanceArgs args)
        {

        }

        /// <summary>
        /// Tells the server to remove the source
        /// </summary>
        /// <param name="args"></param>
        private async void OnSourceRemove(SourceInstanceArgs args)
        {
            await this._signalrProxy.RemoveSourceInstance(args.SourceInstance.SourceInstanceId);
        }

        #endregion
    }
}
