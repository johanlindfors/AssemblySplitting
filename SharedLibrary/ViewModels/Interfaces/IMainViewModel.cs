using SharedLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharedLibrary.ViewModels.Interfaces
{
    public interface IMainViewModel : IViewModelBase
    {
        ICommand SaveCommand { get; }
    }
}
