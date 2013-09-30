using SharedLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Facades.Services
{
    public class LocalFolderStorageService : IStorageService
    {
        ISerializerService serializer;

        public LocalFolderStorageService(ISerializerService serializer)
        {
            this.serializer = serializer;
        }

        public async Task Write<T>(T obj, string filename)
        {
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var storageFile = await localFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            using (var writeStream = await storageFile.OpenStreamForWriteAsync())
            {
                serializer.Serialize<T>(obj, writeStream);
            }
        }

        public async Task<T> Read<T>(string filename)
        {
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            using(var readStream = await localFolder.OpenStreamForReadAsync(filename))
            {
                return serializer.Deserialize<T>(readStream);
            }
        }
    }
}
