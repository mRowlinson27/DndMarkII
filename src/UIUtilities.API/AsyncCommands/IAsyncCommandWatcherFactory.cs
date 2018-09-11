
namespace UIUtilities.API.AsyncCommands
{
    public interface IAsyncCommandWatcherFactory
    {
        IAsyncCommandWatcher<T> Create<T>();

        IAsyncCommandWatcher<T> CreateWithContext<T>();
    }
}
