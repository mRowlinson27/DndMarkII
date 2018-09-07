
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
                await funcTask();
                return null;
            };
        }
    }
}
