
namespace Services.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Database.API;
    using Database.API.Dto;
    using FakeItEasy;
    using FluentAssertions;
    using NUnit.Framework;
    using Utilities.API;

    [TestFixture]
    public class PrimaryStatsServiceTests
    {
        private PrimaryStatsService _primaryStatsService;

        private ILogger _logger;
        private IPrimaryStatsRepo _primaryStatsRepo;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _primaryStatsRepo = A.Fake<IPrimaryStatsRepo>();

            _primaryStatsService = new PrimaryStatsService(_logger, _primaryStatsRepo);
        }

        [Test]
        public async Task GetAllPrimaryStatsAsync_GetsFromDatabase()
        {
            //Arrange
            var dbPrimaryStat = new PrimaryStat
            {
                Id = AbilityModifier.Cha,
                Name = "PrimaryStat1",
                AbilityScore = 12,
            };

            var dbPrimaryStats = new List<PrimaryStat> {dbPrimaryStat};

            A.CallTo(() => _primaryStatsRepo.GetPrimaryStatsAsync()).Returns(dbPrimaryStats);

            //Act
            var result = await _primaryStatsService.GetAllPrimaryStatsAsync();
            var firstResult = result.FirstOrDefault();

            //Assert
            firstResult.Should().NotBe(null);
            firstResult.Id.Should().Be(API.Dto.AbilityModifier.Cha);
            firstResult.Name.Should().Be(dbPrimaryStat.Name);
            firstResult.AbilityScore.Should().Be(dbPrimaryStat.AbilityScore);
        }

        [TestCase(1, -5)]
        [TestCase(8, -1)]
        [TestCase(9, -1)]
        [TestCase(10, 0)]
        [TestCase(11, 0)]
        [TestCase(12, 1)]
        [TestCase(16, 3)]
        public async Task GetAllPrimaryStatsAsync_CorrectModifier(int abilityScore, int correctAbilityModifier)
        {
            //Arrange
            var dbPrimaryStats = new List<PrimaryStat>
            {
                new PrimaryStat
                {
                    Id = AbilityModifier.Cha,
                    Name = "PrimaryStat1",
                    AbilityScore = abilityScore,
                }
            };

            A.CallTo(() => _primaryStatsRepo.GetPrimaryStatsAsync()).Returns(dbPrimaryStats);

            var result = await _primaryStatsService.GetAllPrimaryStatsAsync();
            var firstResult = result.FirstOrDefault();

            //Assert
            firstResult.Should().NotBe(null);
            firstResult.AbilityModifier.Should().Be(correctAbilityModifier);
        }
    }
}
