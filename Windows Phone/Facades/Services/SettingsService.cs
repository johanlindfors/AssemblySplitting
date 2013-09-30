using SharedLibrary.Services.Interfaces;
using System.IO.IsolatedStorage;

namespace Facades.Services
{
    public class SettingsService : ISettingsService
    {
        public T Get<T>(string key)
        {
            var result = default(T);
            try
            {
                var settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains(key))
                {
                    result = (T)settings[key];
                }
            }
            catch { }
            return result;
        }

        public void Set<T>(string key, T value)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings[key] = value;
            settings.Save();
        }

        public void Remove(string key)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(key))
            {
                settings.Remove(key);
                settings.Save();
            }
        }
    }
}
