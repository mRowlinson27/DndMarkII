
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.Threading.Tasks;
    using API;
    using API.AsyncCommands;
    using Utilities.API;

    public class AsyncSimpleCommand : AsyncCommandBase<object>, IAsyncCommand
    {
        private readonly Func<Task<object>> _command;
        private readonly INotifyTaskCompletion<object> _notifyTaskCompletion;
        private readonly IAsyncCommandWatcher<object> _asyncCommandWatcher;

        public AsyncSimpleCommand(IAsyncCommandWatcher<object> asyncCommandWatcher, Func<Task<object>> command, INotifyTaskCompletion<object> notifyTaskCompletion)
            : base(asyncCommandWatcher)
        {
            _command = command;
            _notifyTaskCompletion = notifyTaskCompletion;
            _asyncCommandWatcher = asyncCommandWatcher;
        }

        public async Task ExecuteAsync(object parameter)
        {
            await _asyncCommandWatcher.ExecuteAsync(parameter, _command, _notifyTaskCompletion);
        }

        public override void Execute(object parameter) => ExecuteAsync(parameter);
    }
}
