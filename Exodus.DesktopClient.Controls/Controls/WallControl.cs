using Exodus.DesktopClient.Controls.Utilities;
using Exodus.DesktopClient.Common.Utilities;
using Exodus.DesktopClient.Data.Types;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using Exodus.DesktopClient.Common.Events.Args;

namespace Exodus.DesktopClient.Controls.Controls
{
    public class WallControl : ContentControl
    {

        #region Fields
        /// <summary>
        /// The Wall. Can represent a digital or physical wall. Contains useful properties used to power this control such as the width/height of the space, the source instnaces on it, etc. The width/height will be simulated in a scaling control.
        /// </summary>
        public static readonly DependencyProperty WallProperty = DependencyProperty.Register(
            "Wall", typeof(DcWall), typeof(WallControl), new PropertyMetadata(null, OnWallChanged));

        /// <summary>
        /// The scale property
        /// </summary>
        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            "Scale", typeof(double), typeof(WallControl), new PropertyMetadata(1.0d, null, OnScaleCoerce));

        /// <summary>
        /// The minimum scale property
        /// </summary>
        public static readonly DependencyProperty MinScaleProperty = DependencyProperty.Register(
            "MinScale", typeof(double), typeof(WallControl), new PropertyMetadata(0.05d, OnMinScaleChanged));

        /// <summary>
        /// The minimum scale property
        /// </summary>
        public static readonly DependencyProperty MaxScaleProperty = DependencyProperty.Register(
            "MaxScale", typeof(double), typeof(WallControl), new PropertyMetadata(1.0d));

        /// <summary>
        /// The increment property. The increment with which the scale can change.
        /// </summary>
        public static readonly DependencyProperty IncrementProperty = DependencyProperty.Register(
            "Increment", typeof(double), typeof(WallControl), new PropertyMetadata(0.05d));

        /// <summary>
        /// The offset x propertyMe
        /// </summary>
        public static readonly DependencyProperty OffsetXProperty = DependencyProperty.Register(
            "OffsetX", typeof(double), typeof(WallControl), new PropertyMetadata(default(double)));

        /// <summary>
        /// The offset y property
        /// </summary>
        public static readonly DependencyProperty OffsetYProperty = DependencyProperty.Register(
            "OffsetY", typeof(double), typeof(WallControl), new PropertyMetadata(default(double)));

        /// <summary>
        /// The view port height property
        /// </summary>
        public static readonly DependencyProperty ViewPortHeightProperty = DependencyProperty.Register(
            "ViewPortHeight", typeof(double), typeof(WallControl), new PropertyMetadata(default(double)));

        /// <summary>
        /// The view port width property
        /// </summary>
        public static readonly DependencyProperty ViewPortWidthProperty = DependencyProperty.Register(
            "ViewPortWidth", typeof(double), typeof(WallControl), new PropertyMetadata(default(double)));

        /// <summary>
        /// The resize source border thickness property
        /// </summary>
        public static readonly DependencyProperty ResizeSourceBorderThicknessProperty = DependencyProperty.Register(
            "ResizeSourceBorderThickness", typeof(double), typeof(WallControl), new PropertyMetadata(15d));

        /// <summary>
        /// The wall dropped on command property
        /// </summary>
        public static readonly DependencyProperty WallDroppedOnCommandProperty =
        DependencyProperty.Register("WallDroppedOnCommand", typeof(ICommand), typeof(WallControl), new PropertyMetadata(null));

        /// <summary>
        /// The source panned command property
        /// </summary>
        public static readonly DependencyProperty SourcePannedCommandProperty =
        DependencyProperty.Register("SourcePannedCommand", typeof(ICommand), typeof(WallControl), new PropertyMetadata(null));

        /// <summary>
        /// The source resized command property
        /// </summary>
        public static readonly DependencyProperty SourceResizedCommandProperty =
        DependencyProperty.Register("SourceResizedCommand", typeof(ICommand), typeof(WallControl), new PropertyMetadata(null));

        /// <summary>
        /// The source remove command property
        /// </summary>
        public static readonly DependencyProperty SourceRemoveCommandProperty =
        DependencyProperty.Register("SourceRemoveCommand", typeof(ICommand), typeof(WallControl), new PropertyMetadata(null));

