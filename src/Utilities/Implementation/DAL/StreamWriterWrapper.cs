
namespace Utilities.Implementation.DAL
{
    using System.IO;
    using API.DAL;

    public class StreamWriterWrapper : IStreamWriterWrapper
    {
        private readonly StreamWriter _streamWriter;

        public StreamWriterWrapper(string path)
        {
            _streamWriter = new StreamWriter(path);
        }

        public StreamWriterWrapper(string path, bool append)
        {
            _streamWriter = new StreamWriter(path, append);
        }

        public void Flush()
        {
            _streamWriter.Flush();
        }

        public void Write(string data)
        {
            _streamWriter.Write(data);
        }

        public void Dispose()
        {
            _streamWriter?.Dispose();
        }
    }
}
