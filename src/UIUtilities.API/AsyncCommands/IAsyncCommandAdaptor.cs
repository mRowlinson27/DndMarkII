
namespace UIUtilities.API.AsyncCommands
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public interface IAsyncCommandAdaptor : ICommand, IDisposable
    {
        Task ExecuteAsync();
    }
}
