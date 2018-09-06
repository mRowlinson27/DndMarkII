
namespace UIUtilities.API.AsyncCommands
{
    using System;
    using System.Threading.Tasks;

    public interface IAsyncCommandFactory
    {
        IAsyncCommand Create(Func<Task> command);

        IAsyncCommand Create<TIn>(Func<TIn, Task> command);

        IAsyncCommandAdaptor CreateAdaptor(Action execute);
    }
}
