using Exodus.DesktopClient.Data.Types;
using Exodus.DesktopClient.Interfaces;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Diagnostics;
using System.Windows.Media;

namespace Exodus.DesktopClient.ViewModels.Popups
{
    public class NewSourcePopupViewModel : BindableBase, IInteractionRequestAware
    {
        #region Fields

        /// <summary>
        /// The innards of the new source popup
        /// </summary>
        private INewSourcePopup _newSourcePopup;

        /// <summary>
        /// The to-be source name
        /// </summary>
        private string _sourceName;

        /// <summary>
        /// The to-be source default width as a string
        /// </summary>
        private string _defaultWidthString;

        /// <summary>
        /// The to-be source default height as a string
        /// </summary>
        private string _defaultHeightString;

        /// <summary>
        /// The to-be source color as a hex string
        /// </summary>
        private Color _color = Colors.Blue;

        #endregion

        #region Properties

        /// <summary>
        /// The innards of the new source popup
        /// </summary>
        public INotification Notification
        {
            get { return this._newSourcePopup; }
            set { SetProperty(ref this._newSourcePopup, (INewSourcePopup)value); }
        }

        /// <summary>
        /// The add source command
        /// </summary>
        public DelegateCommand AddSourceCommand { get; private set; }

        /// <summary>
        /// The cancel command
        /// </summary>
        public DelegateCommand CancelCommand { get; private set; }

        /// <summary>
        /// The to-be source name
        /// </summary>
        public string SourceName
        {
            get { return this._sourceName; }
            set { this.SetProperty(ref this._sourceName, (string)value); }
        }

        /// <summary>
        /// The to-be source default width as a string
        /// </summary>
        public string DefaultWidthString
        {
            get { return this._defaultWidthString; }
            set { this.SetProperty(ref this._defaultWidthString, (string)value); }
        }

        /// <summary>
        /// The to-be source default height as a string
        /// </summary>
        public string DefaultHeightString
        {
            get { return this._defaultHeightString; }
            set { this.SetProperty(ref this._defaultHeightString, (string)value); }
        }

        /// <summary>
        /// The to-be source color as a hex string
        /// </summary>
        public Color Color
        {
            get { return this._color; }
            set { this.SetProperty(ref this._color, (Color)value); }
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
        public NewSourcePopupViewModel()
        {
            this.AddSourceCommand = new DelegateCommand(OnAddSource);
            this.CancelCommand = new DelegateCommand(OnCancel);
        }

        #endregion

        #region Private Methods

        private void OnAddSource()
        {
            int defaultWidth = 100;
            int defaultHeight = 100;

            //TODO: IValueConverter
            int.TryParse(this.DefaultWidthString, out defaultWidth);
            int.TryParse(this.DefaultHeightString, out defaultHeight);


            //TODO: Error checking
            this._newSourcePopup.NewSource = new DcSource(this.SourceName, defaultWidth, defaultHeight, this.Color.ToString());
            this._newSourcePopup.Confirmed = true;
            this.ClearFields();
            this.FinishInteraction?.Invoke();
        }

        private void OnCancel()
        {
            this._newSourcePopup.NewSource = null;
            this._newSourcePopup.Confirmed = false;
            this.ClearFields();
            this.FinishInteraction?.Invoke();
        }

        private void ClearFields()
        {
            this.SourceName = "";
            this.DefaultWidthString = "";
            this.DefaultHeightString = "";
            this.Color = Colors.Blue;
        }

        #endregion
    }
}
