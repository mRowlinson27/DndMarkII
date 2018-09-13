
namespace UIView.UnitTests
{
    using System.Threading.Tasks;
    using FakeItEasy;
    using NUnit.Framework;
    using TestUtils;
    using UIModel.API;
    using UIModel.API.Dto;
    using UIUtilities;
    using UIUtilities.AsyncCommands;
    using Utilities.API;
    using ViewModel;

    [TestFixture]
    public class SkillViewModelTests
    {
        private SkillViewModel _skillViewModel;

        private ILogger _logger;
        private ISkillModel _model;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _model = A.Fake<ISkillModel>();

            _skillViewModel = new SkillViewModel(_logger, _model, ViewModelCreation.GetRealAsyncCommandAdaptorFactory(), new UiThreadInvoker(_logger));
        }

        [Test]
        public async Task UpdateSkill_CallsModel()
        {
            //Arrange
            var uiSkill = new UiSkill();
            _skillViewModel.Skill = uiSkill;

            //Act
            await _skillViewModel.UpdateSkill.ExecuteAsync();

            //Assert
            A.CallTo(() => _model.Update(uiSkill)).MustHaveHappened();
        }
    }
}
