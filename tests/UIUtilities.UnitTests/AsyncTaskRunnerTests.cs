
namespace UIUtilities.UnitTests
{
    using System;
    using System.Threading.Tasks;
    using API;
    using FakeItEasy;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class AsyncTaskRunnerTests
    {
        private INotifyTaskCompletionFactory _notifyTaskCompletionFactory;

        private Func<Task> _basicFuncTask;

        [SetUp]
        public void Setup()
        {
            _notifyTaskCompletionFactory = A.Fake<INotifyTaskCompletionFactory>();
            _basicFuncTask = async ()  => { await Task.Delay(1);};
        }

        [Test]
        public void AsyncTaskRunner_HasNotRanTask_HasStartedFalse()
        {
            //Arrange
            var asyncTaskRunner = new AsyncTaskRunner(_basicFuncTask, _notifyTaskCompletionFactory);

            //Act
            var hasStarted = asyncTaskRunner.HasStarted;

            //Assert
            hasStarted.Should().BeFalse();
        }

        [Test]
        public void AsyncTaskRunner_RanTask_HasStartedTrue()
        {
            //Arrange
            var asyncTaskRunner = new AsyncTaskRunner(_basicFuncTask, _notifyTaskCompletionFactory);

            //Act
            asyncTaskRunner.StartTask();
            var hasStarted = asyncTaskRunner.HasStarted;

            //Assert
            hasStarted.Should().BeTrue();
        }
    }
}
