
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
    public class AsyncCommandWatcherContextDecoratorTests
    {
        private AsyncCommandWatcherContextDecorator<object> _asyncCommandWatcherContextDecorator;
        private IAsyncCommandWatcher<object> _asyncCommandWatcher;
        private IUiStateController _uiStateController;

        [SetUp]
        public void Setup()
        {
            _asyncCommandWatcher = A.Fake<IAsyncCommandWatcher<object>>();
            _uiStateController = A.Fake<IUiStateController>();

            _asyncCommandWatcherContextDecorator = new AsyncCommandWatcherContextDecorator<object>(_asyncCommandWatcher, _uiStateController);
        }

        [Test]
        public void Execution_GetsExecutionFromChild()
        {
            //Arrange
            var executionResult = A.Fake<INotifyTaskCompletion<object>>();
            A.CallTo(() => _asyncCommandWatcher.Execution).Returns(executionResult);

            //Act
            var result = _asyncCommandWatcherContextDecorator.Execution;

            //Assert
            result.Should().Be(executionResult);
        }

        [TestCase(true, true, true)]
        [TestCase(false, false, false)]
        [TestCase(false, true, false)]
        [TestCase(true, false, false)]
        public void CanExecute_ReturnsChildAndedWithContextLock(bool childCanExecute, bool uiunLocked, bool correctResult)
        {
            //Arrange
            A.CallTo(() => _asyncCommandWatcher.CanExecute(null)).Returns(childCanExecute);
            A.CallTo(() => _uiStateController.UiLocked).Returns(!uiunLocked);

            //Act
            var result = _asyncCommandWatcherContextDecorator.CanExecute(null);

            //Assert
            result.Should().Be(correctResult);
        }

        [Test]
        public async Task ExecuteAsync_UsesUiLock()
        {
            //Arrange
            var uiLock = A.Fake<IUiLockerContext>();
            A.CallTo(() => _uiStateController.LockedContext()).Returns(uiLock);

            var parameter = new Object();
            Func<Task<object>> commandFunc = CommandFunc;
            var notifyTaskCompletion = A.Fake<INotifyTaskCompletion<object>>();

            //Act
            await _asyncCommandWatcherContextDecorator.ExecuteAsync(parameter, commandFunc, notifyTaskCompletion);

            //Assert
            A.CallTo(() => _uiStateController.LockedContext()).MustHaveHappened()
                .Then(A.CallTo(() => _asyncCommandWatcher.ExecuteAsync(parameter, commandFunc, notifyTaskCompletion)).MustHaveHappened())
                .Then(A.CallTo(() => uiLock.Dispose()).MustHaveHappened());

        }

        [Test]
        public void CanExecuteChanged_UiUnlocked_TriggersWhenChildChanges()
        {
            //Arrange
            A.CallTo(() => _uiStateController.UiLocked).Returns(false);

            bool wasExecuted = false;
            _asyncCommandWatcherContextDecorator.CanExecuteChanged += (sender, args) => wasExecuted = true;

            //Act
            _asyncCommandWatcher.CanExecuteChanged += Raise.FreeForm<EventHandler>.With(_asyncCommandWatcher, EventArgs.Empty);

            //Assert
            wasExecuted.Should().BeTrue();
        }

        [Test]
        public void CanExecuteChanged_ChildCanExecute_TriggersWhenUiLockChanges()
        {
            //Arrange
            A.CallTo(() => _asyncCommandWatcher.CanExecute(null)).Returns(true);

            bool wasExecuted = false;
            _asyncCommandWatcherContextDecorator.CanExecuteChanged += (sender, args) => wasExecuted = true;

            //Act
            _uiStateController.UiLockUpdated += Raise.FreeForm<EventHandler>.With(_asyncCommandWatcher, EventArgs.Empty);

            //Assert
            wasExecuted.Should().BeTrue();
        }

        private Task<object> CommandFunc()
        {
            throw new NotImplementedException();
        }
    }
}
