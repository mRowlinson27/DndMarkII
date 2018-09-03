
namespace UIUtilities.UnitTests
{
    using System;
    using API;
    using FakeItEasy;
    using FluentAssertions;
    using NUnit.Framework;
    using Utilities.API;

    [TestFixture]
    public class UiStateControllerTests
    {
        private UiStateController _uiStateController;

        private ILogger _logger;
        private IUiLockerContextFactory _uiLockerContextFactory;

        private IUiLockerContext _uiLockerContext;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _uiLockerContextFactory = A.Fake<IUiLockerContextFactory>();

            _uiLockerContext = A.Fake<IUiLockerContext>();
            
            _uiStateController = new UiStateController(_logger, _uiLockerContextFactory);
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

        [Test]
        public void LockedContext_PassesItself_ReturnsLockedContext()
        {
            //Arrange
            A.CallTo(() => _uiLockerContextFactory.Create(_uiStateController)).Returns(_uiLockerContext);

            //Act
            var resultContext = _uiStateController.LockedContext();

            //Assert
            resultContext.Should().Be(_uiLockerContext);
        }
    }
}
