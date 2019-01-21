using Exodus.DesktopClient.Common.Utilities;
using Exodus.DesktopClient.Controls.Utilities;
using Exodus.DesktopClient.Data.Types;
using Exodus.DesktopClient.Utilities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Exodus.DesktopClient.Views.UserControls.TreeView
{
    //TODO: Break this up to be MVVM
    /// <summary>
    /// Interaction logic for DcTreeViewItemContent.xaml
    /// </summary>
    public partial class DcTreeViewItemContent : UserControl
    {
        #region Fields

        /// <summary>
        /// The icon property
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon", typeof(Control), typeof(DcTreeViewItemContent), new PropertyMetadata(default(Control)));

        /// <summary>
        /// The label property
        /// </summary>
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label", typeof(string), typeof(DcTreeViewItemContent), new PropertyMetadata(default(string)));

        /// <summary>
        /// The payload property
        /// </summary>
        public static readonly DependencyProperty PayloadProperty = DependencyProperty.Register(
            "Payload", typeof(DcBase), typeof(DcTreeViewItemContent), new PropertyMetadata(default(DcBase)));

        private FrameworkElement payloadMovementTrigger;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public Control Icon
        {
            get { return (Control)this.GetValue(IconProperty); }
            set { this.SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        public string Label
        {
            get { return (string)this.GetValue(LabelProperty); }
            set { this.SetValue(LabelProperty, value); }
        }

        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        /// <value>The payload.</value>
        public DcBase Payload
        {
            get { return (DcBase)this.GetValue(PayloadProperty); }
            set { this.SetValue(PayloadProperty, value); }
        }


        #endregion

        #region Constructors and Destructors

        public DcTreeViewItemContent()
        {
            InitializeComponent();
            this.Loaded += DcTreeViewItemContent_Loaded;
        }

        #endregion

        #region Private Methods

        private void DcTreeViewItemContent_Loaded(object sender, RoutedEventArgs e)
        {
            //If we're in a a LVI, use that mouse move trigger
            this.payloadMovementTrigger = this.FindVisualAncestor<ListViewItem>();
            if (payloadMovementTrigger != null)
            {
                this.payloadMovementTrigger.PreviewMouseMove += this.OnPreviewMouseMove;
                return;
            }

            //Otherwise... use our self as the movement trigger.
            this.payloadMovementTrigger = this;
            this.payloadMovementTrigger.PreviewMouseMove += this.OnPreviewMouseMove;
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.Payload.Drop(this);
        }

        #endregion

    }
}
