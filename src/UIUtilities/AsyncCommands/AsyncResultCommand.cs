
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Neutronium.MVVMComponents;

    class AsyncResultCommand<TIn, TResult> : AsyncCommandBase, INotifyPropertyChanged, IResultCommand<TResult>
    {
        /*public NotifyTaskCompletion<TResult> Execution
        {
            get => _execution;
            private set
            {
                _execution = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Func<TIn, Task<TResult>> _command;

        private NotifyTaskCompletion<TResult> _execution;

        public AsyncCommandWithInput(Func<TIn, Task<TResult>> command)
        {
            _command = command;
        }

        public override bool CanExecute(object parameter)
        {
            return Execution == null || Execution.IsCompleted;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            Execution = new NotifyTaskCompletion<TResult>(_command((TIn)parameter));

            RaiseCanExecuteChanged();

            await Execution.TaskCompletion;

            RaiseCanExecuteChanged();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Task<TResult> Execute()
        {
            throw new NotImplementedException();
        }*/
        public override Task ExecuteAsync(object parameter)
        {
            throw new NotImplementedException();
        }

        public Task<TResult> Execute()
        {
            throw new NotImplementedException();
        }
    }
}
