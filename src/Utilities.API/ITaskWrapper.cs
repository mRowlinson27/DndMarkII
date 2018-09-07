namespace Utilities.API
{
    using System;
    using System.Threading.Tasks;

    public interface ITaskWrapper
    {
        Func<Task<object>> WrapTaskWithNullReturnValue(Func<Task> funcTask);

        Func<Task<object>> WrapTaskWithNullReturnValue<TIn>(Func<TIn, Task> funcTask, TIn param);

        Func<Task<object>> WrapActionWithNullReturnValue(Action action);
    }
}