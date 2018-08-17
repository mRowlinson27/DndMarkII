
namespace Utilities.Implementation.DAL
{
    using API.DAL;

    public class StreamWriterWrapperFactory : IStreamWriterWrapperFactory
    {
        public IStreamWriterWrapper Create(string path)
        {
            return new StreamWriterWrapper(path, true);
        }
    }
}
