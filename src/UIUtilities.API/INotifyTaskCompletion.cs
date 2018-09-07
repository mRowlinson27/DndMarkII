
namespace UIUtilities.API
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;

    public interface INotifyTaskCompletion<TResult> : INotifyPropertyChanged
    {
        Task<TResult> Task { get; }

        TResult Result { get; }

        TaskStatus Status { get; }

        bool IsCompleted { get; }

        bool IsNotCompleted { get; }

        bool IsSuccessfullyCompleted { get; }

        bool IsCanceled { get; }

        bool IsFaulted { get; }

        AggregateException Exception { get; }

        Exception InnerException { get; }

        string ErrorMessage { get; }

        Task TaskCompletion { get; }

        void Start(Func<Task<TResult>> task);
    }
}
