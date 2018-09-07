
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using API;
    using API.AsyncCommands;
    using Utilities.API;

    public class AsyncSimpleCommand : IAsyncCommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => _asyncCommandBase.CanExecuteChanged += value;
            remove =>  _asyncCommandBase.CanExecuteChanged -= value;
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => _asyncCommandBase.PropertyChanged += value;
            remove => _asyncCommandBase.PropertyChanged -= value;
        }

        public INotifyTaskCompletion<object> Execution => _asyncCommandBase.Execution;

        private readonly Func<Task> _command;
        private readonly INotifyTaskCompletion<object> _notifyTaskCompletion;
        private readonly ITaskWrapper _taskWrapper;
        private readonly IAsyncCommandBase<object> _asyncCommandBase;

        public AsyncSimpleCommand(IAsyncCommandBase<object> asyncCommandBase, Func<Task> command, INotifyTaskCompletion<object> notifyTaskCompletion, ITaskWrapper taskWrapper)
        {
            _command = command;
            _notifyTaskCompletion = notifyTaskCompletion;
            _taskWrapper = taskWrapper;
            _asyncCommandBase = asyncCommandBase;
        }

        public bool CanExecute(object parameter)
        {
            return _asyncCommandBase.CanExecute(parameter);
        }

        public async Task ExecuteAsync(object parameter)
        {
            var wrappedTask =_taskWrapper.WrapTaskWithNullReturnValue(_command);

            await _asyncCommandBase.ExecuteAsync(parameter, wrappedTask, _notifyTaskCompletion);
        }

        public void Dispose()
        {
            _asyncCommandBase.Dispose();
        }
    }
}
