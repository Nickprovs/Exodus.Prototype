using System;
using System.Globalization;
using System.Windows.Data;

namespace Exodus.Common.Controls.Converters
{
    public class DoubleToScaledDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double doubleToBeScaled = (double) value;
            double scale = 0.1;
            double.TryParse(parameter.ToString(), out scale);

            return doubleToBeScaled * scale;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
