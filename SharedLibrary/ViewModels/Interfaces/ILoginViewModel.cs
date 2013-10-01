using SharedLibrary.Infrastructure;
using System.Windows.Input;

namespace SharedLibrary.ViewModels.Interfaces
{
    public interface ILoginViewModel : IViewModelBase
    {
        ICommand LoginCommand { get; }
        ICommand RegisterCommand { get; }
        ICommand LoginWithFacebookCommand { get; }

        string Username { get; set; }
        string Password { get; set; }
    }
}
