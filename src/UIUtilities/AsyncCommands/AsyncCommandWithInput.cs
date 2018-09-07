
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.Threading.Tasks;
    using API;

    public class AsyncCommandWithInput<TIn> : AsyncCommandBase
    {
        private readonly Func<TIn, Task> _command;

        private readonly INotifyTaskCompletionFactory _notifyTaskCompletionFactory;

        public AsyncCommandWithInput(Func<TIn, Task> command, INotifyTaskCompletionFactory notifyTaskCompletionFactory, IUiStateController stateController) : base(stateController)
        {
            _command = command;
            _notifyTaskCompletionFactory = notifyTaskCompletionFactory;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Task<object> wrappedTask = WrapTaskWithReturnValue((TIn) parameter);

            Execution = _notifyTaskCompletionFactory.Create<object>();
//            Execution.Start(wrappedTask);

            RaiseCanExecuteChanged();

            if (Execution != null)
            {
                await Execution.TaskCompletion.ConfigureAwait(false);
            }

            RaiseCanExecuteChanged();
        }

        private async Task<object> WrapTaskWithReturnValue(TIn param)
        {
            await _command(param).ConfigureAwait(false);
            return null;
        }
    }
}