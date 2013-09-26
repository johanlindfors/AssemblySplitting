using SharedLibrary.Services.Interfaces;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facades.Services
{
    public class RootFrameNavigationService : INavigationService
    {        
        readonly PhoneApplicationFrame rootFrame = null;
        Dictionary<string, string> views = null;

        public RootFrameNavigationService(PhoneApplicationFrame rootFrame)
        {
            this.rootFrame = rootFrame;
            views = new Dictionary<string, string>();
        }

        public bool CanGoBack
        {
            get { return rootFrame.CanGoBack; }
        }

        public void NavigateToUri(string uri)
        {
            rootFrame.Navigate(new Uri(uri, UriKind.Relative));
        }

        public void Navigate(string view)
        {
            if (views.ContainsKey(view))
                rootFrame.Navigate(new Uri(views[view], UriKind.Relative));
        }

        public void Navigate(string view, Dictionary<string,object> parameters) {
            if (parameters == null || parameters.Count == 0)
            {
                Navigate(view);
            }
            else
            {
                var sb = new StringBuilder();
                bool firstItem = true;
                foreach (var item in parameters)
                {
                    sb.Append(string.Format("{0}{1}={2}",firstItem ? "?" : "&", item.Key, item.Value));
                    firstItem = false;
                }
                NavigateToUri(views[view] + sb.ToString());
            }
        }

        public void RegisterView<T>(string key, T page)
        {
            if (views.ContainsKey(key))
                views[key] = page as string;
            else
                views.Add(key, page as string);
        }

        public void RemoveBackEntry()
        {
            rootFrame.RemoveBackEntry();
        }
    }
}
