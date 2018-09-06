
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using API;
    using API.AsyncCommands;

    public abstract class AsyncCommandBase : IAsyncCommand
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler CanExecuteChanged;

        public INotifyTaskCompletion<object> Execution
        {
            get => _execution;
            protected set
            {
                _execution = value;
                OnPropertyChanged();
            }
        }
        private INotifyTaskCompletion<object> _execution;

        private readonly IUiStateController _stateController;

        protected AsyncCommandBase(IUiStateController stateController)
        {
            _stateController = stateController;
            _stateController.UiLockUpdated += StateControllerOnUiLockUpdated;
        }

        public abstract Task ExecuteAsync(object parameter);

        public virtual bool CanExecute(object parameter)
        {
            return (Execution == null || Execution.IsCompleted) && !_stateController.UiLocked;
        }

        public async void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            using (_stateController.LockedContext())
            {
                RaiseCanExecuteChanged();

                await ExecuteAsync(parameter).ConfigureAwait(false);
            }

            RaiseCanExecuteChanged();
        }

        protected void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void StateControllerOnUiLockUpdated(object sender, EventArgs e)
        {
            RaiseCanExecuteChanged();
        }

        public void Dispose()
        {
            _stateController.UiLockUpdated -= StateControllerOnUiLockUpdated;
        }
    }
}
