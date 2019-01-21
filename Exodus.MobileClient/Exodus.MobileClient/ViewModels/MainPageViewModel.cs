using Exodus.MobileClient.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exodus.MobileClient.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Fields

        private IMcSignalrProxy _signalrProxy;

        #endregion

        #region Properties

        public DelegateCommand<string> NavigateCommand { get; }

        #endregion

        #region Constructors and Destructors

        public MainPageViewModel(IMcSignalrProxy signalrProxy, INavigationService navigationService) : base(navigationService)
        {
            this.Title = "Welcome";

            this._signalrProxy = signalrProxy;

            this.NavigateCommand = new DelegateCommand<string>(this.OnNavigateCommandExecuted);
        }

        #endregion

        #region Private Methods

        private async void OnNavigateCommandExecuted(string path)
        {
            await _navigationService.NavigateAsync(path);
        }

        #endregion
    }
}
