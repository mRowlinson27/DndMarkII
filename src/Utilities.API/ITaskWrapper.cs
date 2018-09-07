namespace Utilities.API
{
    using System;
    using System.Threading.Tasks;

    public interface ITaskWrapper
    {
        Func<Task<object>> WrapTaskWithNullReturnValue(Func<Task> funcTask);
    }
}