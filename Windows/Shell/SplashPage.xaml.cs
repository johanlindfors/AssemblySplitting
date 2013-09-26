using SharedLibrary.Infrastructure;
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

            var delay = Task.Run(async () =>
            {
                Debug.WriteLine("Doing async work");
                await Task.Delay(2000);
                Debug.WriteLine("Async work done");
            });
            Bootstrapper.BuildUp(rootFrame);

            await Task.WhenAll(delay);

            UpdateResources();

            NavigateAwayFromSplashPage();
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
        }        
    }
}
