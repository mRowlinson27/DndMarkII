
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using API;

    public class AsyncCommandBase2<TResult> : IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler CanExecuteChanged;

        public INotifyTaskCompletion<TResult> Execution
        {
            get => _execution;
            private set
            {
                if (_execution != null)
                {
                    _execution.PropertyChanged -= ExecutionOnPropertyChanged;
                }

                _execution = value;
                _execution.PropertyChanged += ExecutionOnPropertyChanged;
                OnPropertyChanged();
            }
        }
        private INotifyTaskCompletion<TResult> _execution;

        public virtual async Task ExecuteAsync(object parameter, Task<TResult> command, INotifyTaskCompletion<TResult> execution)
        {
            Execution = execution;

            Execution.Start(command);

            await Execution.TaskCompletion.ConfigureAwait(false);
        }

        public virtual bool CanExecute(object parameter)
        {
            return Execution == null || Execution.IsCompleted;
        }

        private void ExecutionOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsCompleted")
            {
                RaiseCanExecuteChanged();
            }
        }

        protected void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            Execution.PropertyChanged -= ExecutionOnPropertyChanged;
        }
    }
}
