
namespace UIUtilities.UnitTests
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using API;
    using AsyncCommands;
    using FakeItEasy;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class AsyncCommandWatcherTests
    {
        private AsyncCommandWatcher<object> _asyncCommandWatcher;
        private INotifyTaskCompletion<object> _notifyTaskCompletion;

        private Func<Task<object>> _command;

        [SetUp]
        public void Setup()
        {
            _notifyTaskCompletion = A.Fake<INotifyTaskCompletion<object>>();

            _command = CommandTask;
            _asyncCommandWatcher = new AsyncCommandWatcher<object>();
        }

        [Test]
        public void CanExecute_NeverRun_ReturnsTrue()
        {
            //Arrange

            //Act
            var result = _asyncCommandWatcher.CanExecute(null);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public void CanExecute_CurrentlyRunning_ReturnsFalse()
        {
            //Arrange
            A.CallTo(() => _notifyTaskCompletion.IsCompleted).Returns(false);
            _asyncCommandWatcher.Execution = _notifyTaskCompletion;

            //Act
            var result = _asyncCommandWatcher.CanExecute(null);

            //Assert
            result.Should().BeFalse();
        }

        [Test]
        public void CanExecute_CurrentlyComplete_ReturnsTrue()
        {
            //Arrange
            A.CallTo(() => _notifyTaskCompletion.IsCompleted).Returns(true);

            //Act
            var result = _asyncCommandWatcher.CanExecute(null);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task Execute_StartsRunningINotificationTask_WaitsForCompletion()
        {
            //Arrange

            //Act
            await _asyncCommandWatcher.ExecuteAsync(null, _command, _notifyTaskCompletion);

            //Assert
            A.CallTo(() => _notifyTaskCompletion.Start(_command)).MustHaveHappened();
            A.CallTo(() => _notifyTaskCompletion.TaskCompletion).MustHaveHappened();
        }

        [Test]
        public void INotificationTask_OnIsCompletedPropertyChanged_RaisesCanExecuteChanged()
        {
            //Arrange
            bool called = false;
            _asyncCommandWatcher.CanExecuteChanged += (s, a) => called = true;
            _asyncCommandWatcher.Execution = _notifyTaskCompletion;

            //Act
            _notifyTaskCompletion.PropertyChanged += Raise.FreeForm<PropertyChangedEventHandler>.With(_notifyTaskCompletion, new PropertyChangedEventArgs("IsCompleted"));

            //Assert
            called.Should().BeTrue();
        }

        private async Task<object> CommandTask()
        {
            throw new NotImplementedException();
        }
    }
}
