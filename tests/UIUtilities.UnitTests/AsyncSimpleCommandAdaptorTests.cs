
namespace UIUtilities.UnitTests
{
    using System;
    using System.ComponentModel;
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
        public async Task ExecuteAsync_CanExecute_CallsChildCommand()
        {
            //Arrange
            A.CallTo(() => _asyncCommand.CanExecute(null)).Returns(true);
            _asyncCommand.CanExecuteChanged += Raise.FreeForm<EventHandler>.With(_asyncCommand, EventArgs.Empty);

            //Act
            await _asyncSimpleCommandAdaptor.ExecuteAsync();

            //Assert
            A.CallTo(() => _asyncCommand.ExecuteAsync(null)).MustHaveHappened();
        }

        [Test]
        public async Task ExecuteAsync_CannotExecute_DoesNothing()
        {
            //Arrange
            A.CallTo(() => _asyncCommand.CanExecute(null)).Returns(false);

            //Act
            await _asyncSimpleCommandAdaptor.ExecuteAsync();

            //Assert
            A.CallTo(() => _asyncCommand.ExecuteAsync(null)).MustNotHaveHappened();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void CanExecute_ReturnsChildValue(bool canExecuteValue)
        {
            //Arrange
            A.CallTo(() => _asyncCommand.CanExecute(null)).Returns(canExecuteValue);
            _asyncCommand.CanExecuteChanged += Raise.FreeForm<EventHandler>.With(_asyncCommand, EventArgs.Empty);

            //Act
            var result = _asyncSimpleCommandAdaptor.CanExecute(null);

            //Assert
            result.Should().Be(canExecuteValue);
        }
    }
}
