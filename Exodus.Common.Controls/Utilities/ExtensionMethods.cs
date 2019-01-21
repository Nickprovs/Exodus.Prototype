using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Exodus.Common.Controls.Utilities
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Converts a Win32 rectangle structure to a WindowsBase Rect that is in device independent units.
        /// </summary>
        /// <param name="nativeRectStruct">The native rect structure.</param>
        /// <param name="dpiX">The horizontal DPI component.</param>
        /// <param name="dpiY">The dpi y.</param>
        /// <returns>Rect.</returns>
        public static Rect ToDeviceIndependentRect(this Rect nativeRectStruct, int dpiX, int dpiY)
        {

            var width = (nativeRectStruct.Right - nativeRectStruct.Left) * 96.0d / dpiY;
            var height = (nativeRectStruct.Bottom - nativeRectStruct.Top) * 96.0d / dpiX;
            var left = nativeRectStruct.Left * 96.0d / dpiX;
            var top = nativeRectStruct.Top * 96.0d / dpiY;

            return new Rect(left, top, width, height);
        }
    }
}
