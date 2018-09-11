
namespace Utilities.UnitTests
{
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Implementation;
    using NUnit.Framework;

    [TestFixture]
    public class TaskWrapperTests
    {
        private TaskWrapper _taskWrapper;
        bool _wasCalled = false;

        [SetUp]
        public void Setup()
        {
            _wasCalled = false;
            _taskWrapper = new TaskWrapper();
        }

        [Test]
        public void WrapTaskWithNullReturnValue_Calling_DoesNotRunFunction()
        {
            //Arrange
            Func<Task> taskFunc = Call;

            //Act
            _taskWrapper.WrapTaskWithNullReturnValue(taskFunc);

            //Assert
            _wasCalled.Should().BeFalse();
        }

        [Test]
        public async Task WrapTaskWithNullReturnValue_ReturnedFunctionCallsOriginalAndReturnsNull()
        {
            //Arrange
            Func<Task> taskFunc = Call;
            var newFunc = _taskWrapper.WrapTaskWithNullReturnValue(taskFunc);

            //Act
            var result = await newFunc();

            //Assert
            result.Should().BeNull();
            _wasCalled.Should().BeTrue();
        }

        [Test]
        public void WrapActionWithNullReturnValue_Called_DoesNotRunAction()
        {
            //Arrange
            void Action() => _wasCalled = true;

            //Act
            _taskWrapper.WrapActionWithNullReturnValue(Action);

            //Assert
            _wasCalled.Should().BeFalse();
        }

        [Test]
        public async Task WrapActionWithNullReturnValue_ReturnedFunctionCallsOriginalAndReturnsNull()
        {
            //Arrange
            void Action() => _wasCalled = true;
            var newFunc = _taskWrapper.WrapActionWithNullReturnValue(Action);

            //Act
            var result = await newFunc();

            //Assert
            result.Should().BeNull();
            _wasCalled.Should().BeTrue();
        }

        private async Task Call()
        {
            _wasCalled = true;
        }
    }
}
