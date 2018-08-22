
namespace UIUtilities
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using API;

    public class AsyncTaskRunner : IAsyncTaskRunner
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual Task Task => _notifyTaskCompletion.Task;

        public TaskStatus Status { get; }

        public bool HasStarted { get; }

        public bool IsCompleted { get; }

        public bool IsNotCompleted { get; }

        public bool IsSuccessfullyCompleted { get; }

        public bool IsCanceled { get; }

        public bool IsFaulted { get; }

        public AggregateException Exception { get; }

        public Exception InnerException { get; }

        public string ErrorMessage { get; }


        private readonly Func<Task> _taskFunc;

        private NotifyTaskCompletion<object> _notifyTaskCompletion;

        public AsyncTaskRunner(Func<Task> taskFunc)
        {
            _taskFunc = taskFunc;
        }

        public virtual void StartTask()
        {
            if (_notifyTaskCompletion != null)
            {
                _notifyTaskCompletion.PropertyChanged -= NotifyTaskCompletionOnPropertyChanged;
            }

            _notifyTaskCompletion = new NotifyTaskCompletion<object>(WrapTaskWithReturnValue());
            _notifyTaskCompletion.PropertyChanged += NotifyTaskCompletionOnPropertyChanged;
        }

        public void CancelTask()
        {
            throw new NotImplementedException();
        }

        private async Task<object> WrapTaskWithReturnValue()
        {
            await _taskFunc();
            return null;
        }

        private void NotifyTaskCompletionOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }

    public class AsyncTaskRunner<TReturn> : IAsyncTaskRunner<TReturn>
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public TReturn Result => _notifyTaskCompletion.Result;

        public Task<TReturn> Task { get; }

        public TaskStatus Status { get; }

        public bool HasStarted { get; }

        public bool IsCompleted { get; }

        public bool IsNotCompleted { get; }

        public bool IsSuccessfullyCompleted { get; }

        public bool IsCanceled { get; }

        public bool IsFaulted { get; }

        public AggregateException Exception { get; }

        public Exception InnerException { get; }

        public string ErrorMessage { get; }

        private readonly Func<Task<TReturn>> _taskFunc;

        private NotifyTaskCompletion<TReturn> _notifyTaskCompletion;

        public AsyncTaskRunner(Func<Task<TReturn>> taskFunc)
        {
            _taskFunc = taskFunc;
        }

        public void StartTask()
        {
            if (_notifyTaskCompletion != null)
            {
                _notifyTaskCompletion.PropertyChanged -= NotifyTaskCompletionOnPropertyChanged;
            }

            _notifyTaskCompletion = new NotifyTaskCompletion<TReturn>(_taskFunc());
            _notifyTaskCompletion.PropertyChanged += NotifyTaskCompletionOnPropertyChanged;
        }

        public void CancelTask()
        {
            throw new NotImplementedException();
        }

        private void NotifyTaskCompletionOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
