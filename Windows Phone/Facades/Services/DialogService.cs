using SharedLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Facades.Services
{
    public class DialogService : IDialogService
    {
        public Task<int?> ShowAsync(string caption, string message, IEnumerable<string> buttons, bool playSound = false)
        {
            throw new NotImplementedException();
        }
    }
}
