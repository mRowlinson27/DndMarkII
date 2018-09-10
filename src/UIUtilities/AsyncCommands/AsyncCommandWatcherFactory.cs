
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
            return new AsyncCommandWatcherContextDecorator<T>(new AsyncCommandWatcher<T>(), _stateController);
        }
    }
}
