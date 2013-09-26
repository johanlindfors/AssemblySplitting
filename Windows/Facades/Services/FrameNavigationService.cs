using SharedLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Facades.Services
{
    public class FrameNavigationService : INavigationService
    {
        readonly Frame frame = null;
        Dictionary<string, Type> views = null;

        public FrameNavigationService(Frame frame)
        {
            this.frame = frame;
            views = new Dictionary<string, Type>();
        }

        public bool CanGoBack
        {
            get { return frame.CanGoBack; }
        }

        public void Navigate(string view)
        {
            if (views.ContainsKey(view))
                frame.Navigate(views[view]);
        }

        public void Navigate(string view, Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public void NavigateToUri(string uri)
        {
            throw new NotImplementedException();
        }

        public void RegisterView<T>(string key, T page)
        {
            if (views.ContainsKey(key))
                views[key] = page as Type;
            else
                views.Add(key, page as Type);
        }

        public void RemoveBackEntry()
        {
            //throw new NotImplementedException();
        }
    }
}
