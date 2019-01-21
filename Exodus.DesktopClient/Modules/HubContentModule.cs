using Exodus.DesktopClient.Views.UserControls.Hub;
using Prism.Ioc;
using Prism.Modularity;

namespace Exodus.DesktopClient.Modules
{
    /// <summary>
    /// Registering all the things that can be loaded in the hub content region
    /// </summary>
    public class HubContentModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HubContentSources>();
            containerRegistry.RegisterForNavigation<HubContentSessions>();
        }

    }
}
