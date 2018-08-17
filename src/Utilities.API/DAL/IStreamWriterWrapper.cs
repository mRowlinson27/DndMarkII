
namespace Utilities.API.DAL
{
    using System;

    public interface IStreamWriterWrapper : IDisposable
    {
        void Flush();

        void Write(string data);
    }
}
