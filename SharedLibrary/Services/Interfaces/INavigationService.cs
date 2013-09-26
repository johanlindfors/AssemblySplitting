using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Services.Interfaces
{
    public interface INavigationService
    {
        bool CanGoBack { get; }
        void Navigate(string view);
        void Navigate(string view, Dictionary<string, object> parameters);
        void NavigateToUri(string uri);
        void RegisterView<T>(string view, T page);
        void RemoveBackEntry();
    }
}
