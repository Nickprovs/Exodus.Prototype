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
	public partial class WallControl : ContentView
	{
        #region Fields

        public static readonly BindableProperty WallProperty = BindableProperty.Create(
            propertyName: nameof(Wall), returnType: typeof(McWall), declaringType: typeof(WallControl), propertyChanged: WallPropertyChangedCallback);

        #endregion

        #region Properties

        public McWall Wall
        {
            get { return (McWall)GetValue(WallProperty); }
            set { SetValue(WallProperty, value); }
        }

        #endregion


        #region Contructors and Destructors

        public WallControl ()
		{
			InitializeComponent ();
		}

        #endregion

        #region Property Changed Callbacks

        private static void WallPropertyChangedCallback(BindableObject bindable, object oldValue, object newValue)
        {
            var wallControl = (WallControl)bindable;
            var test = wallControl.Wall;
        }

        #endregion
    }
}