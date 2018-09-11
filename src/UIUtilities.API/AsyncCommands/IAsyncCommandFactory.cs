
namespace UIUtilities.API.AsyncCommands
{
    using System;
    using System.Threading.Tasks;

    public interface IAsyncCommandFactory
    {
        IAsyncCommand Create(Func<Task> command);

        IAsyncCommand Create(Action action);

        IAsyncCommand CreateWithContext(Func<Task> command);

        IAsyncCommand CreateWithContext(Action action);

        IAsyncCommand<TResult> CreateResultCommand<TResult>(Func<TResult> command);
    }
}
