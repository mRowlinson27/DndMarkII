﻿
namespace UIUtilities.API.AsyncCommands
{
    using System;
    using System.Threading.Tasks;

    public interface IAsyncCommandWatcher<TResult> : IWatchableCommandProperties<TResult>
    {
        Task ExecuteAsync(object parameter, Func<Task<TResult>> command, INotifyTaskCompletion<TResult> execution);
    }
}