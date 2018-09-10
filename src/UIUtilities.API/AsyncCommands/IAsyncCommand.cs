
namespace UIUtilities.API.AsyncCommands
{
    using System.Threading.Tasks;

    public interface IAsyncCommand : IWatchableCommandProperties<object>
    {
        Task ExecuteAsync(object parameter);

        void Execute(object parameter);
    }

    public interface IAsyncCommand<TResult> : IWatchableCommandProperties<TResult>
    {
        Task<TResult> ExecuteAsync(object parameter);

        void Execute(object parameter);
    }
}
