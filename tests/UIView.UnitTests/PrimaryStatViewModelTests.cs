
namespace UIView.UnitTests
{
    using FakeItEasy;
    using FluentAssertions;
    using NUnit.Framework;
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

        private IUiThreadInvoker _uiThreadInvoker;

        private IAsyncCommandFactory _asyncCommandFactory;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _uiThreadInvoker = A.Fake<IUiThreadInvoker>();
            _asyncCommandFactory = A.Fake<IAsyncCommandFactory>();

            _primaryStatViewModel = new PrimaryStatViewModel(_logger, _asyncCommandFactory, _uiThreadInvoker)
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
        }
    }
}
