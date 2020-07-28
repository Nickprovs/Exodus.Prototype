using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Exodus.Common.Controls.Controls
{
    public class ExodusScrollViewer : ScrollViewer
    {

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="ExodusScrollViewer"/> class.
        /// </summary>
        static ExodusScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExodusScrollViewer), new FrameworkPropertyMetadata(typeof(ExodusScrollViewer)));
        }

        #endregion
    }
}
