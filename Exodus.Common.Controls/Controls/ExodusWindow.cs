
namespace Exodus.Common.Controls.Controls
{
    using Exodus.Common.Controls.Utilities;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// Class ExodusWindow.
    /// </summary>
    /// <seealso cref="System.Windows.Controls.ContentControl" />
    public class ExodusWindow : ContentControl
    {
        #region Fields
        /// <summary>
        /// The title property
        /// </summary>
        public static readonly DependencyProperty WindowTitleProperty =
            DependencyProperty.Register("WindowTitle", typeof(string), typeof(ExodusWindow), new PropertyMetadata("Exodus", WindowTitleCallback));

        /// <summary>
        /// The window header size property
        /// </summary>
        public static readonly DependencyProperty WindowHeaderSizeProperty =
            DependencyProperty.Register("WindowHeaderSize", typeof(double), typeof(ExodusWindow), new PropertyMetadata(35.0));

        /// <summary>
        /// The icon property
        /// </summary>
        public static readonly DependencyProperty WindowIconProperty =
            DependencyProperty.Register("WindowIcon", typeof(object), typeof(ExodusWindow));

        /// <summary>
        /// The window state property
        /// </summary>
        public static readonly DependencyProperty WindowStateProperty =
            DependencyProperty.Register("WindowState", typeof(WindowState), typeof(ExodusWindow), new PropertyMetadata(default(WindowState)));

        /// <summary>
        /// IsMaximized to check if window is maximized/restored (since we can't rely on WindowState anymore).
        /// </summary>
        public static readonly DependencyProperty IsMaximizedProperty =
            DependencyProperty.Register("IsMaximized", typeof(bool), typeof(ExodusWindow), new PropertyMetadata(false));

        /// <summary>
        /// The _close
        /// </summary>
        private Button _close;

        /// <summary>
        /// The first load occured
        /// </summary>
        private bool _firstLoadOccured;

        /// <summary>
        /// The _maximize
        /// </summary>
        private Button _maximize;

        /// <summary>
        /// The _minimize
        /// </summary>
        private Button _minimize;

        /// <summary>
        /// The _title
        /// </summary>
        private FrameworkElement _title;

