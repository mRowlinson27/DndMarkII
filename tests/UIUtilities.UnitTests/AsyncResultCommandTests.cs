
namespace UIUtilities.UnitTests
{
    using System;
    using System.Threading.Tasks;
    using API;
    using API.AsyncCommands;
    using AsyncCommands;
    using FakeItEasy;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class AsyncResultCommandTests
    {
        private AsyncResultCommand<int> _asyncSimpleCommand;

        private Func<Task<int>> _command;
        private INotifyTaskCompletion<int> _notifyTaskCompletion;
        private IAsyncCommandWatcher<int> _asyncCommandWatcher;

        [SetUp]
        public void Setup()
        {
            _notifyTaskCompletion = A.Fake<INotifyTaskCompletion<int>>();
            _asyncCommandWatcher = A.Fake<IAsyncCommandWatcher<int>>();

            _command = async () => 0;

            _asyncSimpleCommand = new AsyncResultCommand<int>(_asyncCommandWatcher, _command, _notifyTaskCompletion);
        }

        [Test]
        public async Task ExecuteAsync_WrapsTaskAndPassesToBase()
        {
            //Arrange
            var parameter = new object();
            A.CallTo(() => _asyncCommandWatcher.ExecuteAsync(parameter, _command, _notifyTaskCompletion)).Returns(10);

            //Act
            var result = await _asyncSimpleCommand.ExecuteAsync(parameter);

            //Assert
            result.Should().Be(10);
        }
    }
}
