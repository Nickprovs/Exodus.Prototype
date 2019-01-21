#pragma warning disable 1584,1711,1572,1581,1580

namespace Exodus.DesktopClient.Controls.Behaviors
{

    using Exodus.DesktopClient.Controls.Events.Args;
    using Exodus.DesktopClient.Controls.Utilities;
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interactivity;
 
    /// <summary>
    /// Class MouseResizeElementBehavior.
    /// </summary>
    /// <seealso cref="FrameworkElement" />
    /// <seealso cref="System.Windows.Interactivity.Behavior{FrameworkElement}" />
    public class MouseResizeElementBehavior : Behavior<FrameworkElement> {
        #region Fields

        /// <summary>
        /// The x property
        /// </summary>
        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
            "X", typeof(int), typeof(MouseResizeElementBehavior), new PropertyMetadata(default(int), null));

        /// <summary>
        /// The height property
        /// </summary>
        public static readonly DependencyProperty HeightProperty = DependencyProperty.Register(
            "Height", typeof(int), typeof(MouseResizeElementBehavior), new PropertyMetadata(default(int), null));

        /// <summary>
        /// The width property
        /// </summary>
        public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
            "Width", typeof(int), typeof(MouseResizeElementBehavior), new PropertyMetadata(default(int), null));

        /// <summary>
        /// The resize edge thickness property
        /// </summary>
        public static readonly DependencyProperty ResizeEdgeThicknessProperty = DependencyProperty.Register(
            "ResizeEdgeThickness", typeof(int), typeof(MouseResizeElementBehavior), new PropertyMetadata(8));

        /// <summary>
        /// The y property
        /// </summary>
        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
            "Y", typeof(int), typeof(MouseResizeElementBehavior), new PropertyMetadata(default(int), null));

        /// <summary>
        /// The _left
        /// </summary>
        private bool _left;

        /// <summary>
        /// The _last position
        /// </summary>
        private Point _lastRelDeltaPosition;

        /// <summary>
        /// The _mouse down position
        /// </summary>
        private Rect _mouseDownPosition;

        /// <summary>
        /// The _top
        /// </summary>
        private bool _top;

        /// <summary>
        /// The _resize move state
        /// </summary>
        private ResizeMoveState _resizeMoveState;

        /// <summary>
        /// The _canvas
        /// </summary>
        private Canvas _canvas;

        #endregion

        #region Events
        /// <summary>
        /// Delegate DragMoveDelegate
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public delegate void ElementResizeEndedEventDelegate(object sender, ElementResizePanEndedEventArgs args);

        /// <summary>
        /// Delegate DragMoveDelegate
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public delegate void ElementPanEndedEventDelegate(object sender, ElementResizePanEndedEventArgs args);

        /// <summary>
        /// Delegate ElementPanEndedEventDelegate
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ElementResizePanEventArgs"/> instance containing the event data.</param>
        public delegate void ElementPanningEventDelegate(object sender, ElementPanningEventArgs args);


        /// <summary>
        /// Occurs when [element resized].
        /// </summary>
        public event ElementResizeEndedEventDelegate ElementResizeEnded;

        /// <summary>
        /// Occurs when [element panned].
        /// </summary>
        public event ElementPanEndedEventDelegate ElementPanEnded;
        /// <summary>
        /// Occurs when [element panning]. Exists because this behavior doesn't handle item selection and multiple item movement. So these deltas are used to translate other selected items outside this behavior.
        /// </summary>
        public event ElementPanningEventDelegate ElementPanning;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height {
            get { return (int) this.GetValue(HeightProperty); }
            set { this.SetValue(HeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the resize edge thickness.
        /// </summary>
        /// <value>The resize edge thickness.</value>
        public int ResizeEdgeThickness {
            get { return (int) this.GetValue(ResizeEdgeThicknessProperty); }
            set { this.SetValue(ResizeEdgeThicknessProperty, value); }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width {
            get { return (int) this.GetValue(WidthProperty); }
            set { this.SetValue(WidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>The x.</value>
        public int X {
            get { return (int) this.GetValue(XProperty); }
            set { this.SetValue(XProperty, value); }
        }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>The y.</value>
        public int Y {
            get { return (int) this.GetValue(YProperty); }
            set { this.SetValue(YProperty, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called when [attached].
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached() {
            base.OnAttached();

            this.AssociatedObject.PreviewMouseMove += this.OnMouseMove;
            this.AssociatedObject.PreviewMouseLeftButtonDown += this.OnMouseDown;
            this.AssociatedObject.PreviewMouseLeftButtonUp += this.OnMouseUp;

            this._canvas = this.AssociatedObject.FindVisualAncestor<Canvas>();
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching() {
            base.OnDetaching();
            this.AssociatedObject.PreviewMouseMove -= this.OnMouseMove;
            this.AssociatedObject.PreviewMouseLeftButtonDown -= this.OnMouseDown;
            this.AssociatedObject.PreviewMouseLeftButtonUp -= this.OnMouseUp;
        }

        /// <summary>
        /// Raises the element pan ended.
        /// </summary>
        /// <param name="oldPos">The old position.</param>
        /// <param name="newPos">The new position.</param>
        /// <param name="delta">The delta.</param>
        protected virtual void RaiseElementPanEnded(Rect oldPos, Rect newPos) {
            this.ElementPanEnded?.Invoke(this, new ElementResizePanEndedEventArgs(oldPos, newPos));
        }

        /// <summary>
        /// Raises the element resize ended.
        /// </summary>
        /// <param name="oldPos">The old position.</param>
        /// <param name="newPos">The new position.</param>
        /// <param name="delta">The delta.</param>
        protected virtual void RaiseElementResizeEnded(Rect oldPos, Rect newPos)
        {
            this.ElementResizeEnded?.Invoke(this, new ElementResizePanEndedEventArgs(oldPos, newPos));
        }

        /// <summary>
        /// Raises the element panning.
        /// </summary>
        /// <param name="delta">The delta.</param>
        protected virtual void RaiseElementPanning(Vector delta)
        {
            this.ElementPanning?.Invoke(this, new ElementPanningEventArgs(delta));
        }

        /// <summary>
        /// Handles the <see cref="E:MouseDown" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs" /> instance containing the event data.</param>
        private void OnMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Released) return;
            if (!this.AssociatedObject.IsMouseOver) return;
            if (!this.AssociatedObject.IsMouseCaptured) {
                var mouseMoveObject = e.OriginalSource as DependencyObject;

                //We need to assign mouse start position here as well because start position doesn't update if we're in an associated object's contex menu. And that causes a crazy delta.
                this._lastRelDeltaPosition = e.GetPosition(this._canvas);
                this._mouseDownPosition = new Rect(this.X, this.Y, this.Width, this.Height);
                this.AssociatedObject.CaptureMouse();
            }

            this._left = false;
            this._top = false;
        }

        /// <summary>
        /// Handles the <see cref="E:MouseUp" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs" /> instance containing the event data.</param>
        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            this._lastRelDeltaPosition = new Point(-1, -1);
            Rect mouseUpPosition = new Rect(this.X, this.Y, this.Width, this.Height);

            this.AssociatedObject.ReleaseMouseCapture();

            var mouseMoveObject = e.OriginalSource as DependencyObject;

            if (this._mouseDownPosition != mouseUpPosition)
            {
                if (this._resizeMoveState == ResizeMoveState.Move)
                    this.RaiseElementPanEnded(this._mouseDownPosition, mouseUpPosition);
                else if (this._resizeMoveState != ResizeMoveState.None)
                    this.RaiseElementResizeEnded(this._mouseDownPosition, mouseUpPosition);
            }

            this._left = false;
            this._top = false;
        }

        /// Handles the <see cref="E:MouseMove" /> event.
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void OnMouseMove(object sender, MouseEventArgs e) {
            if (this._canvas == null)
                return;

            Point decimalRelPoint = e.GetPosition(this.AssociatedObject);
            Point relPoint = new Point(Math.Round(decimalRelPoint.X), Math.Round(decimalRelPoint.Y));

            Point decimalCurPos = e.GetPosition(this._canvas);
            Point curPos = new Point(Math.Round(decimalCurPos.X), Math.Round(decimalCurPos.Y));

            var mouseMoveObject = e.OriginalSource as DependencyObject;

            switch (e.LeftButton) {
                case MouseButtonState.Released:
                    if (this.AssociatedObject.IsMouseOver)
                    {

                        //If our object is very tiny (but big enough that we can still resize it)... we need a way to still move it around. By doing this... we bypass the resizeedgebuffer which is an inward resizeedge
                        if ((this.AssociatedObject.MinWidth > 10 && this.AssociatedObject.MinHeight > 10) && Math.Abs(relPoint.X - this.AssociatedObject.ActualWidth / 2) < 10 && Math.Abs(relPoint.Y - this.AssociatedObject.ActualHeight / 2) < 10)
                        {
                            this.AssociatedObject.Cursor = Cursors.SizeAll;
                            this._resizeMoveState = ResizeMoveState.Move;
                            break;
                        }

                        //North Check
                        if (!(relPoint.X < this.ResizeEdgeThickness) && !(this.AssociatedObject.ActualWidth - relPoint.X < this.ResizeEdgeThickness) && (this.AssociatedObject.ActualHeight - relPoint.Y < this.ResizeEdgeThickness))
                        {
                            this.AssociatedObject.Cursor = Cursors.SizeNS;
                            this._resizeMoveState = ResizeMoveState.ResizeFromSouth;
                        }

                        //North East Check
                        else if (relPoint.X < this.ResizeEdgeThickness && this.AssociatedObject.ActualHeight - relPoint.Y < this.ResizeEdgeThickness)
                        {
                            this.AssociatedObject.Cursor = Cursors.SizeNESW;
                            this._resizeMoveState = ResizeMoveState.ResizeFromSouthWest;
                        }

                        //East Check
                        else if ((relPoint.X < this.ResizeEdgeThickness) && !(relPoint.Y < this.ResizeEdgeThickness) && !(this.AssociatedObject.ActualHeight - relPoint.Y < this.ResizeEdgeThickness))
                        {
                            this.AssociatedObject.Cursor = Cursors.SizeWE;
                            this._resizeMoveState = ResizeMoveState.ResizeFromWest;
                        }

                        //South East Check
                        else if (relPoint.X < this.ResizeEdgeThickness && relPoint.Y < this.ResizeEdgeThickness)
                        {
                            this.AssociatedObject.Cursor = Cursors.SizeNWSE;
                            this._resizeMoveState = ResizeMoveState.ResizeFromNorthWest;
                        }

                        //South Check
                        else if (!(relPoint.X < this.ResizeEdgeThickness) && !(this.AssociatedObject.ActualWidth - relPoint.X < this.ResizeEdgeThickness) && (relPoint.Y < this.ResizeEdgeThickness))
                        {
                            this.AssociatedObject.Cursor = Cursors.SizeNS;
                            this._resizeMoveState = ResizeMoveState.ResizeFromNorth;
                        }

                        //South West Check
                        else if (this.AssociatedObject.ActualWidth - relPoint.X < this.ResizeEdgeThickness && relPoint.Y < this.ResizeEdgeThickness)
                        {
                            this.AssociatedObject.Cursor = Cursors.SizeNESW;
                            this._resizeMoveState = ResizeMoveState.ResizeFromNorthEast;
                        }

                        //West Check
                        else if ((this.AssociatedObject.ActualWidth - relPoint.X < this.ResizeEdgeThickness) && !(relPoint.Y < this.ResizeEdgeThickness) && !(this.AssociatedObject.ActualHeight - relPoint.Y < this.ResizeEdgeThickness))
                        {
                            this.AssociatedObject.Cursor = Cursors.SizeWE;
                            this._resizeMoveState = ResizeMoveState.ResizeFromEast;
                        }

                        //North West Check
                        else if (this.AssociatedObject.ActualWidth - relPoint.X < this.ResizeEdgeThickness && this.AssociatedObject.ActualHeight - relPoint.Y < this.ResizeEdgeThickness)
                        {
                            this.AssociatedObject.Cursor = Cursors.SizeNWSE;
                            this._resizeMoveState = ResizeMoveState.ResizeFromSouthEast;
                        }

                        //If our mouse is over the thing, and we're not on a section of the resize border... we're here to move.
                        else
                        {
                            this.AssociatedObject.Cursor = Cursors.SizeAll;
                            this._resizeMoveState = ResizeMoveState.Move;
                        }
                    }

                    //If our mouse is not over the thing... we're going to restore cursor state.
                    else
                    {
                        this.AssociatedObject.Cursor = null;
                        this._resizeMoveState = ResizeMoveState.None;
                    }
                    break;

                case MouseButtonState.Pressed:
                    if (!this.AssociatedObject.IsMouseCaptured) return;

                    var delta = curPos - this._lastRelDeltaPosition;

                    var oldPosition = new Rect(this.X, this.Y, this.Width, this.Height);

                    switch (this._resizeMoveState)
                    {
                        case ResizeMoveState.ResizeFromNorth:
                            if (relPoint.Y < this.ResizeEdgeThickness || this._top)
                            {
                                this.ResizeTop(delta);
                                this._top = true;
                            }
                            break;

                        case ResizeMoveState.ResizeFromNorthEast:
                            if (relPoint.Y < this.ResizeEdgeThickness || this._top)
                            {
                                this.ResizeTop(delta);
                                this.ResizeRight(delta);
                                this._top = true;
                            }
                            break;

                        case ResizeMoveState.ResizeFromEast:
                            this.ResizeRight(delta);
                            break;

                        case ResizeMoveState.ResizeFromSouthEast:
                            this.ResizeBottom(delta);
                            this.ResizeRight(delta);
                            break;

                        case ResizeMoveState.ResizeFromSouth:
                            this.ResizeBottom(delta);
                            break;

                        case ResizeMoveState.ResizeFromSouthWest:
                            this._left = true;
                            this.ResizeBottom(delta);
                            this.ResizeLeft(delta);
                            break;

                        case ResizeMoveState.ResizeFromWest:
                            if (relPoint.X < this.ResizeEdgeThickness || this._left)
                            {
                                this.ResizeLeft(delta);
                                this._left = true;
                            }
                            break;

                        case ResizeMoveState.ResizeFromNorthWest:
                            if (relPoint.X < this.ResizeEdgeThickness || this._left && this._top)
                            {
                                this.ResizeTop(delta);
                                this.ResizeLeft(delta);
                                this._left = true;
                                this._top = true;
                            }
                            break;
                        //Moving element case
                        case ResizeMoveState.Move:
                            this.TryMoveElement(delta);
                            break;
                    }
                    var newPosition = new Rect(this.X, this.Y, this.Width, this.Height);

                    if (oldPosition != newPosition)
                    {
                        //If we're panning... raise element panning
                        if(this._resizeMoveState == ResizeMoveState.Move)
                            this.RaiseElementPanning(delta);
                    }

                                Debug.WriteLine($"NEW POSITION:  X:{this.X}, Y: {this.Y}, Width: {this.Width}, Height: {this.Height} - Mouse Resize Behavior");


                    break;
            }

            this._lastRelDeltaPosition = new Point(Math.Round(curPos.X), Math.Round(curPos.Y));
        }

        /// <summary>
        /// Tries the move element.
        /// </summary>
        /// <param name="delta">The delta.</param>
        private void TryMoveElement(Vector delta)
        {          
            if (this.X + delta.X + this.AssociatedObject?.ActualWidth < this._canvas?.ActualWidth && this.X + delta.X > 0)            
                this.X += (int)delta.X;
            

            if (this.Y + delta.Y + this.AssociatedObject?.ActualHeight < this._canvas?.ActualHeight && this.Y + delta.Y > 0)          
                this.Y += (int)delta.Y;

        }

        /// <summary>
        /// Resizes the top.
        /// </summary>
        /// <param name="delta">The delta.</param>
        private void ResizeTop(Vector delta)
        {
            //Only resize top if our Y + delta is greater than 0 and our height resize doesn't put us smaller than 10 pixels.
            if (this.Y + delta.Y > 0 && this.Height - delta.Y > this.AssociatedObject.MinHeight)
            {
                this.Height -= (int)delta.Y;
                this.Y += (int)delta.Y;
            }
        }

        /// <summary>
        /// Resizes the right.
        /// </summary>
        /// <param name="delta">The delta.</param>
        private void ResizeRight(Vector delta)
        {
            //Only resize right if our right edge + delta is less than Canvas Width and our width resize doesn't put us smaller than 10 pixels.
            if (this.X + this.AssociatedObject.ActualWidth + delta.X < this._canvas.ActualWidth && this.Width + delta.X > this.AssociatedObject.MinWidth)
                this.Width += (int)delta.X;
        }

        /// <summary>
        /// Resizes the bottom.
        /// </summary>
        /// <param name="delta">The delta.</param>
        private void ResizeBottom(Vector delta)
        {
            //Only resize bottom if our bottom edge + delta is less than the canvas height and our height resize doesn't put us smaller than 10 pixels.
            if (this.Y + this.AssociatedObject.ActualHeight + delta.Y < this._canvas.ActualHeight && this.Height + delta.Y > this.AssociatedObject.MinHeight)
                this.Height += (int)delta.Y;
        }

        /// <summary>
        /// Resizes the left.
        /// </summary>
        /// <param name="delta">The delta.</param>
        private void ResizeLeft(Vector delta)
        {
            //Only resize left if our X + delta is greater than 0 and our width resize doesn't put us smaller than 10 pixels.
            if (this.X + delta.X > 0 && this.Width - delta.X > this.AssociatedObject.MinWidth)
            {
                this.Width -= (int)delta.X;
                this.X += (int)delta.X;
            }    
        }
        #endregion
    }

    public enum ResizeMoveState
    {
        ResizeFromNorth,
        ResizeFromNorthEast,
        ResizeFromEast,
        ResizeFromSouthEast,
        ResizeFromSouth,
        ResizeFromSouthWest,
        ResizeFromWest,
        ResizeFromNorthWest,
        Move,
        None
    }


}