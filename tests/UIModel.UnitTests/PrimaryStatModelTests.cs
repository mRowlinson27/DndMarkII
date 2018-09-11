
namespace UIModel.UnitTests
{
    using API;
    using API.Dto;
    using FakeItEasy;
    using NUnit.Framework;
    using Services.API;
    using Services.API.Dto;

    [TestFixture]
    public class PrimaryStatModelTests
    {
        private PrimaryStatModel _primaryStatModel;

        private IPrimaryStatsService _primaryStatsService;
        private IAutoMapper _autoMapper;

        [SetUp]
        public void Setup()
        {
            _primaryStatsService = A.Fake<IPrimaryStatsService>();
            _autoMapper = A.Fake<IAutoMapper>();

            _primaryStatModel = new PrimaryStatModel(_primaryStatsService, _autoMapper);
        }

        [Test]
        public void Update_CallsService()
        {
            //Arrange
            var uiPrimaryStat = new UiPrimaryStat();
            var svcPrimaryStatUpdateRequest = new PrimaryStatUpdateRequest();
            A.CallTo(() => _autoMapper.MapToSvcRequest(uiPrimaryStat)).Returns(svcPrimaryStatUpdateRequest);

            //Act
            _primaryStatModel.Update(uiPrimaryStat);

            //Assert
            A.CallTo(() => _primaryStatsService.UpdatePrimaryStat(svcPrimaryStatUpdateRequest)).MustHaveHappened();
        }
    }
}
