#region Using

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#endregion

namespace Exodus.DesktopClient.Controls.Controls {
    /// <summary>
    /// Class ZoomAndPanControl.
    /// </summary>
    public partial class ZoomAndPanControl : ContentControl {
        #region Fields

        /// <summary>
        /// The content scale property
        /// </summary>
        public static readonly DependencyProperty ContentScaleProperty =
            DependencyProperty.Register("ContentScale", typeof(double), typeof(ZoomAndPanControl),
                new FrameworkPropertyMetadata(1.0, ContentScale_PropertyChanged, ContentScale_Coerce));

        /// <summary>
        /// The minimum content scale property
        /// </summary>
        public static readonly DependencyProperty MinContentScaleProperty =
            DependencyProperty.Register("MinContentScale", typeof(double), typeof(ZoomAndPanControl),
                new FrameworkPropertyMetadata(0.01, MinOrMaxContentScale_PropertyChanged));

        /// <summary>
        /// The maximum content scale property
        /// </summary>
        public static readonly DependencyProperty MaxContentScaleProperty =
            DependencyProperty.Register("MaxContentScale", typeof(double), typeof(ZoomAndPanControl),
                new FrameworkPropertyMetadata(10.0, MinOrMaxContentScale_PropertyChanged));

        /// <summary>
        /// The content offset x property
        /// </summary>
        public static readonly DependencyProperty ContentOffsetXProperty =
            DependencyProperty.Register("ContentOffsetX", typeof(double), typeof(ZoomAndPanControl),
                new FrameworkPropertyMetadata(0.0, ContentOffsetX_PropertyChanged, ContentOffsetX_Coerce));

        /// <summary>
        /// The content offset y property
        /// </summary>
        public static readonly DependencyProperty ContentOffsetYProperty =
            DependencyProperty.Register("ContentOffsetY", typeof(double), typeof(ZoomAndPanControl),
                new FrameworkPropertyMetadata(0.0, ContentOffsetY_PropertyChanged, ContentOffsetY_Coerce));

        /// <summary>
        /// The animation duration property
        /// </summary>
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register("AnimationDuration", typeof(double), typeof(ZoomAndPanControl),
                new FrameworkPropertyMetadata(0.4));

        /// <summary>
        /// The content viewport height property
        /// </summary>
        public static readonly DependencyProperty ContentViewportHeightProperty =
            DependencyProperty.Register("ContentViewportHeight", typeof(double), typeof(ZoomAndPanControl),
                new FrameworkPropertyMetadata(0.0));

        /// <summary>
        /// The content viewport width property
        /// </summary>
        public static readonly DependencyProperty ContentViewportWidthProperty =
            DependencyProperty.Register("ContentViewportWidth", typeof(double), typeof(ZoomAndPanControl),
                new FrameworkPropertyMetadata(0.0));

        /// <summary>
        /// The content zoom focus x property
        /// </summary>
        public static readonly DependencyProperty ContentZoomFocusXProperty =
            DependencyProperty.Register("ContentZoomFocusX", typeof(double), typeof(ZoomAndPanControl),
                new FrameworkPropertyMetadata(0.0));

        /// <summary>
        /// The content zoom focus y property
        /// </summary>
        public static readonly DependencyProperty ContentZoomFocusYProperty =
            DependencyProperty.Register("ContentZoomFocusY", typeof(double), typeof(ZoomAndPanControl),
                new FrameworkPropertyMetadata(0.0));

        /// <summary>
        /// The is mouse wheel scrolling enabled property
        /// </summary>
        public static readonly DependencyProperty IsMouseWheelScrollingEnabledProperty =
            DependencyProperty.Register("IsMouseWheelScrollingEnabled", typeof(bool), typeof(ZoomAndPanControl),
                new FrameworkPropertyMetadata(false));

