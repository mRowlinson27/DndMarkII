
namespace Utilities.API.DAL
{
    using System.Threading.Tasks;

    public interface IJsonFile<T>
    {
        Task<T> ReadAsync();

        Task WriteAsync(T data);

        T Read();

        void Write(T data);
    }
}
