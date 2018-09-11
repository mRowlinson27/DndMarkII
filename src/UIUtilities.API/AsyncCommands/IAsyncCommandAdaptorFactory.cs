
namespace UIUtilities.API.AsyncCommands
{
    using System;
    using System.Threading.Tasks;

    public interface IAsyncCommandAdaptorFactory
    {
        IAsyncCommandAdaptor Create(Func<Task> command);

        IAsyncCommandAdaptor Create(Action action);

        IAsyncCommandAdaptor CreateWithContext(Func<Task> command);

        IAsyncCommandAdaptor CreateWithContext(Action action);
    }
}
