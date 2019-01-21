using Exodus.DesktopClient.Data.Types;
using Exodus.DesktopClient.Interfaces;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace Exodus.DesktopClient.ViewModels.Popups
{
    public class NewSpaceSessionPopupViewModel : BindableBase, IInteractionRequestAware
    {
        #region Fields

        /// <summary>
        /// The innards of the new space session popup
        /// </summary>
        private INewSpaceSessionPopup _newSpaceSessionPopup;

        /// <summary>
        /// The to-be space session name
        /// </summary>
        private string _spaceSessionName;

        /// <summary>
        /// The to-be space session width as a string
        /// </summary>
        private string _widthString;

        /// <summary>
        /// The to-be space session height as a string
        /// </summary>
        private string _heightString;
        #endregion

        #region Properties

        /// <summary>
        /// The innards of the new source popup
        /// </summary>
        public INotification Notification
        {
            get { return this._newSpaceSessionPopup; }
            set { SetProperty(ref this._newSpaceSessionPopup, (INewSpaceSessionPopup)value); }
        }

        /// <summary>
        /// The add source command
        /// </summary>
        public DelegateCommand AddSpaceSessionCommand { get; private set; }

        /// <summary>
        /// The cancel command
        /// </summary>
        public DelegateCommand CancelCommand { get; private set; }

        /// <summary>
        /// The to-be source name
        /// </summary>
        public string SpaceSessionName
        {
            get { return this._spaceSessionName; }
            set { this.SetProperty(ref this._spaceSessionName, (string)value); }
        }

        /// <summary>
        /// The to-be source default width as a string
        /// </summary>
        public string WidthString
        {
            get { return this._widthString; }
            set { this.SetProperty(ref this._widthString, (string)value); }
        }

        /// <summary>
        /// The to-be source default height as a string
        /// </summary>
        public string HeightString
        {
            get { return this._heightString; }
            set { this.SetProperty(ref this._heightString, (string)value); }
        }

        /// <summary>
        /// The action that will close the dialog
        /// </summary>
        public Action FinishInteraction { get; set; }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// The constructor
        /// </summary>
        public NewSpaceSessionPopupViewModel()
        {
            this.AddSpaceSessionCommand = new DelegateCommand(OnAddSpaceSession);
            this.CancelCommand = new DelegateCommand(OnCancel);
        }

        #endregion

        #region Private Methods

        private void OnAddSpaceSession()
        {
            int width = 100;
            int height = 100;

            //TODO: IValueConverter
            int.TryParse(this.WidthString, out width);
            int.TryParse(this.HeightString, out height);

            //TODO: Error checking
            this._newSpaceSessionPopup.NewSpaceSession = new DcSpaceSession(this.SpaceSessionName, new DcDigitalWall(this.SpaceSessionName, width, height, new ObservableCollection<DcSourceInstance>()));
            this._newSpaceSessionPopup.Confirmed = true;
            this.ClearFields();
            this.FinishInteraction?.Invoke();
        }

        private void OnCancel()
        {
            this._newSpaceSessionPopup.NewSpaceSession = null;
            this._newSpaceSessionPopup.Confirmed = false;
            this.ClearFields();
            this.FinishInteraction?.Invoke();
        }

        private void ClearFields()
        {
            this.SpaceSessionName = "";
            this.WidthString = "";
            this.HeightString = "";
        }

        #endregion

    }
}
