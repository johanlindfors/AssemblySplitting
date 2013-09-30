using SharedLibrary.Services.Interfaces;
using System;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

namespace Facades.Services
{
    public class IsolatedStorageService : IStorageService
    {
        ISerializerService serializer;
        private object s_lock = new object();

        public IsolatedStorageService(ISerializerService serializer)
        {
            this.serializer = serializer;
        }

        public Task Write<T>(T obj, string filename)
        {
            return Task.Run(() =>
            {
                if (obj == null)
                    return;

                try
                {
                    using (var userStore = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        lock (s_lock)
                        {
                            using (var fileStream = new IsolatedStorageFileStream(filename, System.IO.FileMode.OpenOrCreate, userStore))
                            {
                                serializer.Serialize(obj, fileStream);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Debug.WriteLine("Failed to write '{0}'", filename);
                }
            });
        }

        public Task<T> Read<T>(string filename)
        {
            T result = default(T);

            try
            {
                using (var userStore = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (userStore.FileExists(filename))
                    {
                        lock (s_lock)
                        {
                            using (var fileStream = new IsolatedStorageFileStream(filename, System.IO.FileMode.Open, userStore))
                            {
                                result = serializer.Deserialize<T>(fileStream);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to read file: {0} as <{1}>", filename, typeof(T));
            }

            return Task.FromResult<T>(result);
        }
    }
}
