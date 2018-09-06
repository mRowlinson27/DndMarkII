
namespace UIUtilities.UnitTests
{
    using System;
    using System.Threading.Tasks;
    using AsyncCommands;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class AsyncSimpleCommandAdaptorTests
    {
        private AsyncSimpleCommandAdaptor _asyncSimpleCommandAdaptor;

        [Test]
        public async Task ExecuteAsync_CanExecute_RunsActionAsynchronously()
        {
            //Arrange
            bool wasCalled = false;
            _asyncSimpleCommandAdaptor = new AsyncSimpleCommandAdaptor(() => wasCalled = true) {ShouldExecute = true};

            //Act
            await _asyncSimpleCommandAdaptor.ExecuteAsync();

            //Assert
            wasCalled.Should().BeTrue();
        }

        [Test]
        public async Task ExecuteAsync_CannotExecute_RunsActionAsynchronously()
        {
            //Arrange
            bool wasCalled = false;
            _asyncSimpleCommandAdaptor = new AsyncSimpleCommandAdaptor(() => wasCalled = true) { ShouldExecute = false };

            //Act
            await _asyncSimpleCommandAdaptor.ExecuteAsync();

            //Assert
            wasCalled.Should().BeFalse();
        }
    }
}
