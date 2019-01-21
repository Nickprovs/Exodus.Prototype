
#region Using

using System;

#endregion

namespace Exodus.DesktopClient.Controls.Events.Args
{
    using System.Windows;
    using Point = System.Windows.Point;

    /// <summary>
    /// Class DragMoveEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ElementPanningEventArgs : EventArgs
    {
        #region Constructors and Destructors
        /// <summary>
        /// </summary>
        /// <returns>DragMoveEventArgs.</returns>
        public ElementPanningEventArgs(Vector delta)
        {
            this.Delta = delta;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the delta.
        /// </summary>
        /// <value>The delta.</value>
        public Vector Delta { get; set; }


        #endregion
    }
}