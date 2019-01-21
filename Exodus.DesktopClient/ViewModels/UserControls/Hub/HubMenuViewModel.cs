using Exodus.DesktopClient.PubSub.Events;
using Exodus.DesktopClient.PubSub.Args;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.ViewModels.UserControls.Hub
{
    public class HubMenuViewModel : BindableBase
    {

        #region Fields

        private IEventAggregator _eventAggregator;

        #endregion

        #region Properties

        /// <summary>
        /// When fired, broadcasts to the app that the content of the hub should change.
        /// </summary>
        public DelegateCommand<string> HubContentNavigationCommand { get; private set; }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// The constructor.
        /// </summary>
        public HubMenuViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            HubContentNavigationCommand = new DelegateCommand<string>(OnHubContentNavigate);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Broadcasts to the app that the content of the hub should change.
        /// </summary>
        /// <param name="navigatePath"></param>
        private void OnHubContentNavigate(string navigationPath)
        {
            this._eventAggregator.GetEvent<HubContentNavigateEvent>().Publish(new HubContentNavigateEventArgs(navigationPath));
        }

        #endregion
    }
}
