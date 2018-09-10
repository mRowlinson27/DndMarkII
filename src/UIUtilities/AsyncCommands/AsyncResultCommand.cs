
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.Threading.Tasks;
    using API;
    using API.AsyncCommands;

    public class AsyncResultCommand<TResult>: AsyncCommandBase<TResult>, IAsyncCommand<TResult>
    {
        private readonly Func<Task<TResult>> _command;
        private readonly INotifyTaskCompletion<TResult> _notifyTaskCompletion;
        private readonly IAsyncCommandWatcher<TResult> _asyncCommandWatcher;

        public AsyncResultCommand(IAsyncCommandWatcher<TResult> asyncCommandWatcher, Func<Task<TResult>> command, INotifyTaskCompletion<TResult> notifyTaskCompletion)
            : base(asyncCommandWatcher)
        {
            _command = command;
            _notifyTaskCompletion = notifyTaskCompletion;
            _asyncCommandWatcher = asyncCommandWatcher;
        }

        public async Task<TResult> ExecuteAsync(object parameter)
        {
            return await _asyncCommandWatcher.ExecuteAsync(parameter, _command, _notifyTaskCompletion);
        }

        public override void Execute(object parameter) => ExecuteAsync(parameter);
    }
}
