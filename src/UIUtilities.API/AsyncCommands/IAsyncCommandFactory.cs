
namespace UIUtilities.API.AsyncCommands
{
    using System;
    using System.Threading.Tasks;

    public interface IAsyncCommandFactory
    {
        IAsyncCommandAdaptor Create(Func<Task> command);

        IAsyncCommand Create<TIn>(Func<TIn, Task> command);

        IAsyncCommandAdaptor Create(Action execute);
    }
}
