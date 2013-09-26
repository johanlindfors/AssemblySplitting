using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedLibrary.Services.Interfaces
{
    public interface IDialogService
    {
        //void Show(string caption, string message, Action OnOkCallback = null, Action OnCancelCallback = null, IEnumerable<string> buttons = null, bool playSound = false);
        Task<int?> ShowAsync(string caption, string message, IEnumerable<string> buttons, bool playSound = false);
    }
}
