
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.ComponentModel;
    using API;
    using API.AsyncCommands;

    public abstract class AsyncCommandBase<TResult> : IWatchableCommandProperties<TResult>
    {
        public event EventHandler CanExecuteChanged
        {
            add => _asyncCommandWatcher.CanExecuteChanged += value;
            remove => _asyncCommandWatcher.CanExecuteChanged -= value;
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => _asyncCommandWatcher.PropertyChanged += value;
            remove => _asyncCommandWatcher.PropertyChanged -= value;
        }

        public INotifyTaskCompletion<TResult> Execution => _asyncCommandWatcher.Execution;

        private readonly IAsyncCommandWatcher<TResult> _asyncCommandWatcher;

        protected AsyncCommandBase(IAsyncCommandWatcher<TResult> asyncCommandWatcher)
        {
            _asyncCommandWatcher = asyncCommandWatcher;
        }

        public virtual bool CanExecute(object parameter)
        {
            return _asyncCommandWatcher.CanExecute(parameter);
        }

        public abstract void Execute(object parameter);

        public void Dispose()
        {
            _asyncCommandWatcher.Dispose();
        }
    }
}
