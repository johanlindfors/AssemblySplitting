using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Threading.Tasks;
using SharedLibrary.Infrastructure;
using SharedLibrary.Services.Interfaces;
using Pages;
using System.Diagnostics;
using System.Threading;
using Facades.Services;
using Microsoft.Practices.Unity;
using Shell.Infrastructure;
using System.Windows;
using SharedLibrary.Infrastructure.Ioc;

namespace Shell
{
    public partial class SplashPage : PhoneApplicationPage
    {
        public SplashPage()
        {
            InitializeComponent();
            this.Loaded += OnSplashPageLoaded;
        }

        private async void OnSplashPageLoaded(object sender, RoutedEventArgs e)
        {
            App.Stopwatch.Stop();
            Debug.WriteLine("Time to display splash: {0}", App.Stopwatch.ElapsedMilliseconds);
            Debug.WriteLine("SplashPage loaded");

            var delay = Task.Run(async () => {
                Debug.WriteLine("Doing async work");
                await Task.Delay(2000);
                Debug.WriteLine("Async work done");
            });
            var bootstrap = Task.Run(() => Bootstrapper.BuildUp());
                        
            await Task.WhenAll(bootstrap,delay);
            
            UpdateResources();

            await NavigateAwayFromSplashPage();
        }

        Task<bool> TryAutoLogin()
        {
            var socialService = ServiceLocator.Resolve<ISocialService>();
            return socialService.ValidateToken();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            ServiceLocator.Resolve<INavigationService>().RemoveBackEntry();
        }

        private async Task NavigateAwayFromSplashPage()
        {
            var autoLoginResult = await TryAutoLogin();
            var navigationService = ServiceLocator.Resolve<INavigationService>();
            if (autoLoginResult)
            {
                Debug.WriteLine("Autologin succeeded, navigating to MainPage");
                navigationService.Navigate("MainPage");
            }
            else
            {
                Debug.WriteLine("Autologin failed, navigating to LoginPage");
                navigationService.Navigate("LoginPage");
            }
        }

        private void UpdateResources()
        {
            App.Current.Resources.Add("LocalizedStrings",new LocalizedStrings());
            App.Current.Resources.Add("ViewModelLocator", new ViewModelLocator(ServiceLocator.Resolve<IContainer>()));
        }        
    }
}