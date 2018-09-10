
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.Threading.Tasks;
    using API;
    using API.AsyncCommands;
    using Utilities.API;

    public class AsyncCommandWithInput<TIn> : AsyncCommandBase<object>, IAsyncCommand
    {
        private readonly IAsyncCommandWatcher<object> _asyncCommandWatcher;
        private readonly Func<TIn, Task> _command;
        private readonly INotifyTaskCompletion<object> _notifyTaskCompletion;
        private readonly ITaskWrapper _taskWrapper;

        public AsyncCommandWithInput(IAsyncCommandWatcher<object> asyncCommandWatcher, Func<TIn, Task> command, INotifyTaskCompletion<object> notifyTaskCompletion, ITaskWrapper taskWrapper)
            : base(asyncCommandWatcher)
        {
            _asyncCommandWatcher = asyncCommandWatcher;
            _command = command;
            _notifyTaskCompletion = notifyTaskCompletion;
            _taskWrapper = taskWrapper;
        }

        public async Task ExecuteAsync(object parameter)
        {
            var wrappedTask = _taskWrapper.WrapTaskWithNullReturnValue(_command, (TIn) parameter);

            await _asyncCommandWatcher.ExecuteAsync(parameter, wrappedTask, _notifyTaskCompletion);
        }

        public override void Execute(object parameter) => ExecuteAsync(parameter);
    }
}