        /// <summary>
        /// The source bring to front command property
        /// </summary>
        public static readonly DependencyProperty SourceBringToFrontCommandProperty =
        DependencyProperty.Register("SourceBringToFrontCommand", typeof(ICommand), typeof(WallControl), new PropertyMetadata(null));

        /// <summary>
        /// The source send to back command property
        /// </summary>
        public static readonly DependencyProperty SourceSendToBackCommandProperty =
        DependencyProperty.Register("SourceSendToBackCommand", typeof(ICommand), typeof(WallControl), new PropertyMetadata(null));

        /// <summary>
        /// The Wall Scroll Viewer
        /// </summary>
        private ScrollViewer _wallScrollViewer;

        /// <summary>
        /// The Wall Zoom and Pan Control
        /// </summary>
        private ZoomAndPanControl _wallZoomAndPanControl;

        /// <summary>
        /// The wall items control container grid.
        /// </summary>
        private Grid _wallItemsControlContainerGrid;

        #endregion

        #region Properties
        /// <summary>
        /// The Wall. Can represent a digital or physical wall. Contains useful properties used to power this control such as the width/height of the space, the source instnaces on it, etc. The width/height will be simulated in a scaling control.
        /// </summary>
        public DcWall Wall
        {
            get { return (DcWall)this.GetValue(WallProperty); }
            set { this.SetValue(WallProperty, value); }
        }

        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>The scale.</value>
        public double Scale
        {
            get { return (double)this.GetValue(ScaleProperty); }
            set { this.SetValue(ScaleProperty, value); }
        }

        /// <summary>
        /// The minimum scale.
        /// </summary>
        public double MinScale
        {
            get { return (double)this.GetValue(MinScaleProperty); }
            set { this.SetValue(MinScaleProperty, value); }
        }

