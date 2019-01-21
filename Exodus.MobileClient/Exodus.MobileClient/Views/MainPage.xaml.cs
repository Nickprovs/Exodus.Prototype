using Exodus.Common.Data.Models;
using Exodus.MobileClient.Utilities;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Exodus.MobileClient.Views
{
    public partial class MainPage : MasterDetailPage, IMasterDetailPageOptions
    {
        public bool IsPresentedAfterNavigation => Device.Idiom != TargetIdiom.Phone;

        public MainPage()
        {
            InitializeComponent();
        }
    }

}
