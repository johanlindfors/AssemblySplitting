using SharedLibrary.Infrastructure;
using SharedLibrary.Infrastructure.Ioc;
using SharedLibrary.Services.Interfaces;
using Shell.Infrastructure;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Shell
{
    public sealed partial class SplashPage : Page
    {
        internal Frame rootFrame;

        public SplashPage()
        {
            this.InitializeComponent();

            rootFrame = new Frame();

            this.Loaded += OnSplashPageLoaded;
        }

        private async void OnSplashPageLoaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("SplashPage loaded");

            await Task.WhenAll(SomeAsyncWork(),RunBootstrapper());

            UpdateResources();

            var result = await TryAutoLogin();

            NavigateAwayFromSplashPage();
        }

        Task<bool> TryAutoLogin()
        {
            var socialService = ServiceLocator.Resolve<ISocialService>();
            return socialService.ValidateToken();
        }

        async Task SomeAsyncWork()
        {
            Debug.WriteLine("Doing async work");
            await Task.Delay(2000);
            Debug.WriteLine("Async work done");
        }

        async Task RunBootstrapper()
        {
            Bootstrapper.BuildUp(rootFrame);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            ServiceLocator.Resolve<INavigationService>().RemoveBackEntry();
        }

        private void NavigateAwayFromSplashPage()
        {
            Debug.WriteLine("Navigating to MainPage");
            Window.Current.Content = rootFrame;
            ServiceLocator.Resolve<INavigationService>().Navigate("MainPage");
        }

        private void UpdateResources()
        {
            App.Current.Resources.Add("LocalizedStrings", new LocalizedStrings());
            App.Current.Resources.Add("ViewModelLocator", new ViewModelLocator(ServiceLocator.Resolve<IContainer>()));
            App.Current.Resources.Add("DeviceSpecificsLocator", new DeviceSpecificsLocator(ServiceLocator.Resolve<IContainer>()));
        }
    }
}
