
namespace UIUtilities
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using API;

    public abstract class AsyncTaskRunnerBase<TResult> : IAsyncTaskRunnerBase<TResult>
    {
        public bool HasStarted { get; internal set; }

        public event PropertyChangedEventHandler PropertyChanged;


        public TaskStatus Status => NotifyTaskCompletion.Status;

        public bool IsCompleted => NotifyTaskCompletion.IsCompleted;

        public bool IsNotCompleted => NotifyTaskCompletion.IsNotCompleted;

        public bool IsSuccessfullyCompleted => NotifyTaskCompletion.IsSuccessfullyCompleted;

        public bool IsCanceled => NotifyTaskCompletion.IsCanceled;

        public bool IsFaulted => NotifyTaskCompletion.IsFaulted;

        public AggregateException Exception => NotifyTaskCompletion.Exception;

        public Exception InnerException => NotifyTaskCompletion.InnerException;

        public string ErrorMessage => NotifyTaskCompletion.ErrorMessage;


        internal INotifyTaskCompletion<TResult> NotifyTaskCompletion;

        public abstract void StartTask();

        public abstract void CancelTask();

        internal void NotifyTaskCompletionOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public void Dispose()
        {
            if (NotifyTaskCompletion != null)
            {
                NotifyTaskCompletion.PropertyChanged -= NotifyTaskCompletionOnPropertyChanged;
            }
        }
    }
}
