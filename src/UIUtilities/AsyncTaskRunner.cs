
namespace UIUtilities
{
    using System;
    using System.Threading.Tasks;
    using API;

    public class AsyncTaskRunner : AsyncTaskRunnerBase<object>, IAsyncTaskRunner
    {
        public Task Task => NotifyTaskCompletion.Task;


        private readonly Func<Task> _taskFunc;

        private readonly INotifyTaskCompletionFactory _notifyTaskCompletionFactory;

        public AsyncTaskRunner(Func<Task> taskFunc, INotifyTaskCompletionFactory notifyTaskCompletionFactory)
        {
            _taskFunc = taskFunc;
            _notifyTaskCompletionFactory = notifyTaskCompletionFactory;
        }

        public override void StartTask()
        {
            if (NotifyTaskCompletion != null)
            {
                NotifyTaskCompletion.PropertyChanged -= NotifyTaskCompletionOnPropertyChanged;
            }

            NotifyTaskCompletion = _notifyTaskCompletionFactory.Create<object>();
            NotifyTaskCompletion.PropertyChanged += NotifyTaskCompletionOnPropertyChanged;
            NotifyTaskCompletion.Start(WrapTaskWithReturnValue);


            HasStarted = true;
        }

        public override void CancelTask()
        {
            throw new NotImplementedException();
        }

        private async Task<object> WrapTaskWithReturnValue()
        {
            await _taskFunc().ConfigureAwait(false);
            return null;
        }
    }

    public class AsyncTaskRunner<TReturn> : AsyncTaskRunnerBase<TReturn>, IAsyncTaskRunner<TReturn>
    {
        public Task<TReturn> Task => NotifyTaskCompletion.Task;

        public TReturn Result => NotifyTaskCompletion.Result;


        private readonly Func<Task<TReturn>> _taskFunc;

        private readonly INotifyTaskCompletionFactory _notifyTaskCompletionFactory;

        public AsyncTaskRunner(Func<Task<TReturn>> taskFunc, INotifyTaskCompletionFactory notifyTaskCompletionFactory)
        {
            _taskFunc = taskFunc;
            _notifyTaskCompletionFactory = notifyTaskCompletionFactory;
        }

        public override void StartTask()
        {
            if (NotifyTaskCompletion != null)
            {
                NotifyTaskCompletion.PropertyChanged -= NotifyTaskCompletionOnPropertyChanged;
            }

            NotifyTaskCompletion = _notifyTaskCompletionFactory.Create<TReturn>();
            NotifyTaskCompletion.PropertyChanged += NotifyTaskCompletionOnPropertyChanged;
            NotifyTaskCompletion.Start(_taskFunc);

            HasStarted = true;
        }

        public override void CancelTask()
        {
            throw new NotImplementedException();
        }
    }
}
