using SharedLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Facades.Services
{
    public class SettingsService : ISettingsService
    {
        public T Get<T>(string key)
        {
            var result =default(T);
            try
            {
                var localSettings = ApplicationData.Current.LocalSettings;
                if (localSettings.Values.ContainsKey(key))
                    result = (T)localSettings.Values[key];
            }
            catch { }
            return result;
        }

        public void Set<T>(string key, T value)
        {
            var localSettings = ApplicationData.Current.LocalSettings;

            localSettings.Values[key] = value;
        }

        public void Remove(string key)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values.Remove(key);
        }
    }
}
