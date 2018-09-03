
namespace UIUtilities.UnitTests
{
    using System;
    using FakeItEasy;
    using FluentAssertions;
    using NUnit.Framework;
    using Utilities.API;

    [TestFixture]
    public class UiStateControllerTests
    {
        private UiStateController _uiStateController;
        private ILogger _logger;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _uiStateController = new UiStateController(_logger);
        }

        [Test]
        public void UiLocked_NoCalls_ReturnsFalse()
        {
            //Arrange

            //Act
            var result = _uiStateController.UiLocked;

            //Assert
            result.Should().BeFalse();
        }

        [Test]
        public void UiLocked_IncCalled_ReturnsTrue()
        {
            //Arrange

            //Act
            _uiStateController.IncUiLock();
            var result = _uiStateController.UiLocked;

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public void UiLocked_DecCalled_ThrowsException()
        {
            //Arrange

            //Act
            Assert.Throws<ArgumentOutOfRangeException>(() => _uiStateController.DecUiLock());

            //Assert
        }

        [Test]
        public void UiLocked_IncCalledThenDec_ReturnsFalse()
        {
            //Arrange

            //Act
            _uiStateController.IncUiLock();
            _uiStateController.DecUiLock();
            var result = _uiStateController.UiLocked;

            //Assert
            result.Should().BeFalse();
        }

        [Test]
        public void UiLocked_StatusChanges_RaisesEvent()
        {
            //Arrange
            int called = 0;
            _uiStateController.UiLockUpdated += (s, a) => called++;

            //Act
            _uiStateController.IncUiLock();
            _uiStateController.DecUiLock();

            //Assert
            called.Should().Be(2);
        }
    }
}
