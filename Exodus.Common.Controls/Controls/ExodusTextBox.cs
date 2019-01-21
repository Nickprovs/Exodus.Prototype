using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exodus.Common.Controls.Controls
{
    public class ExodusTextBox : TextBox
    {
        #region Fields

        /// <summary>
        /// The watermark property
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark", typeof(string), typeof(ExodusTextBox));

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the watermark.
        /// </summary>
        /// <value>The watermark.</value>
        public string Watermark
        {
            get { return (string)this.GetValue(WatermarkProperty); }
            set { this.SetValue(WatermarkProperty, value); }
        }

        #endregion

        static ExodusTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExodusTextBox), new FrameworkPropertyMetadata(typeof(ExodusTextBox)));
        }
    }
}
