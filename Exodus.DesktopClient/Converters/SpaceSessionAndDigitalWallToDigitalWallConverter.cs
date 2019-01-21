using Exodus.DesktopClient.Data.Types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Exodus.DesktopClient.Converters
{
    /// <summary>
    /// The purpose of this converter is to provide binding updates for the wall. When binding Session.Wall to the wall control, the wall is only notified when JUST the wall changes. So if we swap the view with a new session... it doesn't get updated.
    /// By multibinding to both Session and Session.Wall, we can give the wall just what it needs (the wall) and reevaluate the bindings when the session changes too because the converter listens for that change.
    /// </summary>
    public class SpaceSessionAndDigitalWallToDigitalWallConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            if (values[0] == null)
                return null;

            if (values[1] == null)
                return null;

            return values[1] as DcDigitalWall;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
