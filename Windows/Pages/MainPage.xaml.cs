using SharedLibrary.Infrastructure;
using SharedLibrary.Services.Interfaces;
using SharedLibrary.ViewModels.Interfaces;
using Windows.UI.Xaml.Controls;

namespace Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.DataContext = ServiceLocator.Resolve<IMainViewModel>();
        }
    }
}
