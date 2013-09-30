using System.IO;

namespace SharedLibrary.Services.Interfaces
{
    public interface ISerializerService
    {
        void Serialize<T>(T obj, Stream stream);
        T Deserialize<T>(Stream stream);
    }
}
