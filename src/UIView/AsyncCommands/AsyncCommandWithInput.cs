
namespace UIView.AsyncCommands
{
    using System;
    using System.Threading.Tasks;

    public class AsyncCommandWithInput<TIn> : AsyncCommandBase
    {
        private readonly Func<TIn, Task> _command;

        public AsyncCommandWithInput(Func<TIn, Task> command)
        {
            _command = command;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            Task<object> wrappedTask = WrapTaskWithReturnValue((TIn) parameter);
            Execution = new NotifyTaskCompletion<object>(wrappedTask);

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