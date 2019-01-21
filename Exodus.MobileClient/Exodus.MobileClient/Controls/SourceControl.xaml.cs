using Exodus.MobileClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Exodus.MobileClient.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SourceControl : ContentView
	{
        #region Fields

        public static readonly BindableProperty SourceInstanceProperty = BindableProperty.Create(
            propertyName: nameof(SourceInstance), returnType: typeof(McSourceInstance), declaringType: typeof(SourceControl));

        #endregion

        #region Properties

        public McSourceInstance SourceInstance
        {
            get { return (McSourceInstance) GetValue(SourceInstanceProperty); }
            set { SetValue(SourceInstanceProperty, value); }
        }

        #endregion




        public SourceControl ()
		{
			InitializeComponent ();
		}
	}
}