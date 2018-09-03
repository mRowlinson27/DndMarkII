
namespace UIView.UnitTests
{
    using FakeItEasy;
    using FluentAssertions;
    using NUnit.Framework;
    using UIModel.API;
    using UIModel.API.Dto;
    using UIUtilities.API;
    using UIUtilities.API.AsyncCommands;
    using Utilities.API;
    using ViewModel;

    [TestFixture]
    public class PrimaryStatViewModelTests
    {
        private PrimaryStatViewModel _primaryStatViewModel;

        private ILogger _logger;

        private IPrimaryStatModel _model;

        private IUiThreadInvoker _uiThreadInvoker;

        private IAsyncCommandFactory _asyncCommandFactory;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _model = A.Fake<IPrimaryStatModel>();
            _uiThreadInvoker = A.Fake<IUiThreadInvoker>();
            _asyncCommandFactory = A.Fake<IAsyncCommandFactory>();

            _primaryStatViewModel = new PrimaryStatViewModel(_logger, _model, _asyncCommandFactory, _uiThreadInvoker)
            {
                PrimaryStat = new UiPrimaryStat()
            };
        }

        [Test]
        public void Set_AbilityScore_CallModelDataUpdated()
        {
            //Arrange
            var newAbilityScore = "5";

            //Act
            _primaryStatViewModel.AbilityScore = newAbilityScore;

            //Assert
            _primaryStatViewModel.PrimaryStat.AbilityScore.Should().Be(newAbilityScore);
            A.CallTo(() => _model.UpdateStatAsync(_primaryStatViewModel.PrimaryStat)).MustHaveHappened();
        }
    }
}
