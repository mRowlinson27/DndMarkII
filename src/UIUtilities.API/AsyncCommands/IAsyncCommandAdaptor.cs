
namespace UIUtilities.API.AsyncCommands
{
    using System;
    using System.Windows.Input;

    public interface IAsyncCommandAdaptor : ICommand, IDisposable
    {
        bool ShouldExecute { get; set; }
    }
}
