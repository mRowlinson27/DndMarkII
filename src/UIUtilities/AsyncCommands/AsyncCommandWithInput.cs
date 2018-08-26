
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.Threading.Tasks;
    using API;
    using UIUtilities;

    public class AsyncCommandWithInput<TIn> : AsyncCommandBase
    {
        private readonly Func<TIn, Task> _command;

        private readonly INotifyTaskCompletionFactory _notifyTaskCompletionFactory;

        public AsyncCommandWithInput(Func<TIn, Task> command, INotifyTaskCompletionFactory notifyTaskCompletionFactory)
        {
            _command = command;
            _notifyTaskCompletionFactory = notifyTaskCompletionFactory;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            Task<object> wrappedTask = WrapTaskWithReturnValue((TIn) parameter);
            Execution = _notifyTaskCompletionFactory.Create(wrappedTask);

            RaiseCanExecuteChanged();

            await Execution.TaskCompletion;

            RaiseCanExecuteChanged();
        }

        private async Task<object> WrapTaskWithReturnValue(TIn param)
        {
            await _command(param);
            return null;
        }
    }
}