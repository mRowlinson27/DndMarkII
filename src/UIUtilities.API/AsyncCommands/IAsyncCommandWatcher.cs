
namespace UIUtilities.API.AsyncCommands
{
    using System;
    using System.Threading.Tasks;

    public interface IAsyncCommandWatcher<TResult> : IWatchableCommandProperties<TResult>
    {
        Task<TResult> ExecuteAsync(object parameter, Func<Task<TResult>> command, INotifyTaskCompletion<TResult> execution);
    }
}
