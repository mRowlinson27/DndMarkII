
namespace UIView.UnitTests.TestUtils
{
    using FakeItEasy;
    using UIUtilities;
    using UIUtilities.API.AsyncCommands;
    using UIUtilities.AsyncCommands;
    using Utilities.API;
    using Utilities.Implementation;

    internal static class ViewModelCreation
    {
        public static IAsyncCommandAdaptorFactory GetRealAsyncCommandAdaptorFactory()
        {
            var logger = A.Fake<ILogger>();
            return new AsyncCommandAdaptorFactory(new AsyncCommandFactory(new NotifyTaskCompletionFactory(logger),
                new AsyncCommandWatcherFactory(new UiStateController(logger, new UiLockerContextFactory())), new TaskWrapper()));
        }
    }
}
