using SharedLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Facades.Services
{
    public class DPAPIProtectionService : IProtectionService
    {
        public Task<byte[]> ProtectAsync(string data, string key)
        {
            byte[] rawData = Encoding.UTF8.GetBytes(data);
            byte[] entropy = Encoding.UTF8.GetBytes(key);
            return Task.FromResult<byte[]>(ProtectedData.Protect(rawData, entropy));
        }

        public Task<string> UnprotectAsync(byte[] data, string key)
        {            
            byte[] entropy = Encoding.UTF8.GetBytes(key);
            var decryptedData = ProtectedData.Unprotect(data, entropy);
            return Task.FromResult<string>(Encoding.UTF8.GetString(decryptedData, 0, decryptedData.Length));
        }
    }
}
