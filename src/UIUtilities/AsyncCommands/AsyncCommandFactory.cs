
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.Threading.Tasks;
    using API;
    using API.AsyncCommands;
    using Utilities.API;

    public class AsyncCommandFactory : IAsyncCommandFactory
    {
        private readonly INotifyTaskCompletionFactory _notifyTaskCompletionFactory;
        private readonly IAsyncCommandWatcherFactory _asyncCommandWatcherFactory;
        private readonly ITaskWrapper _taskWrapper;

        public AsyncCommandFactory(INotifyTaskCompletionFactory notifyTaskCompletionFactory, IAsyncCommandWatcherFactory asyncCommandWatcherFactory, ITaskWrapper taskWrapper)
        {
            _notifyTaskCompletionFactory = notifyTaskCompletionFactory;
            _asyncCommandWatcherFactory = asyncCommandWatcherFactory;
            _taskWrapper = taskWrapper;
        }

        public IAsyncCommandAdaptor Create(Func<Task> command)
        {
            var asyncCommand = new AsyncSimpleCommand(_asyncCommandWatcherFactory.Create<object>(), _taskWrapper.WrapTaskWithNullReturnValue(command), _notifyTaskCompletionFactory.Create<object>());
            return new AsyncSimpleCommandAdaptor(asyncCommand);
        }

        public IAsyncCommand Create<TIn>(Func<TIn, Task> command)
        {
            return new AsyncCommandWithInput<TIn>(_asyncCommandWatcherFactory.Create<object>(), command, _notifyTaskCompletionFactory.Create<object>(), _taskWrapper);
        }

        public IAsyncCommandAdaptor Create(Action command)
        {
            return Create(_taskWrapper.WrapActionWithNullReturnValue(command));
        }
    }
}