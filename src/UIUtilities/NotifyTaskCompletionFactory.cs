
namespace UIUtilities
{
    using API;
    using Utilities.API;

    public class NotifyTaskCompletionFactory : INotifyTaskCompletionFactory
    {
        private readonly ILogger _logger;

        public NotifyTaskCompletionFactory(ILogger logger)
        {
            _logger = logger;
        }

        public INotifyTaskCompletion<TResult> Create<TResult>()
        {
            return new NotifyTaskCompletion<TResult>(_logger);
        }
    }
}
