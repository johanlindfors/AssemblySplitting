using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Services.Interfaces
{
    public interface IProtectionService
    {
        Task<byte[]> ProtectAsync(string data, string key);
        Task<string> UnprotectAsync(byte[] data, string key);
    }
}
