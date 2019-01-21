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
	public partial class McListViewItemContent : ContentView
	{

        #region Fields

        public static readonly BindableProperty IconProperty = BindableProperty.Create(
            propertyName: nameof(Icon), returnType: typeof(Icon), declaringType: typeof(McListViewItemContent));

        public static readonly BindableProperty NameProperty = BindableProperty.Create(
            propertyName: nameof(Name), returnType: typeof(string), declaringType: typeof(McListViewItemContent));

        public static readonly BindableProperty PayloadProperty = BindableProperty.Create(
            propertyName: nameof(Payload), returnType: typeof(McBase), declaringType: typeof(McListViewItemContent));

        #endregion


        #region Properties

        public Icon Icon
        {
            get { return (Icon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(IconProperty, value); }
        }

        public McBase Payload
        {
            get { return (McBase)GetValue(PayloadProperty); }
            set { SetValue(PayloadProperty, value); }
        }
        #endregion

        public McListViewItemContent()
		{
			InitializeComponent();
		}
	}
}