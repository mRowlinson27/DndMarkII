
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
    public class AsyncCommandBaseTests
    {
        private AsyncCommandBase2<object> _asyncCommandBase2;
        private INotifyTaskCompletion<object> _notifyTaskCompletion;

        private Task<object> _command;

        [SetUp]
        public void Setup()
        {
            _notifyTaskCompletion = A.Fake<INotifyTaskCompletion<object>>();

            _command = WrapTaskWithReturnValue(Command);
            _asyncCommandBase2 = new AsyncCommandBase2<object>();
        }

        [Test]
        public void CanExecute_NeverRun_ReturnsTrue()
        {
            //Arrange

            //Act
            var result = _asyncCommandBase2.CanExecute(null);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public void CanExecute_CurrentlyRunning_ReturnsFalse()
        {
            //Arrange
            A.CallTo(() => _notifyTaskCompletion.IsCompleted).Returns(false);
            _asyncCommandBase2.Execution = _notifyTaskCompletion;

            //Act
            var result = _asyncCommandBase2.CanExecute(null);

            //Assert
            result.Should().BeFalse();
        }

        [Test]
        public void CanExecute_CurrentlyComplete_ReturnsTrue()
        {
            //Arrange
            A.CallTo(() => _notifyTaskCompletion.IsCompleted).Returns(true);

            //Act
            var result = _asyncCommandBase2.CanExecute(null);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task Execute_StartsRunningINotificationTask_WaitsForCompletion()
        {
            //Arrange

            //Act
            await _asyncCommandBase2.ExecuteAsync(null, _command, _notifyTaskCompletion);

            //Assert
            A.CallTo(() => _notifyTaskCompletion.Start(_command)).MustHaveHappened();
            A.CallTo(() => _notifyTaskCompletion.TaskCompletion).MustHaveHappened();
        }

        [Test]
        public void INotificationTask_OnIsCompletedPropertyChanged_RaisesCanExecuteChanged()
        {
            //Arrange
            bool called = false;
            _asyncCommandBase2.CanExecuteChanged += (s, a) => called = true;
            _asyncCommandBase2.Execution = _notifyTaskCompletion;

            //Act
            _notifyTaskCompletion.PropertyChanged += Raise.FreeForm<PropertyChangedEventHandler>.With(_notifyTaskCompletion, new PropertyChangedEventArgs("IsCompleted"));

            //Assert
            called.Should().BeTrue();
        }

        private async Task<object> WrapTaskWithReturnValue(Func<Task> command)
        {
            await command().ConfigureAwait(false);
            return null;
        }

        private Task Command()
        {
            throw new NotImplementedException();
        }
    }
}
