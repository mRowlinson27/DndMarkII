
namespace UIUtilities.AsyncCommands
{
    using System;
    using System.Threading.Tasks;

    public static class AsyncCommandFactory
    {
        public static AsyncSimpleCommand Create(Func<Task> command)
        {
            return new AsyncSimpleCommand(command);
        }

        public static AsyncCommandWithInput<TIn> Create<TIn>(Func<TIn, Task> command)
        {
            return new AsyncCommandWithInput<TIn>(command);
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