        /// <summary>
        /// The maximum scale.
        /// </summary>
        public double MaxScale
        {
            get { return (double)this.GetValue(MaxScaleProperty); }
            set { this.SetValue(ScaleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the increment. The increment with which the scale can change.
        /// </summary>
        /// <value>The increment.</value>
        public double Increment
        {
            get { return (double)this.GetValue(IncrementProperty); }
            set { this.SetValue(IncrementProperty, value); }
        }

        /// <summary>
        /// Gets or sets the offset x.
        /// </summary>
        /// <value>The offset x.</value>
        public double OffsetX
        {
            get { return (double)this.GetValue(OffsetXProperty); }
            set { this.SetValue(OffsetXProperty, value); }
        }

        /// <summary>
        /// Gets or sets the offset y.
        /// </summary>
        /// <value>The offset y.</value>
        public double OffsetY
        {
            get { return (double)this.GetValue(OffsetYProperty); }
            set { this.SetValue(OffsetYProperty, value); }
        }

        /// <summary>
        /// Gets or sets the height of the view port.
        /// </summary>
        /// <value>The height of the view port.</value>
        public double ViewPortHeight
        {
            get { return (double)this.GetValue(ViewPortHeightProperty); }
            set { this.SetValue(ViewPortHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the width of the view port.
        /// </summary>
        /// <value>The width of the view port.</value>
        public double ViewPortWidth
        {
            get { return (double)this.GetValue(ViewPortWidthProperty); }
            set { this.SetValue(ViewPortWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the WallDroppedOnCommand
        /// </summary>
        [TypeConverter(typeof(CommandConverter))]
        public ICommand WallDroppedOnCommand
        {
            get { return (ICommand)this.GetValue(WallDroppedOnCommandProperty); }
            set { this.SetValue(WallDroppedOnCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the SourcePannedCommand
        /// </summary>
        [TypeConverter(typeof(CommandConverter))]
        public ICommand SourcePannedCommand
        {
            get { return (ICommand)this.GetValue(SourcePannedCommandProperty); }
            set { this.SetValue(SourcePannedCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the SourceResizedCommand
        /// </summary>
        [TypeConverter(typeof(CommandConverter))]
        public ICommand SourceResizedCommand
        {
            get { return (ICommand)this.GetValue(SourceResizedCommandProperty); }
            set { this.SetValue(SourceResizedCommandProperty, value); }
        }


        /// <summary>
        /// Gets or sets the SourceRemoveCommand
        /// </summary>
        [TypeConverter(typeof(CommandConverter))]
        public ICommand SourceRemoveCommand
        {
            get { return (ICommand)this.GetValue(SourceRemoveCommandProperty); }
            set { this.SetValue(SourceRemoveCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the SourceBringToFrontCommand
        /// </summary>
        [TypeConverter(typeof(CommandConverter))]
        public ICommand SourceBringToFrontCommand
        {
            get { return (ICommand)this.GetValue(SourceBringToFrontCommandProperty); }
            set { this.SetValue(SourceBringToFrontCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the SourceSendToBackCommand
        /// </summary>
        [TypeConverter(typeof(CommandConverter))]
        public ICommand SourceSendToBackCommand
        {
            get { return (ICommand)this.GetValue(SourceSendToBackCommandProperty); }
            set { this.SetValue(SourceSendToBackCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the resize source border thickness.
        /// </summary>
        /// <value>The resize source border thickness.</value>
        public double ResizeSourceBorderThickness
        {
            get { return (double)this.GetValue(ResizeSourceBorderThicknessProperty); }
            set { this.SetValue(ResizeSourceBorderThicknessProperty, value); }
        }
        #endregion

        #region Constructors and Destructors

        public WallControl()
        {
            this.Loaded += this.Wall_Loaded;
            this.DragEnter += this.Wall_Dragged;
            this.DragOver += this.Wall_Dragged;
            this.Drop += this.Wall_Dropped;
        }

        static WallControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WallControl), new FrameworkPropertyMetadata(typeof(WallControl)));
        }


        #endregion

        public override void OnApplyTemplate()
        {
            //Hook into the wall's scroll viewer
            this._wallScrollViewer = this.Template.FindName("WallScrollViewer", this) as ScrollViewer;
            if (this._wallScrollViewer != null)
            {
                this._wallScrollViewer.SizeChanged += _wallScrollViewer_SizeChanged;
                this._wallScrollViewer.PreviewMouseWheel += _wallScrollViewer_PreviewMouseWheel;
            }

            //Hook into the wall's zoom and pan
            this._wallZoomAndPanControl = this.Template.FindName("WallZoomAndPanControl", this) as ZoomAndPanControl;

            //Hook into the wall's item's control container grid
            this._wallItemsControlContainerGrid = this.Template.FindName("WallItemsControlContainerGrid", this) as Grid;

            base.OnApplyTemplate();
        }

        private void Wall_Dragged(object sender, DragEventArgs e)
        {
            DcSource source = e.GetData<DcSource>();
            if (source == null)
                e.Effects = DragDropEffects.None;

            e.Handled = true;
        }

        private void Wall_Dropped(object sender, DragEventArgs e)
        {
            //Get the thing (canvas, hopefully) we dropped on...
            var dropTarg = e.OriginalSource;
            if (e.OriginalSource.GetType() != typeof(Canvas))
                dropTarg = (dropTarg as UIElement).FindVisualAncestor<Canvas>();

            //If we didn't drop on the canvas, and we got here, I don't know what happened
            if (dropTarg == null) return;

            //Was it a source we dropped?
            DcSource source = e.GetData<DcSource>();

            //If not... there's nothing we can do here
            if (source == null)
                return;
   
            //Get the point, relative to our canvas, at which we dropped our source
            Point pos = e.GetPosition((UIElement)dropTarg);

            //If there is a view model hooked into this command, let's let them know someone tried to drop a source on us
            if (this.WallDroppedOnCommand != null)
                this.WallDroppedOnCommand.Execute(new SourceDropArgs(pos, source));


        }


        private void Wall_Loaded(object sender, RoutedEventArgs e)
        {
            //Let's start all the way zoomed out.
            this.Scale = this.MinScale;
        }

        private void _wallScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            if (e.Delta > 0)
            {
                Point curContentMousePoint = e.GetPosition(this._wallItemsControlContainerGrid);
                this.ZoomIn(curContentMousePoint);
            }
            else if (e.Delta < 0)
            {
                Point curContentMousePoint = e.GetPosition(this._wallItemsControlContainerGrid);
                this.ZoomOut(curContentMousePoint);
            }

            this.UpdateViewPort();
        }

        /// <summary>
        /// Zooms the in.
        /// </summary>
        /// <param name="contentZoomCenter">The content zoom center.</param>
        private void ZoomIn(Point contentZoomCenter)
        {
            if (this._wallZoomAndPanControl == null) return;
            double newScale = this._wallZoomAndPanControl.ContentScale + this.Increment;
            if (newScale > 1) newScale = 1;
            this._wallZoomAndPanControl.ZoomAboutPoint(newScale, contentZoomCenter);
        }

        /// <summary>
        /// Zooms the out.
        /// </summary>
        /// <param name="contentZoomCenter">The content zoom center.</param>
        private void ZoomOut(Point contentZoomCenter)
        {
            if (this._wallZoomAndPanControl == null) return;
            double newScale = this._wallZoomAndPanControl.ContentScale - this.Increment;
            if (newScale < 0) newScale = 0;
            this._wallZoomAndPanControl.ZoomAboutPoint(newScale, contentZoomCenter);
        }

        /// <summary>
        /// When the size of the control is changed... we must rejigger our self to recalcuate some values and scale back out.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _wallScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Rejigger();
        }

        /// <summary>
        /// Rejiggers the view of the wall control.
        /// </summary>
        private void Rejigger()
        {
            //Note: The width/height of the space session representes the SIMULATED width/height
            double minScaleWidth = (this._wallScrollViewer.DesiredSize.Width - 6) / this.Wall?.Width ?? 1920;
            double minScaleHeight = (this._wallScrollViewer.DesiredSize.Height - 6) / this.Wall?.Height ?? 1080;

            this.MinScale = Math.Min(minScaleWidth, minScaleHeight);

            this.UpdateViewPort();

            //As we resize the window, we want to zoom all the way out... this is expected behavior for things with zoom when scaling the window
            //Zoom back out to the potentially new minscale
            this.Scale = this.MinScale;
        }

        /// <summary>
        /// Updates the view port.
        /// </summary>
        private void UpdateViewPort()
        {
            double renderSizeWidth = this._wallZoomAndPanControl.ContentViewportWidth;
            double renderSizeHeight = this._wallZoomAndPanControl.ContentViewportHeight;

            //Note: The width/height of the space session representes the SIMULATED width/height

            if (renderSizeWidth > (this.Wall?.Width ?? 1920))
                renderSizeWidth = this.Wall?.Width ?? 1920;

            if (renderSizeHeight > (this.Wall?.Height ?? 1080))
                renderSizeHeight = (this.Wall?.Height ?? 1080);

            this.ViewPortWidth = renderSizeWidth;
            this.ViewPortHeight = renderSizeHeight;

            //Anytime we updated the viewport, we need to make sure the ability to resize sources is retained.
            this.RecalculateSourceResizeBorderThickness();
        }

        /// <summary>
        /// Called when [scale coerce].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="basevalue">The basevalue.</param>
        /// <returns>System.Object.</returns>
        private static object OnScaleCoerce(DependencyObject d, object basevalue)
        {
            WallControl view = d as WallControl;
            double value = basevalue as double? ?? double.NaN;
            if (view == null || double.IsNaN(value)) return basevalue;
            if (value < view.MinScale)
                return view.MinScale;
            if (value > view.MaxScale)
                return view.MaxScale;
            return value;
        }

        /// <summary>
        /// Handles the <see cref="E:MinScaleChanged" /> event.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void OnMinScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WallControl view = d as WallControl;
            if (view == null) return;
            view.Scale = Math.Max(view.Scale, (double)e.NewValue);
            view.Increment = (view.MaxScale - view.MinScale) / 20.0d;
        }

        /// <summary>
        /// Handles the wall changed business.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnWallChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WallControl view = d as WallControl;
            if (view == null) return;
            view.Rejigger();
        }

        private void RecalculateSourceResizeBorderThickness()
        {
            double cursorHeightSizeInPixels = SystemParameters.CursorHeight;
            double cursorWidthSizeInPixels = SystemParameters.CursorWidth;
            double approxCursorSize = (cursorHeightSizeInPixels + cursorWidthSizeInPixels) / 2;
            if (this.Scale > 0.05)
                this.ResizeSourceBorderThickness = 2*approxCursorSize / Math.Sqrt(this.Scale);
        }
    }
}
