
namespace Utilities.Implementation
{
    using System;
    using System.Threading.Tasks;
    using API;

    public class TaskWrapper : ITaskWrapper
    {
        public Func<Task<object>> WrapTaskWithNullReturnValue(Func<Task> funcTask)
        {
           return async () =>
            {
                await funcTask().ConfigureAwait(false);
                return null;
            };
        }

        public Func<Task<object>> WrapTaskWithNullReturnValue<TIn>(Func<TIn, Task> funcTask, TIn param)
        {
            return async () =>
            {
                await funcTask(param).ConfigureAwait(false);
                return null;
            };
        }
    }
}
