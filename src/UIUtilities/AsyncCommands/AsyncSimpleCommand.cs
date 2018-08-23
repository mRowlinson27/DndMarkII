
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.Threading.Tasks;
    using API;
    using UIUtilities;

    public class AsyncSimpleCommand : AsyncCommandBase
    {
        private readonly Func<Task> _command;

        private readonly INotifyTaskCompletionFactory _notifyTaskCompletionFactory;

        public AsyncSimpleCommand(Func<Task> command, INotifyTaskCompletionFactory notifyTaskCompletionFactory)
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

            Task<object> wrappedTask = WrapTaskWithReturnValue();

            Execution = _notifyTaskCompletionFactory.Create(wrappedTask);

            RaiseCanExecuteChanged();

            if (Execution != null)
            {
                await Execution.TaskCompletion;
            }

            RaiseCanExecuteChanged();
        }

        private async Task<object> WrapTaskWithReturnValue()
        {
            await _command();
            return null;
        }
    }
}
