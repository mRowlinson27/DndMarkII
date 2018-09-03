
namespace UIUtilities.UnitTests
{
    using API;
    using FakeItEasy;
    using NUnit.Framework;

    [TestFixture]
    public class UiLockerContextTests
    {
        private UiLockerContext _uiLockerContext;

        private IUiStateController _uiStateController;

        [SetUp]
        public void Setup()
        {
            _uiStateController = A.Fake<IUiStateController>();
        }

        [Test]
        public void Ctr_CallsIncOnStateController()
        {
            //Arrange

            //Act
            _uiLockerContext = new UiLockerContext(_uiStateController);

            //Assert
            A.CallTo(() => _uiStateController.IncUiLock()).MustHaveHappened();
        }

        [Test]
        public void Dispose_CallsDecOnStateController()
        {
            //Arrange
            _uiLockerContext = new UiLockerContext(_uiStateController);

            //Act
            _uiLockerContext.Dispose();

            //Assert
            A.CallTo(() => _uiStateController.DecUiLock()).MustHaveHappened();
        }
    }
}
