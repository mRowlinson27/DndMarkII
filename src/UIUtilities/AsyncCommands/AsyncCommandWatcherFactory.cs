
namespace UIUtilities.AsyncCommands
{
    using API;
    using API.AsyncCommands;

    public class AsyncCommandWatcherFactory : IAsyncCommandWatcherFactory
    {
        private readonly IUiStateController _stateController;

        public AsyncCommandWatcherFactory(IUiStateController stateController)
        {
            _stateController = stateController;
        }

        public IAsyncCommandWatcher<T> Create<T>()
        {
            return new AsyncCommandWatcher<T>();
        }

        public IAsyncCommandWatcher<T> CreateWithContext<T>()
        {
            return new AsyncCommandWatcherContextDecorator<T>(Create<T>(), _stateController);
        }
    }
}
