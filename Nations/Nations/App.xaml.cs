using Nations.Services;
using Nations.ViewModels;
using Nations.Views;

using Prism;
using Prism.Ioc;

using Syncfusion.Licensing;

using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace Nations
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MzIyMjYwQDMxMzgyZTMyMmUzMFBYUGxzd0Nqc0lFMFo3MWFBWUpWZkVPT1hOY2JsUFMrRWRjeUhpRmVzanM9");


            InitializeComponent();

            await NavigationService.NavigateAsync($"NavigationPage/{nameof(LoginPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<CountryDetailPage, CountryDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<CountriesPage, CountriesPageViewModel>();
            containerRegistry.Register<IApiService, ApiService>();
        }
    }
}
