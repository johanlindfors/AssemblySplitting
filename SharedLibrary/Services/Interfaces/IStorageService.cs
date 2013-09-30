using System.Threading.Tasks;

namespace SharedLibrary.Services.Interfaces
{
    public interface IStorageService
    {
        Task Write<T>(T obj, string filename);
        Task<T> Read<T>(string filename);
    }
}