        /// <summary>
        /// Cache the last window region to restore from a maximize.
        /// </summary>
        private Rect _lastRect;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="ExodusWindow" /> class.
        /// </summary>
        static ExodusWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExodusWindow), new FrameworkPropertyMetadata(typeof(ExodusWindow)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExodusWindow" /> class.
        /// </summary>
        public ExodusWindow()
        {
            this.Loaded += this.ExodusWindow_Loaded;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this window is maximized.
        /// </summary>
        /// <value><c>true</c> if this instance is maximized; otherwise, <c>false</c>.</value>
        public bool IsMaximized
        {
            get { return (bool)this.GetValue(IsMaximizedProperty); }
            set { this.SetValue(IsMaximizedProperty, value); }
        }

        /// <summary>
        /// Gets or sets the size of the window header.
        /// </summary>
        /// <value>The size of the window header.</value>
        public double WindowHeaderSize
        {
            get { return (double)this.GetValue(WindowHeaderSizeProperty); }
            set { this.SetValue(WindowHeaderSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets a window's icon.
        /// </summary>
        /// <value>The icon.</value>
        public object WindowIcon
        {
            get { return this.GetValue(WindowIconProperty); }
            set { this.SetValue(WindowIconProperty, value); }
        }

        /// <summary>
        /// Gets or sets the state of the window.
        /// </summary>
        /// <value>The state of the window.</value>
        public WindowState WindowState
        {
            get { return (WindowState)this.GetValue(WindowStateProperty); }
            set { this.SetValue(WindowStateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string WindowTitle
        {
            get { return (string)this.GetValue(WindowTitleProperty); }
            set { this.SetValue(WindowTitleProperty, value); }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call
        /// <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //The title bar... we conditionally assign an event handler to this is we're a real Windows window
            this._title = this.Template.FindName("PART_Title", this) as FrameworkElement;

            //The close button
            this._close = this.Template.FindName("PART_Close", this) as Button;
            if (this._close != null) this._close.Click += this.Close_Click;

            //The minimize button
            this._minimize = this.Template.FindName("PART_Minimize", this) as Button;
            if (this._minimize != null) this._minimize.Click += this.Minimize_Click;

            //The maximize button
            this._maximize = this.Template.FindName("PART_Maximize", this) as Button;
            if (this._maximize != null) this._maximize.Click += this.Maximize_Click;

            //Not all ExodusWindows are DIRECTLY inside of a real window with a set title. 
            //So let's check if our window has a title... And if so... set our title equal to that.

            var window = this.GetParentWindow(this);
            if (window == null) return;
            if (!string.IsNullOrEmpty(this.WindowTitle))
                window.Title = this.WindowTitle;
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Windows the callback.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void WindowTitleCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var aw = d as ExodusWindow;
            var window = aw?.GetParentWindow(aw);
            if (window == null) return;
            window.Title = aw.WindowTitle;
        }

        /// <summary>
        /// Handles the Loaded event of the ExodusWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ExodusWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.StateChanged += this.Window_StateChanged;
            }

            if (!this._firstLoadOccured)
            {
                this._title.MouseLeftButtonDown += this.Title_MouseLeftButtonDown;
                this._firstLoadOccured = true;
            }
        }

        /// <summary>
        /// Handles the StateChanged event of the Window control.
        /// This event is raised if the user taps [WIN+UP] or smashes the window into the upper region of the monitor.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Window_StateChanged(object sender, EventArgs e)
        {
            // Since ExodusWindow has a custom title bar maximize will not work properly.
            // The code below prevents the window from being officially maximized. Instead we'll handle maximize/restore ourselves.
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;

                if (!this.IsMaximized)
                {
                    this.IsMaximized = true;
                    this.MaximizeWindow();
                }
            }
        }

   
        /// <summary>
        /// Handles the Click event of the Close control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            var parent = this.GetParentWindow(this);
            parent.Close();
        }

        /// <summary>
        /// Gets the parent window.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <returns>Window.</returns>
        private Window GetParentWindow(DependencyObject child)
        {
            while (true)
            {
                DependencyObject parentObject = VisualTreeHelper.GetParent(child);

                if (parentObject == null) return null;

                Window parent = parentObject as Window;
                if (parent != null) return parent;
                child = parentObject;
            }
        }

        /// <summary>
        /// Handles the Click event of the Maximize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsMaximized)
            {
                this.IsMaximized = false;
                this.RestoreWindow();
            }
            else
            {
                this.IsMaximized = true;
                this.MaximizeWindow();
            }
        }

        /// <summary>
        /// Maximizes the window.Since WindowState cannot be used this will simulate a window maximize.
        /// </summary>
        private void MaximizeWindow()
        {
            var window = Window.GetWindow(this);
            if (window != null)
            {
                var currentLocationOfWindow = new Point(window.Left, window.Top);
                var screen = WpfScreenHelper.Screen.FromPoint(currentLocationOfWindow);




                this._lastRect = new Rect(window.Left, window.Top, window.Width, window.Height);
                window.Height = screen.Bounds.Height;
                window.Width = screen.Bounds.Width;
                window.Left = screen.Bounds.Left;
                window.Top = screen.Bounds.Top;
                window.ResizeMode = ResizeMode.CanMinimize;
            }


 
            //var window = Window.GetWindow(this);
            //if (window != null)
            //{
            //    this._lastRect = new Rect(window.Left, window.Top, window.Width, window.Height);
            //    window.Height = info.WorkingAreaPixels.ToDeviceIndependentRect(info.DpiX, info.DpiY).Height;
            //    window.Width = info.WorkingAreaPixels.ToDeviceIndependentRect(info.DpiX, info.DpiY).Width;
            //    window.Left = info.WorkingAreaPixels.ToDeviceIndependentRect(info.DpiX, info.DpiY).Left;
            //    window.Top = info.WorkingAreaPixels.ToDeviceIndependentRect(info.DpiX, info.DpiY).Top;
            //    window.ResizeMode = ResizeMode.CanMinimize;
            //}
            
        }

        /// <summary>
        /// Restores the window. Since WindowState cannot be used this will simulate a window restore.
        /// </summary>
        private void RestoreWindow()
        {
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.ResizeMode = ResizeMode.CanResize;
                window.Height = this._lastRect.Height;
                window.Width = this._lastRect.Width;
                window.Left = this._lastRect.Left;
                window.Top = this._lastRect.Top;
            }
        }

        /// <summary>
        /// Handles the Click event of the Minimize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {

            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the WindowTitle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs" /> instance containing the event data.</param>
        private void Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //If we're maximized, no need to move the window.
            if (this.IsMaximized) return;

            //If we're inside a window
            var parent = this.GetParentWindow(this);
            parent.DragMove();
        }

        #endregion
    }
}
