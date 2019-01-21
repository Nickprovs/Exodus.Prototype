using Exodus.DesktopClient.Interfaces;
using Exodus.DesktopClient.PubSub.Args;
using Exodus.DesktopClient.PubSub.Events;
using Exodus.DesktopClient.Views.UserControls.Hub;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exodus.DesktopClient.ViewModels.UserControls.Hub
{
    public class HubContainerViewModel
    {
        #region Fields
        /// <summary>
        /// The window manager.
        /// </summary>
        private readonly IDcWindowManager _windowManager;

        /// <summary>
        /// The event aggregator.
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// The region manager.
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// This signalr proxy. This is how we communicate with the backend. 
        /// </summary>
        private readonly IDcSignalrProxy _signalrProxy;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="windowManager"></param>
        /// <param name="eventAggregator"></param>
        /// <param name="regionManager"></param>
        /// <param name="signalrProxy"></param>
        public HubContainerViewModel(IDcWindowManager windowManager, IEventAggregator eventAggregator, IRegionManager regionManager, IDcSignalrProxy signalrProxy)
        {
            this._windowManager = windowManager;
            this._eventAggregator = eventAggregator;
            this._regionManager = regionManager;
            this._eventAggregator.GetEvent<HubContentNavigateEvent>().Subscribe(this.OnHubContentNavigateEvent);
            this._signalrProxy = signalrProxy;
        }
        #endregion

        #region Private Methods

        private void OnHubContentNavigateEvent(HubContentNavigateEventArgs args)
        {
            switch (args.NavigationPath)
            {
                case "Sources":
                    this._regionManager.RequestNavigate("HubContentRegion", nameof(HubContentSources));
                    break;
                case "Sessions":
                    this._regionManager.RequestNavigate("HubContentRegion", nameof(HubContentSessions));
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
