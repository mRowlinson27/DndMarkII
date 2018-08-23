﻿
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.Threading.Tasks;
    using API;
    using API.AsyncCommands;

    public class AsyncCommandFactory : IAsyncCommandFactory
    {
        private readonly INotifyTaskCompletionFactory _notifyTaskCompletionFactory;

        public AsyncCommandFactory(INotifyTaskCompletionFactory notifyTaskCompletionFactory)
        {
            _notifyTaskCompletionFactory = notifyTaskCompletionFactory;
        }

        public IAsyncCommand Create(Func<Task> command)
        {
            return new AsyncSimpleCommand(command, _notifyTaskCompletionFactory);
        }

        public IAsyncCommand Create<TIn>(Func<TIn, Task> command)
        {
            return new AsyncCommandWithInput<TIn>(command, _notifyTaskCompletionFactory);
        }

//        public static AsyncSimpleCommand<object> Create(Func<CancellationToken, Task> command)
//        {
//            return new AsyncSimpleCommand<object>(async token => { await command(token); return null; });
//        }
//
//        public static AsyncSimpleCommand<TResult> Create<TResult>(Func<CancellationToken, Task<TResult>> command)
//        {
//            return new AsyncSimpleCommand<TResult>(command);
//        }
    }
}