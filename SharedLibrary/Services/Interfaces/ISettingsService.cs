using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Services.Interfaces
{
    public enum SettingsPolicy
    {
        Local = 0,
        Roaming,
    }

    public interface ISettingsService
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);
        void Remove(string key);
    }
}
