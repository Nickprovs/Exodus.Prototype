using Exodus.MobileClient.Interfaces;
using Exodus.MobileClient.Types;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Exodus.MobileClient.ViewModels
{
    public class SessionsPageViewModel : BaseViewModel
    {
        #region Fields

        private IMcProfile _profile;

        private McSession _selectedSession;

        #endregion

        #region Properties

        public DelegateCommand<McSession> ItemTappedCommand { get; }

        public IMcProfile Profile { get; private set; }

        public McSession SelectedSession
        {
            get { return _selectedSession; }
            set { SetProperty(ref _selectedSession, value); }
        }

        #endregion

        #region Constructors and Destructors

        public SessionsPageViewModel(IMcProfile profile, INavigationService navigationService) : base(navigationService)
        {
            this.Title = "Sessions";

            this.Profile = profile;

            this.ItemTappedCommand = new DelegateCommand<McSession>(this.OnItemTapped);
        }

        #endregion

        #region Private Methods

        private async void OnItemTapped(McSession tappedSession)
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("session", tappedSession);
            var res = await _navigationService.NavigateAsync("SpaceDisplay", navParameters);
        }

        #endregion

    }
}
