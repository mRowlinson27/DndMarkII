
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using API;
    using API.AsyncCommands;

    public class AsyncCommandWatcherContextDecorator<TResult> : IAsyncCommandWatcher<TResult>
    {
        public event PropertyChangedEventHandler PropertyChanged
        {
            add => _baseAsyncCommandWatcher.PropertyChanged += value;
            remove => _baseAsyncCommandWatcher.PropertyChanged -= value;
        }

        public event EventHandler CanExecuteChanged;

        public INotifyTaskCompletion<TResult> Execution => _baseAsyncCommandWatcher.Execution;

        private readonly IAsyncCommandWatcher<TResult> _baseAsyncCommandWatcher;
        private readonly IUiStateController _context;

        public AsyncCommandWatcherContextDecorator(IAsyncCommandWatcher<TResult> baseAsyncCommandWatcher, IUiStateController context)
        {
            _baseAsyncCommandWatcher = baseAsyncCommandWatcher;
            _context = context;

            _baseAsyncCommandWatcher.CanExecuteChanged += BaseAsyncCommandWatcherOnCanExecuteChanged;
            _context.UiLockUpdated += ContextOnUiLockUpdated;
        }

        public bool CanExecute(object parameter)
        {
            return _baseAsyncCommandWatcher.CanExecute(parameter) && !_context.UiLocked;
        }

        public async Task ExecuteAsync(object parameter, Func<Task<TResult>> command, INotifyTaskCompletion<TResult> execution)
        {
            using (_context.LockedContext())
            {
                await _baseAsyncCommandWatcher.ExecuteAsync(parameter, command, execution);
            }
        }

        private void BaseAsyncCommandWatcherOnCanExecuteChanged(object sender, EventArgs e)
        {
            if (!_context.UiLocked)
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void ContextOnUiLockUpdated(object sender, EventArgs e)
        {
            if (_baseAsyncCommandWatcher.CanExecute(null))
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Dispose()
        {
            _baseAsyncCommandWatcher.CanExecuteChanged -= BaseAsyncCommandWatcherOnCanExecuteChanged;
            _context.UiLockUpdated -= ContextOnUiLockUpdated;
            _baseAsyncCommandWatcher.Dispose();
        }
    }
}
