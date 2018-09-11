
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

        public IAsyncCommand Create(Func<Task> command)
        {
            return new AsyncSimpleCommand(_asyncCommandWatcherFactory.Create<object>(), _taskWrapper.WrapTaskWithNullReturnValue(command), _notifyTaskCompletionFactory.Create<object>());
        }

        public IAsyncCommand Create(Action action)
        {
            return Create(_taskWrapper.WrapActionWithNullReturnValue(action));
        }

        public IAsyncCommand CreateWithContext(Func<Task> command)
        {
            return new AsyncSimpleCommand(_asyncCommandWatcherFactory.CreateWithContext<object>(), _taskWrapper.WrapTaskWithNullReturnValue(command), _notifyTaskCompletionFactory.Create<object>());
        }

        public IAsyncCommand CreateWithContext(Action action)
        {
            return CreateWithContext(_taskWrapper.WrapActionWithNullReturnValue(action));
        }

        public IAsyncCommand Create<TIn>(Func<TIn, Task> command)
        {
            throw new NotImplementedException();
        }

        public IAsyncCommand<TResult> CreateResultCommand<TResult>(Func<TResult> command)
        {
            return new AsyncResultCommand<TResult>(_asyncCommandWatcherFactory.Create<TResult>(), _taskWrapper.WrapActionWithNullReturnValue(command), _notifyTaskCompletionFactory.Create<TResult>());
        }
    }
}