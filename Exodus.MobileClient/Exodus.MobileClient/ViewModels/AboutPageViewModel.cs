using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exodus.MobileClient.ViewModels
{
    public class AboutPageViewModel : BaseViewModel
    {

        public AboutPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.Title = "About";
        }

    }
}
