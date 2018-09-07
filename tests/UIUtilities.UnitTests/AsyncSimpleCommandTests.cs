﻿
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

        private Func<Task> _command;
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

            _command = async () => {  };
            _wrappedCommand = async () => null;

            _asyncSimpleCommand = new AsyncSimpleCommand(_asyncCommandWatcher, _command, _notifyTaskCompletion, _taskWrapper);
        }

        [Test]
        public async Task ExecuteAsync_WrapsTaskAndPassesToBase()
        {
            //Arrange
            A.CallTo(() => _taskWrapper.WrapTaskWithNullReturnValue(_command)).Returns(_wrappedCommand);
            var parameter = new object();

            //Act
            await _asyncSimpleCommand.ExecuteAsync(parameter);

            //Assert
            A.CallTo(() => _asyncCommandWatcher.ExecuteAsync(parameter, _wrappedCommand, _notifyTaskCompletion)).MustHaveHappened();
        }
    }
}
