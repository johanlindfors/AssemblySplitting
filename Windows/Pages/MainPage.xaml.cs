using SharedLibrary.Infrastructure;
using SharedLibrary.Services.Interfaces;
using SharedLibrary.ViewModels.Interfaces;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Pages
{
    public sealed partial class MainPage : Page
    {
        IMainViewModel ViewModel { get { return this.DataContext as IMainViewModel; } }

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var socialService = ServiceLocator.Resolve<ISocialService>();
            var autoLogin = await socialService.ValidateToken();
            if (!autoLogin)
            {
                var result = await socialService.SignIn();
                if (result.Result)
                {
                    var validationResponse = await socialService.ValidateToken();
                }
            }

            await ViewModel.ViewNavigatedTo();
        }
    }
}
