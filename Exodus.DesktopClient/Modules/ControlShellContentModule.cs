using Exodus.DesktopClient.ViewModels.Shells;
using Exodus.DesktopClient.Views.UserControls.Hub;
using Exodus.DesktopClient.Views.UserControls.Login;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Exodus.DesktopClient.Modules
{
    /// <summary>
    /// Registering all the things that can be loaded in the control shell region.
    /// </summary>
    public class ControlShellContentModule : IModule
    {
        #region Fields

        /// <summary>
        /// The region manager
        /// </summary>
        private IRegionManager _regionManager;

        #endregion

        public ControlShellContentModule(IRegionManager regionManager)
        {
            this._regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            this._regionManager.RequestNavigate("ControlShellRegion", nameof(Login));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HubContainer>();
            containerRegistry.RegisterForNavigation<Login>();
        }

    }
}
