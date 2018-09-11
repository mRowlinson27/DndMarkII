
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
    public class AsyncCommandWithInputTests
    {
        private AsyncCommandWithInput<int> _asyncCommandWithInput;

        private Func<int, Task> _command;
        private Func<Task<object>> _wrappedCommand;
        private INotifyTaskCompletion<object> _notifyTaskCompletion;
        private ITaskWrapper _taskWrapper;
        private IAsyncCommandWatcher<object> _asyncCommandWatcher;

        [SetUp]
        public void Setup()
        {
            _notifyTaskCompletion = A.Fake<INotifyTaskCompletion<object>>();
            _taskWrapper = A.Fake<ITaskWrapper>();
            _asyncCommandWatcher = A.Fake<IAsyncCommandWatcher<object>>();

            _command = async (intIn) => { };
            _wrappedCommand = async () => null;

            _asyncCommandWithInput = new AsyncCommandWithInput<int>(_asyncCommandWatcher, _command, _notifyTaskCompletion, _taskWrapper);
        }

        [Test]
        public async Task ExecuteAsync_WrapsTaskAndPassesToBase()
        {
            //Arrange
            var parameter = 5;
            A.CallTo(() => _taskWrapper.WrapTaskWithNullReturnValue(_command, parameter)).Returns(_wrappedCommand);

            //Act
            await _asyncCommandWithInput.ExecuteAsync(parameter);

            //Assert
            A.CallTo(() => _asyncCommandWatcher.ExecuteAsync(parameter, _wrappedCommand, _notifyTaskCompletion)).MustHaveHappened();
        }
    }
}
