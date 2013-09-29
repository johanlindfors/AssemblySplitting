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
            ServiceLocator.Resolve<INavigationService>().Navigate("MainPage");
        }

        private void UpdateResources()
        {
            App.Current.Resources.Add("LocalizedStrings",new LocalizedStrings());
        }        
    }
}