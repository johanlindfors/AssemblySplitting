using SharedLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;

namespace Facades.Services
{
    public class DPAPIProtectionService : IProtectionService
    {
        public const BinaryStringEncoding Encoding = BinaryStringEncoding.Utf8;

        public async Task<byte[]> ProtectAsync(string data, string key)
        {
            var provider = new DataProtectionProvider(key);
            var buffMessage = CryptographicBuffer.ConvertStringToBinary(data, Encoding);
            var buffProtected = await provider.ProtectAsync(buffMessage);

            var buffer = new byte[buffProtected.Length];
            CryptographicBuffer.CopyToByteArray(buffProtected, out buffer);
            return buffer;
        }

        public async Task<string> UnprotectAsync(byte[] data, string key)
        {
            var provider = new DataProtectionProvider(key);
            var buffProtected = CryptographicBuffer.CreateFromByteArray(data);
            var buffUnprotected = await provider.UnprotectAsync(buffProtected);

            return CryptographicBuffer.ConvertBinaryToString(Encoding, buffUnprotected);
        }
    }
}
