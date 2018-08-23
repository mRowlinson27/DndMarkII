﻿
namespace UIUtilities.API
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;

    public interface IAsyncTaskRunnerBase<TResult> : INotifyPropertyChanged, IDisposable
    {
        TaskStatus Status { get; }

        bool HasStarted { get; }

        bool IsCompleted { get; }

        bool IsNotCompleted { get; }

        bool IsSuccessfullyCompleted { get; }

        bool IsCanceled { get; }

        bool IsFaulted { get; }

        AggregateException Exception { get; }

        Exception InnerException { get; }

        string ErrorMessage { get; }

        void StartTask();

        void CancelTask();
    }

    public interface IAsyncTaskRunner<TResult> : IAsyncTaskRunnerBase<TResult>
    {
        Task<TResult> Task { get; }

        TResult Result { get; }
    }

    public interface IAsyncTaskRunner : IAsyncTaskRunnerBase<object>
    {
        Task Task { get; }
    }
}