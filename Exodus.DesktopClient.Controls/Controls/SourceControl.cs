using Exodus.DesktopClient.Common.Events.Args;
using Exodus.DesktopClient.Controls.Behaviors;
using Exodus.DesktopClient.Data.Types;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Exodus.DesktopClient.Controls.Controls
{
    public class SourceControl : ContentControl
    {
        #region Fields

        /// <summary>
        /// The source instance property
        /// </summary>
        public static readonly DependencyProperty SourceInstanceProperty = DependencyProperty.Register(
            "SourceInstance", typeof(DcSourceInstance), typeof(SourceControl), new PropertyMetadata(default(DcSourceInstance)));

        //TODO: See if there's a better way to bind to a command from the context menu to here. Not sure if this routed command business is the best/cleanest way.

        /// <summary>
        /// The bring source to front context menu routed command
        /// </summary>
        public static RoutedCommand BringSourceToFrontContextMenuCommand = new RoutedCommand();

        /// <summary>
        /// The send source to back context menu routed command
        /// </summary>
        public static RoutedCommand SendSourceToBackContextMenuCommand = new RoutedCommand();

        /// <summary>
        /// The remove source context menu routed command
        /// </summary>
        public static RoutedCommand RemoveSourceContextMenuCommand = new RoutedCommand();

        /// <summary>
        /// The resize behavior for this control
        /// </summary>
        private MouseResizeElementBehavior _mouseResizeElementBehavior;

        /// <summary>
        /// The resize source border thickness property. Default's to 15 if standalone source control. Otherwise, calculated in wallcontrol.cs
        /// </summary>
        public static readonly DependencyProperty ResizeSourceBorderThicknessProperty = DependencyProperty.Register(
            "ResizeSourceBorderThickness", typeof(double), typeof(SourceControl), new PropertyMetadata(15d));

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the source instance.
        /// </summary>
        /// <value>The source instance.</value>
        public DcSourceInstance SourceInstance
        {
            get { return (DcSourceInstance)this.GetValue(SourceInstanceProperty); }
            set { this.SetValue(SourceInstanceProperty, value); }
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

        #region Events
        /// <summary>
        /// Occurs when [source panned].
        /// </summary>
        public event EventHandler<SourceInstanceArgs> SourcePanned;

        /// <summary>
        /// Occurs when [source resized].
        /// </summary>
        public event EventHandler<SourceInstanceArgs> SourceResized;

        /// <summary>
        /// Occurs when [source brought to front].
        /// </summary>
        public event EventHandler<SourceInstanceArgs> SourceBringToFront;

        /// <summary>
        /// Occurs when [source sent to back].
        /// </summary>
        public event EventHandler<SourceInstanceArgs> SourceSendToBack;

        /// <summary>
        /// Occurs when [source removed].
        /// </summary>
        public event EventHandler<SourceInstanceArgs> SourceRemove;

        #endregion


        #region Constructors and Destructors

        public SourceControl()
        {
            this.CommandBindings.Add(new CommandBinding(BringSourceToFrontContextMenuCommand, this.BringSourceToFrontContextMenuCommand_Executed, this.BringSourceToFrontContextMenuCommand_CanExecute));
            this.CommandBindings.Add(new CommandBinding(SendSourceToBackContextMenuCommand, this.SendSourceToBackContextMenuCommand_Executed, this.SendSourceToBackContextMenuCommand_CanExecute));
            this.CommandBindings.Add(new CommandBinding(RemoveSourceContextMenuCommand, this.RemoveSourceContextMenuCommand_Executed, this.RemoveSourceContextMenuCommand_CanExecute));
        }

        static SourceControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SourceControl), new FrameworkPropertyMetadata(typeof(SourceControl)));
        }

        #endregion

        #region Public Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //Hook into the source's resize behavior
            this._mouseResizeElementBehavior = this.Template.FindName("MouseResizeElementBehavior", this) as MouseResizeElementBehavior;
            if(this._mouseResizeElementBehavior != null)
            {
                this._mouseResizeElementBehavior.ElementPanEnded += this._mouseResizeElementBehavior_ElementPanEnded;
                this._mouseResizeElementBehavior.ElementResizeEnded += this._mouseResizeElementBehavior_ElementResizeEnded;
            }
        }

        #endregion

        #region Private Methods

        #region Event Handlers

        private void _mouseResizeElementBehavior_ElementResizeEnded(object sender, Events.Args.ElementResizePanEndedEventArgs args)
        {
            this.RaiseSourceResized();
        }

        private void _mouseResizeElementBehavior_ElementPanEnded(object sender, Events.Args.ElementResizePanEndedEventArgs args)
        {
            this.RaiseSourcePanned();
        }

        #endregion

        #region Additional Routed Command Business

        private void BringSourceToFrontContextMenuCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void BringSourceToFrontContextMenuCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.RaiseSourceBringToFront();
        }

        private void SendSourceToBackContextMenuCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SendSourceToBackContextMenuCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.RaiseSourceSendToBack();
        }

        private void RemoveSourceContextMenuCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RemoveSourceContextMenuCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.RaiseSourceRemove();
        }

        #endregion

        #region Event Raisers

        /// <summary>
        /// Raises the source panned.
        /// </summary>
        private void RaiseSourcePanned()
        {
            this.SourcePanned?.Invoke(this, new SourceInstanceArgs(this.SourceInstance));
        }

        /// <summary>
        /// Raises the source resized.
        /// </summary>
        private void RaiseSourceResized()
        {
            this.SourceResized?.Invoke(this, new SourceInstanceArgs(this.SourceInstance));
        }

        /// <summary>
        /// Raises the source bring to front.
        /// </summary>
        private void RaiseSourceBringToFront()
        {
            this.SourceBringToFront?.Invoke(this, new SourceInstanceArgs(this.SourceInstance));
        }

        /// <summary>
        /// Raises the source send to back.
        /// </summary>
        private void RaiseSourceSendToBack()
        {
            this.SourceSendToBack?.Invoke(this, new SourceInstanceArgs(this.SourceInstance));
        }

        /// <summary>
        /// Raises the source closed.
        /// </summary>
        private void RaiseSourceRemove()
        {
            this.SourceRemove?.Invoke(this, new SourceInstanceArgs(this.SourceInstance));
        }

        #endregion

        #endregion
    }
}
