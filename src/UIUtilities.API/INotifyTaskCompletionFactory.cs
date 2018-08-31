
namespace UIUtilities.API
{
    public interface INotifyTaskCompletionFactory
    {
        INotifyTaskCompletion<TResult> Create<TResult>();
    }
}