        /// <summary>
        /// The viewport zoom focus x property
        /// </summary>
        public static readonly DependencyProperty ViewportZoomFocusXProperty =
            DependencyProperty.Register("ViewportZoomFocusX", typeof(double), typeof(ZoomAndPanControl),
                new FrameworkPropertyMetadata(0.0));

        /// <summary>
        /// The viewport zoom focus y property
        /// </summary>
        public static readonly DependencyProperty ViewportZoomFocusYProperty =
            DependencyProperty.Register("ViewportZoomFocusY", typeof(double), typeof(ZoomAndPanControl),
                new FrameworkPropertyMetadata(0.0));

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="ZoomAndPanControl"/> class.
        /// </summary>
        static ZoomAndPanControl() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(typeof(ZoomAndPanControl)));
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [content offset x changed].
        /// </summary>
        public event EventHandler ContentOffsetXChanged;

        /// <summary>
        /// Occurs when [content offset y changed].
        /// </summary>
        public event EventHandler ContentOffsetYChanged;

        /// <summary>
        /// Occurs when [content scale changed].
        /// </summary>
        public event EventHandler ContentScaleChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the content offset x.
        /// </summary>
        /// <value>The content offset x.</value>
        public double ContentOffsetX {
            get { return (double) this.GetValue(ContentOffsetXProperty); }
            set { this.SetValue(ContentOffsetXProperty, value); }
        }

        /// <summary>
        /// Gets or sets the content offset y.
        /// </summary>
        /// <value>The content offset y.</value>
        public double ContentOffsetY {
            get { return (double) this.GetValue(ContentOffsetYProperty); }
            set { this.SetValue(ContentOffsetYProperty, value); }
        }

