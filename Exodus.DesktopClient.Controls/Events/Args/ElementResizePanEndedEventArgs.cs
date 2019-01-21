
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
    public class ElementResizePanEndedEventArgs : EventArgs
    {
        #region Constructors and Destructors
        /// <summary>
        /// </summary>
        /// <returns>DragMoveEventArgs.</returns>
        public ElementResizePanEndedEventArgs(Rect oldPos, Rect newPos)
        {
            this.OldPos = oldPos;
            this.NewPos = newPos;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the old position.
        /// </summary>
        /// <value>The old position.</value>
        public Rect OldPos { get; set; }

        /// <summary>
        /// Gets or sets the new position.
        /// </summary>
        /// <value>The new position.</value>
        public Rect NewPos { get; set; }
        #endregion
    }
}