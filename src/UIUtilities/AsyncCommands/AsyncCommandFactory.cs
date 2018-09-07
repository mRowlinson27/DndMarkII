
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using API;
    using API.AsyncCommands;
    using Utilities.API;

    public class AsyncCommandFactory : IAsyncCommandFactory
    {
        private readonly INotifyTaskCompletionFactory _notifyTaskCompletionFactory;
        private readonly IUiStateController _stateController;
        private readonly ITaskWrapper _taskWrapper;

        public AsyncCommandFactory(INotifyTaskCompletionFactory notifyTaskCompletionFactory, IUiStateController stateController, ITaskWrapper taskWrapper)
        {
            _notifyTaskCompletionFactory = notifyTaskCompletionFactory;
            _stateController = stateController;
            _taskWrapper = taskWrapper;
        }

        public IAsyncCommand Create(Func<Task> command)
        {
            return new AsyncSimpleCommand(new AsyncCommandWatcher<object>(), command, _notifyTaskCompletionFactory.Create<object>(), _taskWrapper);
        }

        public IAsyncCommand Create<TIn>(Func<TIn, Task> command)
        {
            return new AsyncCommandWithInput<TIn>(new AsyncCommandWatcher<object>(), command, _notifyTaskCompletionFactory.Create<object>(), _taskWrapper);
        }

        public IAsyncCommandAdaptor CreateAdaptor(Action execute)
        {
            return new AsyncSimpleCommandAdaptor(execute);
        }

//        public static AsyncSimpleCommand<object> Create(Func<CancellationToken, Task> command)
//        {
//            return new AsyncSimpleCommand<object>(async token => { await command(token); return null; });
//        }
//
//        public static AsyncSimpleCommand<TResult> Create<TResult>(Func<CancellationToken, Task<TResult>> command)
//        {
//            return new AsyncSimpleCommand<TResult>(command);
//        }
    }
}