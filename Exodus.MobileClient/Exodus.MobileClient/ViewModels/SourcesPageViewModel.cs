using Exodus.MobileClient.Interfaces;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Exodus.MobileClient.ViewModels
{
    public class SourcesPageViewModel : BaseViewModel
    {
        #region Fields

        private IMcProfile _profile;

        #endregion

        #region Properties

        public IMcProfile Profile { get; private set; }

        #endregion

        #region Constructors and Destructors

        public SourcesPageViewModel(IMcProfile profile, INavigationService navigationService) : base(navigationService)
        {
            this.Title = "Sources";

            this.Profile = profile;
        }

        #endregion

    }
}
