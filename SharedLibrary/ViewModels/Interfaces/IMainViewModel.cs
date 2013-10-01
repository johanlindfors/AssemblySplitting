using SharedLibrary.Infrastructure;
using System.Windows.Input;

namespace SharedLibrary.ViewModels.Interfaces
{
    public interface IMainViewModel : IViewModelBase
    {
        ICommand SaveCommand { get; }
    }
}
