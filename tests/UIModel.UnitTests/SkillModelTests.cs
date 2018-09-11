
namespace UIModel.UnitTests
{
    using API;
    using API.Dto;
    using FakeItEasy;
    using NUnit.Framework;
    using Services.API;
    using Services.API.Dto;

    [TestFixture]
    public class SkillModelTests
    {
        private SkillModel _skillModel;
        private ISkillsService _skillsService;
        private IAutoMapper _autoMapper;

        [SetUp]
        public void Setup()
        {
            _skillsService = A.Fake<ISkillsService>();
            _autoMapper = A.Fake<IAutoMapper>();

            _skillModel = new SkillModel(_skillsService, _autoMapper);
        }

        [Test]
        public void Update_MapsAndSendsToService()
        {
            //Arrange
            var uiSkill = new UiSkill();
            var skillUpdateRequest = new SkillUpdateRequest();

            A.CallTo(() => _autoMapper.MapToSvcRequest(uiSkill)).Returns(skillUpdateRequest);

            //Act
            _skillModel.Update(uiSkill);

            //Assert
            A.CallTo(() => _skillsService.UpdateSkill(skillUpdateRequest)).MustHaveHappened();
        }
    }
}
