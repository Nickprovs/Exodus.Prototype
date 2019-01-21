using Exodus.DesktopClient.Data.Types;
using System.Windows;

namespace Exodus.DesktopClient.Common.Utilities
{
    public static class Helper
    {
        /// <summary>
        /// Drops the specified source.
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <param name="source">The source.</param>
        public static void Drop(this DcBase payload, DependencyObject source)
        {
            var data = new DataObject("DcBase", payload);
            DragDrop.DoDragDrop(source, data, DragDropEffects.Copy);
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args">The <see cref="DragEventArgs" /> instance containing the event data.</param>
        /// <returns>T.</returns>
        public static T GetData<T>(this DragEventArgs args) where T : DcBase
        {
            var data = args.Data;
            var obj = data.GetData("DcBase");
            return obj as T;
        }
    }
}
