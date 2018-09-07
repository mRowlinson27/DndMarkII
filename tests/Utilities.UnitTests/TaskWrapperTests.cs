
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

        private async Task Call()
        {
            _wasCalled = true;
        }
    }
}
