
namespace UIUtilities.API
{
    using System.Threading.Tasks;

    public interface INotifyTaskCompletionFactory
    {
        INotifyTaskCompletion<TResult> Create<TResult>(Task<TResult> task);
    }
}
