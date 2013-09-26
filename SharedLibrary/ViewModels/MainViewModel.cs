using SharedLibrary.Infrastructure;
using SharedLibrary.Services.Interfaces;
using SharedLibrary.ViewModels.Interfaces;
using System.Windows.Input;

namespace SharedLibrary.ViewModels
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        public ICommand SaveCommand { get; private set; }

        public MainViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            SaveCommand = new RelayCommand(SaveAction) { IsEnabled = true };
        }

        void SaveAction()
        {
            navigationService.Navigate("SecondPage");
        }
    }
}
