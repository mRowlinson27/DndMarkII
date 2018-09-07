
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using API;
    using API.AsyncCommands;

    public class AsyncCommandWatcher<TResult> : IAsyncCommandWatcher<TResult>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler CanExecuteChanged;

        public INotifyTaskCompletion<TResult> Execution
        {
            get => _execution;
            set
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

        public async Task ExecuteAsync(object parameter, Func<Task<TResult>> command, INotifyTaskCompletion<TResult> execution)
        {
            Execution = execution;

            Execution.Start(command);

            await Execution.TaskCompletion.ConfigureAwait(false);
        }

        public bool CanExecute(object parameter)
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

        private void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            if (Execution != null)
            {
                Execution.PropertyChanged -= ExecutionOnPropertyChanged;
            }
        }
    }
}
