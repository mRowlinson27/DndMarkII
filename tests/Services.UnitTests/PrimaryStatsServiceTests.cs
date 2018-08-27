
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

        private List<PrimaryStat> _primaryStats;

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
            A.CallTo(() => _primaryStatsRepo.GetPrimaryStatsAsync()).Returns(_primaryStats);

            //Act
            var result = await _primaryStatsService.GetAllPrimaryStatsAsync();

            //Assert
            result.Should().BeEquivalentTo(_primaryStats);
        }

        private void GenerateBasicModel()
        {
            _primaryStats = new List<PrimaryStat>
            {
                new PrimaryStat {Name = "PrimaryStat1"}
            };
        }
    }
}
