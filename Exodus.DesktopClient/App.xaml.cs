using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using Exodus.DesktopClient.Views.Shells;
using Exodus.DesktopClient.Interfaces;
using Exodus.DesktopClient.Utilities;
using Prism.Modularity;
using Exodus.DesktopClient.Modules;
using Exodus.DesktopClient.Engine.Interfaces;
using Exodus.DesktopClient.Engine.Classes;

namespace Exodus.DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<ControlShell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IDcWindowManager, DcWindowManager>();
            containerRegistry.RegisterSingleton<IDcProfile, DcProfile>();
            containerRegistry.RegisterSingleton<IDcSignalrProxy, DcSignalrProxy>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ControlShellContentModule>();
            moduleCatalog.AddModule<HubContentModule>();
        }
    }
}