        /// <summary>
        /// Gets or sets the content scale.
        /// </summary>
        /// <value>The content scale.</value>
        public double ContentScale {
            get { return (double) this.GetValue(ContentScaleProperty); }
            set { this.SetValue(ContentScaleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the height of the content viewport.
        /// </summary>
        /// <value>The height of the content viewport.</value>
        public double ContentViewportHeight {
            get { return (double) this.GetValue(ContentViewportHeightProperty); }
            set { this.SetValue(ContentViewportHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the width of the content viewport.
        /// </summary>
        /// <value>The width of the content viewport.</value>
        public double ContentViewportWidth {
            get { return (double) this.GetValue(ContentViewportWidthProperty); }
            set { this.SetValue(ContentViewportWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the content zoom focus x.
        /// </summary>
        /// <value>The content zoom focus x.</value>
        public double ContentZoomFocusX {
            get { return (double) this.GetValue(ContentZoomFocusXProperty); }
            set { this.SetValue(ContentZoomFocusXProperty, value); }
        }

        /// <summary>
        /// Gets or sets the content zoom focus y.
        /// </summary>
        /// <value>The content zoom focus y.</value>
        public double ContentZoomFocusY {
            get { return (double) this.GetValue(ContentZoomFocusYProperty); }
            set { this.SetValue(ContentZoomFocusYProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is mouse wheel scrolling enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is mouse wheel scrolling enabled; otherwise, <c>false</c>.</value>
        public bool IsMouseWheelScrollingEnabled {
            get { return (bool) this.GetValue(IsMouseWheelScrollingEnabledProperty); }
            set { this.SetValue(IsMouseWheelScrollingEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets the maximum content scale.
        /// </summary>
        /// <value>The maximum content scale.</value>
        public double MaxContentScale {
            get { return (double) this.GetValue(MaxContentScaleProperty); }
            set { this.SetValue(MaxContentScaleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the minimum content scale.
        /// </summary>
        /// <value>The minimum content scale.</value>
        public double MinContentScale {
            get { return (double) this.GetValue(MinContentScaleProperty); }
            set { this.SetValue(MinContentScaleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the viewport zoom focus x.
        /// </summary>
        /// <value>The viewport zoom focus x.</value>
        public double ViewportZoomFocusX {
            get { return (double) this.GetValue(ViewportZoomFocusXProperty); }
            set { this.SetValue(ViewportZoomFocusXProperty, value); }
        }

        /// <summary>
        /// Gets or sets the viewport zoom focus y.
        /// </summary>
        /// <value>The viewport zoom focus y.</value>
        public double ViewportZoomFocusY {
            get { return (double) this.GetValue(ViewportZoomFocusYProperty); }
            set { this.SetValue(ViewportZoomFocusYProperty, value); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
        /// </summary>
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            this.content = this.Template.FindName("PART_Content", this) as FrameworkElement;
            if (this.content != null) {
                this.contentScaleTransform = new ScaleTransform(this.ContentScale, this.ContentScale);

                this.contentOffsetTransform = new TranslateTransform();
                this.UpdateTranslationX();
                this.UpdateTranslationY();

                TransformGroup transformGroup = new TransformGroup();
                transformGroup.Children.Add(this.contentOffsetTransform);
                transformGroup.Children.Add(this.contentScaleTransform);
                this.content.RenderTransform = transformGroup;
            }
        }

        /// <summary>
        /// Snaps the content offset to.
        /// </summary>
        /// <param name="contentOffset">The content offset.</param>
        public void SnapContentOffsetTo(Point contentOffset) {
            this.ContentOffsetX = contentOffset.X;
            this.ContentOffsetY = contentOffset.Y;
        }

        /// <summary>
        /// Zooms the about point.
        /// </summary>
        /// <param name="newContentScale">The new content scale.</param>
        /// <param name="contentZoomFocus">The content zoom focus.</param>
        public void ZoomAboutPoint(double newContentScale, Point contentZoomFocus) {
            newContentScale = Math.Min(Math.Max(newContentScale, this.MinContentScale), this.MaxContentScale);

            double screenSpaceZoomOffsetX = (contentZoomFocus.X - this.ContentOffsetX) * this.ContentScale;
            double screenSpaceZoomOffsetY = (contentZoomFocus.Y - this.ContentOffsetY) * this.ContentScale;
            double contentSpaceZoomOffsetX = screenSpaceZoomOffsetX / newContentScale;
            double contentSpaceZoomOffsetY = screenSpaceZoomOffsetY / newContentScale;
            double newContentOffsetX = contentZoomFocus.X - contentSpaceZoomOffsetX;
            double newContentOffsetY = contentZoomFocus.Y - contentSpaceZoomOffsetY;

            this.ContentScale = newContentScale;
            this.ContentOffsetX = newContentOffsetX < 0 ? 0 : newContentOffsetX;
            this.ContentOffsetY = newContentOffsetY < 0 ? 0 : newContentOffsetY;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called to arrange and size the content of a <see cref="T:System.Windows.Controls.Control" /> object.
        /// </summary>
        /// <param name="arrangeBounds">The computed size that is used to arrange the content.</param>
        /// <returns>The size of the control.</returns>
        protected override Size ArrangeOverride(Size arrangeBounds) {
            Size size = base.ArrangeOverride(this.DesiredSize);

            if (this.content.DesiredSize != this.unScaledExtent) {
                this.unScaledExtent = this.content.DesiredSize;

                if (this.ScrollOwner != null) this.ScrollOwner.InvalidateScrollInfo();
            }

            this.UpdateViewportSize(arrangeBounds);

            return size;
        }

        /// <summary>
        /// Called to remeasure a control.
        /// </summary>
        /// <param name="constraint">The maximum size that the method can return.</param>
        /// <returns>The size of the control, up to the maximum specified by <paramref name="constraint" />.</returns>
        protected override Size MeasureOverride(Size constraint) {
            Size infiniteSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            Size childSize = base.MeasureOverride(infiniteSize);

            if (childSize != this.unScaledExtent) {
                this.unScaledExtent = childSize;

                if (this.ScrollOwner != null) this.ScrollOwner.InvalidateScrollInfo();
            }

            this.UpdateViewportSize(constraint);

            double width = constraint.Width;
            double height = constraint.Height;

            if (double.IsInfinity(width)) width = childSize.Width;

            if (double.IsInfinity(height)) height = childSize.Height;

            this.UpdateTranslationX();
            this.UpdateTranslationY();

            return new Size(width, height);
        }

        /// <summary>
        /// Contents the offset x_ coerce.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="baseValue">The base value.</param>
        /// <returns>System.Object.</returns>
        private static object ContentOffsetX_Coerce(DependencyObject d, object baseValue) {
            ZoomAndPanControl c = (ZoomAndPanControl) d;
            double value = (double) baseValue;
            double minOffsetX = 0.0;
            double maxOffsetX = Math.Max(0.0, c.unScaledExtent.Width - c.constrainedContentViewportWidth);
            value = Math.Min(Math.Max(value, minOffsetX), maxOffsetX);
            return value;
        }

        /// <summary>
        /// Contents the offset x_ property changed.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void ContentOffsetX_PropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            ZoomAndPanControl c = (ZoomAndPanControl) o;

            c.UpdateTranslationX();

            if (!c.disableContentFocusSync) c.UpdateContentZoomFocusX();

            if (c.ContentOffsetXChanged != null) c.ContentOffsetXChanged(c, EventArgs.Empty);

            if (!c.disableScrollOffsetSync && c.ScrollOwner != null) c.ScrollOwner.InvalidateScrollInfo();
        }

        /// <summary>
        /// Contents the offset y_ coerce.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="baseValue">The base value.</param>
        /// <returns>System.Object.</returns>
        private static object ContentOffsetY_Coerce(DependencyObject d, object baseValue) {
            ZoomAndPanControl c = (ZoomAndPanControl) d;
            double value = (double) baseValue;
            double minOffsetY = 0.0;
            double maxOffsetY = Math.Max(0.0, c.unScaledExtent.Height - c.constrainedContentViewportHeight);
            value = Math.Min(Math.Max(value, minOffsetY), maxOffsetY);
            return value;
        }

        /// <summary>
        /// Contents the offset y_ property changed.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void ContentOffsetY_PropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            ZoomAndPanControl c = (ZoomAndPanControl) o;

            c.UpdateTranslationY();

            if (!c.disableContentFocusSync) c.UpdateContentZoomFocusY();

            if (c.ContentOffsetYChanged != null) c.ContentOffsetYChanged(c, EventArgs.Empty);

            if (!c.disableScrollOffsetSync && c.ScrollOwner != null) c.ScrollOwner.InvalidateScrollInfo();
        }

        /// <summary>
        /// Contents the scale_ coerce.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="baseValue">The base value.</param>
        /// <returns>System.Object.</returns>
        private static object ContentScale_Coerce(DependencyObject d, object baseValue) {
            ZoomAndPanControl c = (ZoomAndPanControl) d;
            double value = (double) baseValue;
            value = Math.Min(Math.Max(value, c.MinContentScale), c.MaxContentScale);
            return value;
        }

        /// <summary>
        /// Contents the scale_ property changed.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void ContentScale_PropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            ZoomAndPanControl c = (ZoomAndPanControl) o;

            if (c.contentScaleTransform != null) {
                c.contentScaleTransform.ScaleX = c.ContentScale;
                c.contentScaleTransform.ScaleY = c.ContentScale;
            }

            c.UpdateContentViewportSize();

            if (c.enableContentOffsetUpdateFromScale)
                try {
                    c.disableContentFocusSync = true;

                    double viewportOffsetX = c.ViewportZoomFocusX - c.ViewportWidth / 2;
                    double viewportOffsetY = c.ViewportZoomFocusY - c.ViewportHeight / 2;
                    double contentOffsetX = viewportOffsetX / c.ContentScale;
                    double contentOffsetY = viewportOffsetY / c.ContentScale;
                    c.ContentOffsetX = c.ContentZoomFocusX - c.ContentViewportWidth / 2 - contentOffsetX;
                    c.ContentOffsetY = c.ContentZoomFocusY - c.ContentViewportHeight / 2 - contentOffsetY;
                }
                finally {
                    c.disableContentFocusSync = false;
                }

            if (c.ContentScaleChanged != null) c.ContentScaleChanged(c, EventArgs.Empty);

            if (c.ScrollOwner != null) c.ScrollOwner.InvalidateScrollInfo();
        }

        /// <summary>
        /// Minimums the or maximum content scale_ property changed.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void MinOrMaxContentScale_PropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            ZoomAndPanControl c = (ZoomAndPanControl) o;
            c.ContentScale = Math.Min(Math.Max(c.ContentScale, c.MinContentScale), c.MaxContentScale);
        }

        /// <summary>
        /// Resets the viewport zoom focus.
        /// </summary>
        private void ResetViewportZoomFocus() {
            this.ViewportZoomFocusX = this.ViewportWidth / 2;
            this.ViewportZoomFocusY = this.ViewportHeight / 2;
        }

        /// <summary>
        /// Updates the size of the content viewport.
        /// </summary>
        private void UpdateContentViewportSize() {
            this.ContentViewportWidth = this.ViewportWidth / this.ContentScale;
            this.ContentViewportHeight = this.ViewportHeight / this.ContentScale;

            this.constrainedContentViewportWidth = Math.Min(this.ContentViewportWidth, this.unScaledExtent.Width);
            this.constrainedContentViewportHeight = Math.Min(this.ContentViewportHeight, this.unScaledExtent.Height);

            this.UpdateTranslationX();
            this.UpdateTranslationY();
        }

        /// <summary>
        /// Updates the content zoom focus x.
        /// </summary>
        private void UpdateContentZoomFocusX() {
            this.ContentZoomFocusX = this.ContentOffsetX + this.constrainedContentViewportWidth / 2;
        }

        /// <summary>
        /// Updates the content zoom focus y.
        /// </summary>
        private void UpdateContentZoomFocusY() {
            this.ContentZoomFocusY = this.ContentOffsetY + this.constrainedContentViewportHeight / 2;
        }

        /// <summary>
        /// Updates the translation x.
        /// </summary>
        private void UpdateTranslationX() {
            if (this.contentOffsetTransform != null) {
                double scaledContentWidth = this.unScaledExtent.Width * this.ContentScale;
                if (scaledContentWidth < this.ViewportWidth)
                    this.contentOffsetTransform.X = (this.ContentViewportWidth - this.unScaledExtent.Width) / 2;
                else
                    this.contentOffsetTransform.X = -this.ContentOffsetX;
            }
        }

        /// <summary>
        /// Updates the translation y.
        /// </summary>
        private void UpdateTranslationY() {
            if (this.contentOffsetTransform != null) {
                double scaledContentHeight = this.unScaledExtent.Height * this.ContentScale;
                if (scaledContentHeight < this.ViewportHeight)
                    this.contentOffsetTransform.Y = (this.ContentViewportHeight - this.unScaledExtent.Height) / 2;
                else
                    this.contentOffsetTransform.Y = -this.ContentOffsetY;
            }
        }

        /// <summary>
        /// Updates the size of the viewport.
        /// </summary>
        /// <param name="newSize">The new size.</param>
        private void UpdateViewportSize(Size newSize) {
            if (this.viewport == newSize) return;

            this.viewport = newSize;

            this.UpdateContentViewportSize();

            this.UpdateContentZoomFocusX();
            this.UpdateContentZoomFocusY();

            this.ResetViewportZoomFocus();

            this.ContentOffsetX = this.ContentOffsetX;
            this.ContentOffsetY = this.ContentOffsetY;

            if (this.ScrollOwner != null) this.ScrollOwner.InvalidateScrollInfo();
        }
        
        #endregion
    }
}