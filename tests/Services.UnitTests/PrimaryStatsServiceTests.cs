
namespace Services.UnitTests
{
    using System.Collections.Generic;
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
            var dbPrimaryStats = new List<PrimaryStat>
            {
                new PrimaryStat
                {
                    Id = AbilityModifier.Cha,
                    Name = "PrimaryStat1",
                    AbilityScore = 12,
                }
            };

            var correctSvcPrimaryStats = new List<Services.API.Dto.PrimaryStat>
            {
                new API.Dto.PrimaryStat
                {
                    Id = API.Dto.AbilityModifier.Cha,
                    Name =  "PrimaryStat1",
                    AbilityScore = 12
                }
            };

            A.CallTo(() => _primaryStatsRepo.GetPrimaryStatsAsync()).Returns(dbPrimaryStats);

            //Act
            var result = await _primaryStatsService.GetAllPrimaryStatsAsync();

            //Assert
            result.Should().BeEquivalentTo(dbPrimaryStats);
        }
    }
}
