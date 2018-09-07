
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using API;
    using API.AsyncCommands;
    using Utilities.API;

    public class AsyncSimpleCommand : AsyncCommandBase<object>, IAsyncCommand
    {
        private readonly Func<Task> _command;
        private readonly INotifyTaskCompletion<object> _notifyTaskCompletion;
        private readonly ITaskWrapper _taskWrapper;
        private readonly IAsyncCommandWatcher<object> _asyncCommandWatcher;

        public AsyncSimpleCommand(IAsyncCommandWatcher<object> asyncCommandWatcher, Func<Task> command, INotifyTaskCompletion<object> notifyTaskCompletion, ITaskWrapper taskWrapper)
            : base(asyncCommandWatcher)
        {
            _command = command;
            _notifyTaskCompletion = notifyTaskCompletion;
            _taskWrapper = taskWrapper;
            _asyncCommandWatcher = asyncCommandWatcher;
        }

        public async Task ExecuteAsync(object parameter)
        {
            var wrappedTask =_taskWrapper.WrapTaskWithNullReturnValue(_command);

            await _asyncCommandWatcher.ExecuteAsync(parameter, wrappedTask, _notifyTaskCompletion);
        }
    }
}
