
namespace Utilities.Implementation.DAL
{
    using API.DAL;

    public class FileWriter : IFileWriter
    {
        private IStreamWriterWrapperFactory _streamWriterWrapperFactory;

        public FileWriter(IStreamWriterWrapperFactory streamWriterWrapperFactory)
        {
            _streamWriterWrapperFactory = streamWriterWrapperFactory;
        }

        public void Write(string fullPath, string data)
        {
            using (var streamWriter = _streamWriterWrapperFactory.Create(fullPath))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
            }
        }
    }
}