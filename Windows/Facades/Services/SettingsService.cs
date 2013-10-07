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
        private SettingsPolicy policy;

        public SettingsService()
        {

        }

        public SettingsService(SettingsPolicy policy) : this()
        {
            this.policy = policy;
        }

        public T Get<T>(string key)
        {
            var result = default(T);
            try
            {
                ApplicationDataContainer settings;
                if (policy == SettingsPolicy.Roaming)
                    settings = ApplicationData.Current.RoamingSettings;
                else
                    settings = ApplicationData.Current.LocalSettings;
                if (settings.Values.ContainsKey(key))
                    result = (T)settings.Values[key];
            }
            catch { }
            return result;
        }

        public void Set<T>(string key, T value)
        {
            ApplicationDataContainer settings;
            if (policy == SettingsPolicy.Roaming)
                settings = ApplicationData.Current.RoamingSettings;
            else
                settings = ApplicationData.Current.LocalSettings;
            settings.Values[key] = value;
        }

        public void Remove(string key)
        {
            ApplicationDataContainer settings;
            if (policy == SettingsPolicy.Roaming)
                settings = ApplicationData.Current.RoamingSettings;
            else
                settings = ApplicationData.Current.LocalSettings;
            settings.Values.Remove(key);
        }
    }
}
