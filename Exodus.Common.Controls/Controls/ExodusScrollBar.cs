using System.Windows;
using System.Windows.Controls.Primitives;

namespace Exodus.Common.Controls.Controls
{
    public class ExodusScrollBar : ScrollBar
    {
        static ExodusScrollBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExodusScrollBar), new FrameworkPropertyMetadata(typeof(ExodusScrollBar)));
        }
    }
}
