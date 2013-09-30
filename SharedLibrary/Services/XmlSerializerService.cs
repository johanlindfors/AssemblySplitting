using SharedLibrary.Services.Interfaces;
using System.IO;
using System.Xml.Serialization;

namespace SharedLibrary.Services
{
    public class XmlSerializerService : ISerializerService
    {
        public void Serialize<T>(T obj, Stream stream)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, obj);
            }
            catch { throw; }
        }

        public T Deserialize<T>(Stream stream)
        {
            T result;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                result = (T)serializer.Deserialize(stream);
            }
            catch { throw; }
            return result;
        }
    }
}
