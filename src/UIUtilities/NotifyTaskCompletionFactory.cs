
namespace UIUtilities
{
    using System.Threading.Tasks;
    using API;
    using Utilities.API;

    public class NotifyTaskCompletionFactory : INotifyTaskCompletionFactory
    {
        private readonly ILogger _logger;

        public NotifyTaskCompletionFactory(ILogger logger)
        {
            _logger = logger;
        }

        public INotifyTaskCompletion<TResult> Create<TResult>(Task<TResult> task)
        {
            return new NotifyTaskCompletion<TResult>(task, _logger);
        }
    }
}
