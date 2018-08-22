
namespace UIUtilities
{
    using System;
    using System.Threading.Tasks;
    using API;

    public class AsyncTaskRunnerFactory : IAsyncTaskRunnerFactory
    {
        public IAsyncTaskRunner Create(Func<Task> taskFunc)
        {
            return new AsyncTaskRunner(taskFunc);
        }

        public IAsyncTaskRunner<TResult> Create<TResult>(Func<Task<TResult>> taskFuncWithReturnValue)
        {
            return new AsyncTaskRunner<TResult>(taskFuncWithReturnValue);
        }
    }
}
