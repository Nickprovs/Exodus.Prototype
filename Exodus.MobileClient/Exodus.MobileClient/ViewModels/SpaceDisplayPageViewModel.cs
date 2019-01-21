using Exodus.MobileClient.Types;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exodus.MobileClient.ViewModels
{
    public class SpaceDisplayPageViewModel : BaseViewModel
    {
        #region Fields

        private McSession _spaceSession;

        #endregion

        #region Properties

        public McSession SpaceSession
        {
            get { return _spaceSession; }
            set
            {
                SetProperty(ref _spaceSession, value);
                if (this._spaceSession != null)
                    this.Title = this._spaceSession.Name;
            }
        }

        #endregion

        #region Constructors and Destructors

        public SpaceDisplayPageViewModel(INavigationService navigationService) : base (navigationService)
        {
        }

        #endregion

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            McSession newSession;
            bool gotSession = parameters.TryGetValue("session", out newSession);

            if (newSession != null)
                this.SpaceSession = newSession;
        }
    }
}
