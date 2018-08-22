
namespace UIUtilities
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;

    internal abstract class AsyncTaskRunnerBase<TReturn>
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

        public Task TaskCompletion { get; }


        internal NotifyTaskCompletion<TReturn> _notifyTaskCompletion;

        public abstract void StartTask();

        public abstract void CancelTask();

        private void NotifyTaskCompletionOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
