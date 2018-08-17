
namespace Utilities.API.DAL
{
    public interface IStreamWriterWrapperFactory
    {
        IStreamWriterWrapper Create(string path);
    }
}
