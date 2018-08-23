
namespace UIUtilities
{
    using System;
    using System.Threading.Tasks;
    using API;

    public class AsyncTaskRunnerFactory : IAsyncTaskRunnerFactory
    {
        private readonly INotifyTaskCompletionFactory _notifyTaskCompletionFactory;

        public AsyncTaskRunnerFactory(INotifyTaskCompletionFactory notifyTaskCompletionFactory)
        {
            _notifyTaskCompletionFactory = notifyTaskCompletionFactory;
        }

        public IAsyncTaskRunner Create(Func<Task> taskFunc)
        {
            return new AsyncTaskRunner(taskFunc, _notifyTaskCompletionFactory);
        }

        public IAsyncTaskRunner<TResult> Create<TResult>(Func<Task<TResult>> taskFuncWithReturnValue)
        {
            return new AsyncTaskRunner<TResult>(taskFuncWithReturnValue, _notifyTaskCompletionFactory);
        }
    }
}
