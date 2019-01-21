#region Using

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

#endregion

namespace Exodus.DesktopClient.Controls.Controls
{
    /// <summary>
    /// Class ZoomAndPanControl.
    /// </summary>
    public partial class ZoomAndPanControl : ContentControl, IScrollInfo {
        #region Fields

        /// <summary>
        /// The content scale transform
        /// </summary>
        public ScaleTransform contentScaleTransform = null;

        /// <summary>
        /// The enable content offset update from scale
        /// </summary>
        private readonly bool enableContentOffsetUpdateFromScale = false;

        /// <summary>
        /// The constrained content viewport height
        /// </summary>
        private double constrainedContentViewportHeight;

        /// <summary>
        /// The constrained content viewport width
        /// </summary>
        private double constrainedContentViewportWidth;

        /// <summary>
        /// The content
        /// </summary>
        private FrameworkElement content;

        /// <summary>
        /// The content offset transform
        /// </summary>
        private TranslateTransform contentOffsetTransform;

        /// <summary>
        /// The disable content focus synchronize
        /// </summary>
        private bool disableContentFocusSync;

        /// <summary>
        /// The disable scroll offset synchronize
        /// </summary>
        private bool disableScrollOffsetSync;

        /// <summary>
        /// The un scaled extent
        /// </summary>
        private Size unScaledExtent = new Size(0, 0);

        /// <summary>
        /// The viewport
        /// </summary>
        private Size viewport = new Size(0, 0);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value that indicates whether scrolling on the horizontal axis is possible.
        /// </summary>
        /// <value><c>true</c> if this instance can horizontally scroll; otherwise, <c>false</c>.</value>
        public bool CanHorizontallyScroll { get; set; } = false;

        /// <summary>
        /// Gets or sets a value that indicates whether scrolling on the vertical axis is possible.
        /// </summary>
        /// <value><c>true</c> if this instance can vertically scroll; otherwise, <c>false</c>.</value>
        public bool CanVerticallyScroll { get; set; } = false;

        /// <summary>
        /// Gets the vertical size of the extent.
        /// </summary>
        /// <value>The height of the extent.</value>
        public double ExtentHeight => this.unScaledExtent.Height * this.ContentScale;

        /// <summary>
        /// Gets the horizontal size of the extent.
        /// </summary>
        /// <value>The width of the extent.</value>
        public double ExtentWidth => this.unScaledExtent.Width * this.ContentScale;

        /// <summary>
        /// Gets the horizontal offset of the scrolled content.
        /// </summary>
        /// <value>The horizontal offset.</value>
        public double HorizontalOffset => this.ContentOffsetX * this.ContentScale;

        /// <summary>
        /// Gets or sets a <see cref="T:System.Windows.Controls.ScrollViewer" /> element that controls scrolling behavior.
        /// </summary>
        /// <value>The scroll owner.</value>
        public ScrollViewer ScrollOwner { get; set; } = null;

        /// <summary>
        /// Gets the vertical offset of the scrolled content.
        /// </summary>
        /// <value>The vertical offset.</value>
        public double VerticalOffset => this.ContentOffsetY * this.ContentScale;

        /// <summary>
        /// Gets the vertical size of the viewport for this content.
        /// </summary>
        /// <value>The height of the viewport.</value>
        public double ViewportHeight => this.viewport.Height;

        /// <summary>
        /// Gets the horizontal size of the viewport for this content.
        /// </summary>
        /// <value>The width of the viewport.</value>
        public double ViewportWidth => this.viewport.Width;

        #endregion

        #region Public Methods

        /// <summary>
        /// Scrolls down within content by one logical unit.
        /// </summary>
        public void LineDown() {
            this.ContentOffsetY += this.ContentViewportHeight / 10;
        }

        /// <summary>
        /// Scrolls left within content by one logical unit.
        /// </summary>
        public void LineLeft() {
            this.ContentOffsetX -= this.ContentViewportWidth / 10;
        }

        /// <summary>
        /// Scrolls right within content by one logical unit.
        /// </summary>
        public void LineRight() {
            this.ContentOffsetX += this.ContentViewportWidth / 10;
        }

        /// <summary>
        /// Scrolls up within content by one logical unit.
        /// </summary>
        public void LineUp() {
            this.ContentOffsetY -= this.ContentViewportHeight / 10;
        }

