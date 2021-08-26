using Prism;
using Prism.Ioc;
using UserTestApplication.Services;
using UserTestApplication.ViewModels;
using UserTestApplication.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace UserTestApplication
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
           
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            MainPage = new MainPage();
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<UserDetailPage>();


            containerRegistry.Register<IUserServices, UserServices>();
        }
    }
}
