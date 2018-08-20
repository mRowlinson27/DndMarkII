
namespace DndMarkII.AsyncCommands
{
    using System;
    using System.Threading.Tasks;

    public class AsyncSimpleCommand : AsyncCommandBase
    {
        private readonly Func<Task> _command;

        public AsyncSimpleCommand(Func<Task> command)
        {
            _command = command;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            Task<object> wrappedTask = WrapTaskWithReturnValue();

            Execution = new NotifyTaskCompletion<object>(wrappedTask);

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
