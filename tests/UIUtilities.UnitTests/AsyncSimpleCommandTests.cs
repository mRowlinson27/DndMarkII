
namespace UIUtilities.UnitTests
{
    using System;
    using System.Threading.Tasks;
    using API;
    using API.AsyncCommands;
    using AsyncCommands;
    using FakeItEasy;
    using NUnit.Framework;
    using Utilities.API;

    [TestFixture]
    public class AsyncSimpleCommandTests
    {
        private AsyncSimpleCommand _asyncSimpleCommand;

        private Func<Task<object>> _command;
        private INotifyTaskCompletion<object> _notifyTaskCompletion;
        private IAsyncCommandWatcher<object> _asyncCommandWatcher;

        [SetUp]
        public void Setup()
        {
            _notifyTaskCompletion = A.Fake<INotifyTaskCompletion<object>>();
            _asyncCommandWatcher = A.Fake<IAsyncCommandWatcher<object>>();

            _command = async () => null;

            _asyncSimpleCommand = new AsyncSimpleCommand(_asyncCommandWatcher, _command, _notifyTaskCompletion);
        }

        [Test]
        public async Task ExecuteAsync_WrapsTaskAndPassesToBase()
        {
            //Arrange
            var parameter = new object();

            //Act
            await _asyncSimpleCommand.ExecuteAsync(parameter);

            //Assert
            A.CallTo(() => _asyncCommandWatcher.ExecuteAsync(parameter, _command, _notifyTaskCompletion)).MustHaveHappened();
        }
    }
}