        /// <summary>
        /// Forces content to scroll until the coordinate space of a <see cref="T:System.Windows.Media.Visual" /> object is visible.
        /// </summary>
        /// <param name="visual">A <see cref="T:System.Windows.Media.Visual" /> that becomes visible.</param>
        /// <param name="rectangle">A bounding rectangle that identifies the coordinate space to make visible.</param>
        /// <returns>A <see cref="T:System.Windows.Rect" /> that is visible.</returns>
        public Rect MakeVisible(Visual visual, Rect rectangle) {
            if (this.content.IsAncestorOf(visual)) {
                Rect transformedRect = visual.TransformToAncestor(this.content).TransformBounds(rectangle);
                Rect viewportRect = new Rect(this.ContentOffsetX, this.ContentOffsetY, this.ContentViewportWidth, this.ContentViewportHeight);
                if (!transformedRect.Contains(viewportRect)) {
                    double horizOffset = 0;
                    double vertOffset = 0;

                    if (transformedRect.Left < viewportRect.Left)
                        horizOffset = transformedRect.Left - viewportRect.Left;
                    else if (transformedRect.Right > viewportRect.Right) horizOffset = transformedRect.Right - viewportRect.Right;

                    if (transformedRect.Top < viewportRect.Top)
                        vertOffset = transformedRect.Top - viewportRect.Top;
                    else if (transformedRect.Bottom > viewportRect.Bottom) vertOffset = transformedRect.Bottom - viewportRect.Bottom;

                    this.SnapContentOffsetTo(new Point(this.ContentOffsetX + horizOffset, this.ContentOffsetY + vertOffset));
                }
            }

            return rectangle;
        }

        /// <summary>
        /// Scrolls down within content after a user clicks the wheel button on a mouse.
        /// </summary>
        public void MouseWheelDown() {
            if (this.IsMouseWheelScrollingEnabled) this.LineDown();
        }

        /// <summary>
        /// Scrolls left within content after a user clicks the wheel button on a mouse.
        /// </summary>
        public void MouseWheelLeft() {
            if (this.IsMouseWheelScrollingEnabled) this.LineLeft();
        }

        /// <summary>
        /// Scrolls right within content after a user clicks the wheel button on a mouse.
        /// </summary>
        public void MouseWheelRight() {
            if (this.IsMouseWheelScrollingEnabled) this.LineRight();
        }

        /// <summary>
        /// Scrolls up within content after a user clicks the wheel button on a mouse.
        /// </summary>
        public void MouseWheelUp() {
            if (this.IsMouseWheelScrollingEnabled) this.LineUp();
        }

        /// <summary>
        /// Scrolls down within content by one page.
        /// </summary>
        public void PageDown() {
            this.ContentOffsetY += this.ContentViewportHeight;
        }

        /// <summary>
        /// Scrolls left within content by one page.
        /// </summary>
        public void PageLeft() {
            this.ContentOffsetX -= this.ContentViewportWidth;
        }

        /// <summary>
        /// Scrolls right within content by one page.
        /// </summary>
        public void PageRight() {
            this.ContentOffsetX += this.ContentViewportWidth;
        }

        /// <summary>
        /// Scrolls up within content by one page.
        /// </summary>
        public void PageUp() {
            this.ContentOffsetY -= this.ContentViewportHeight;
        }

        /// <summary>
        /// Sets the amount of horizontal offset.
        /// </summary>
        /// <param name="offset">The degree to which content is horizontally offset from the containing viewport.</param>
        public void SetHorizontalOffset(double offset) {
            if (this.disableScrollOffsetSync) return;

            try {
                this.disableScrollOffsetSync = true;

                this.ContentOffsetX = offset / this.ContentScale;
            }
            finally {
                this.disableScrollOffsetSync = false;
            }
        }

        /// <summary>
        /// Sets the amount of vertical offset.
        /// </summary>
        /// <param name="offset">The degree to which content is vertically offset from the containing viewport.</param>
        public void SetVerticalOffset(double offset) {
            if (this.disableScrollOffsetSync) return;

            try {
                this.disableScrollOffsetSync = true;

                this.ContentOffsetY = offset / this.ContentScale;
            }
            finally {
                this.disableScrollOffsetSync = false;
            }
        }

        #endregion
    }
}