
namespace UIUtilities
{
    using System;
    using System.Threading.Tasks;
    using API.AsyncCommands;
    using AsyncCommands;

    public class AsyncCommandAdaptorFactory : IAsyncCommandAdaptorFactory
    {
        private readonly IAsyncCommandFactory _asyncCommandFactory;

        public AsyncCommandAdaptorFactory(IAsyncCommandFactory asyncCommandFactory)
        {
            _asyncCommandFactory = asyncCommandFactory;
        }

        public IAsyncCommandAdaptor Create(Func<Task> command)
        {
            return new AsyncSimpleCommandAdaptor(_asyncCommandFactory.Create(command));
        }

        public IAsyncCommandAdaptor Create(Action action)
        {
            return new AsyncSimpleCommandAdaptor(_asyncCommandFactory.Create(action));
        }

        public IAsyncCommandAdaptor CreateWithContext(Func<Task> command)
        {
            return new AsyncSimpleCommandAdaptor(_asyncCommandFactory.CreateWithContext(command));
        }

        public IAsyncCommandAdaptor CreateWithContext(Action action)
        {
            return new AsyncSimpleCommandAdaptor(_asyncCommandFactory.CreateWithContext(action));
        }
    }
}
