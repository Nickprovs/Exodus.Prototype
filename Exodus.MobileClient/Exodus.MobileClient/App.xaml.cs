using Exodus.MobileClient.Interfaces;
using Exodus.MobileClient.Utilities;
using Exodus.MobileClient.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Exodus.MobileClient
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) {}

        /// <summary>
        /// Handle when your app starts
        /// </summary>
        protected override void OnStart()
        {
        }

        /// <summary>
        /// Handles when your app is about to sleep. It may resume or it may die.
        /// </summary>
        protected override void OnSleep()
        { 

        }

        /// <summary>
        /// Handles when your app resumes from sleep
        /// </summary>
        protected override void OnResume()
        {

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>("Navigation");
            containerRegistry.RegisterForNavigation<LoginPage>("Login");
            containerRegistry.RegisterForNavigation<MainPage>("Index");
            containerRegistry.RegisterForNavigation<AboutPage>("About");
            containerRegistry.RegisterForNavigation<SourcesPage>("Sources");
            containerRegistry.RegisterForNavigation<SessionsPage>("Sessions");
            containerRegistry.RegisterForNavigation<SpaceDisplayPage>("SpaceDisplay");

            containerRegistry.RegisterSingleton<IMcSignalrProxy, McSignalrProxy>();
            containerRegistry.RegisterSingleton<IMcProfile, McProfile>();
            containerRegistry.RegisterSingleton<IMcDeviceContext, McDeviceContext>();

        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var result = await NavigationService.NavigateAsync("Navigation/Login");

            if (!result.Success)           
                SetMainPageFromException(result.Exception);
            
        }

        /// <summary>
        /// Sets the app's main page if we encountered an exception during navigation
        /// </summary>
        /// <param name="ex"></param>
        private void SetMainPageFromException(Exception ex)
        {
            var layout = new StackLayout
            {
                Padding = new Thickness(40)
            };
            layout.Children.Add(new Label
            {
                Text = ex?.GetType()?.Name ?? "Unknown Error encountered",
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            });

            layout.Children.Add(new ScrollView
            {
                Content = new Label
                {
                    Text = $"{ex}",
                    LineBreakMode = LineBreakMode.WordWrap
                }
            });

            MainPage = new ContentPage
            {
                Content = layout
            };
        }
    }
}
