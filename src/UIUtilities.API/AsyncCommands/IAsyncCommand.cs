
namespace UIUtilities.API.AsyncCommands
{
    using System.Threading.Tasks;

    public interface IAsyncCommand : IAsyncCommand<object> {}

    public interface IAsyncCommand<TResult> : IWatchableCommandProperties<TResult>
    {
        Task ExecuteAsync(object parameter);
    }
}
