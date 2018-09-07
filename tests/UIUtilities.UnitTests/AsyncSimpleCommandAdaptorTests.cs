
namespace UIUtilities.UnitTests
{
    using System;
    using System.Threading.Tasks;
    using API.AsyncCommands;
    using AsyncCommands;
    using FakeItEasy;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class AsyncSimpleCommandAdaptorTests
    {
        private AsyncSimpleCommandAdaptor _asyncSimpleCommandAdaptor;

        private IAsyncCommand _asyncCommand;

        [SetUp]
        public void Setup()
        {
            _asyncCommand = A.Fake<IAsyncCommand>();
            _asyncSimpleCommandAdaptor = new AsyncSimpleCommandAdaptor(_asyncCommand);
        }

        [Test]
        public async Task ExecuteAsync_CanExecute_RunsActionAsynchronously()
        {
            //Arrange
            _asyncSimpleCommandAdaptor.ShouldExecute = true;

            //Act
            await _asyncSimpleCommandAdaptor.ExecuteAsync();

            //Assert
            A.CallTo(() => _asyncCommand.ExecuteAsync(null)).MustHaveHappened();
        }

        [Test]
        public async Task ExecuteAsync_CannotExecute_DoesNothing()
        {
            //Arrange
            _asyncSimpleCommandAdaptor.ShouldExecute = false;

            //Act
            await _asyncSimpleCommandAdaptor.ExecuteAsync();

            //Assert
            A.CallTo(() => _asyncCommand.ExecuteAsync(null)).MustNotHaveHappened();
        }
    }
}
