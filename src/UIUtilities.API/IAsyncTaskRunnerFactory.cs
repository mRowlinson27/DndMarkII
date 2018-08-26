
namespace UIUtilities.API
{
    using System;
    using System.Threading.Tasks;

    public interface IAsyncTaskRunnerFactory
    {
        IAsyncTaskRunner Create(Func<Task> taskFunc);
        IAsyncTaskRunner<TResult> Create<TResult>(Func<Task<TResult>> taskFuncWithReturnValue);
    }
}
