using SharedLibrary.Infrastructure;
using SharedLibrary.Services.Interfaces;
using Windows.UI.Xaml.Controls;

namespace Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var canGoBack = ServiceLocator.Resolve<INavigationService>().CanGoBack;
        }
    }
}
