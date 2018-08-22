
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using API.AsyncCommands;
    using UIUtilities;

    public abstract class AsyncCommandBase : IAsyncCommand, INotifyPropertyChanged
    {
        public abstract Task ExecuteAsync(object parameter);

        public virtual bool CanExecute(object parameter)
        {
            return Execution == null || Execution.IsCompleted;
        }

        public NotifyTaskCompletion<object> Execution
        {
            get => _execution;
            protected set
            {
                _execution = value;
                OnPropertyChanged();
            }
        }

        private NotifyTaskCompletion<object> _execution;

        public event PropertyChangedEventHandler PropertyChanged;

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        public event EventHandler CanExecuteChanged;
//        {
//            add => CommandManager.RequerySuggested += value;
//            remove => CommandManager.RequerySuggested -= value;
//        }

        protected void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
//            CommandManager.InvalidateRequerySuggested();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
