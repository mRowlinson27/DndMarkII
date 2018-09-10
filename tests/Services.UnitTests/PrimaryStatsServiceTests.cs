
namespace Services.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using API;
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
        private ISvcAutoMapper _svcAutoMapper;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _primaryStatsRepo = A.Fake<IPrimaryStatsRepo>();
            _svcAutoMapper = A.Fake<ISvcAutoMapper>();

            _primaryStatsService = new PrimaryStatsService(_logger, _primaryStatsRepo, _svcAutoMapper);
        }

        [Test]
        public void GetAllPrimaryStats_GetsFromDatabase_TransformsMundaneAttributes()
        {
            //Arrange
            var dbPrimaryStats = new List<PrimaryStat>
            {
                new PrimaryStat
                {
                    Id = AbilityType.Cha,
                    Name = "PrimaryStat1",
                    AbilityScore = 12,
                }
            };

            var svcPrimaryStats = new List<API.Dto.PrimaryStat>
            {
                new API.Dto.PrimaryStat
                {
                    Id = API.Dto.AbilityType.Cha,
                    Name = "PrimaryStat1",
                    AbilityScore = 12
                }
            };


            A.CallTo(() => _primaryStatsRepo.GetPrimaryStats()).Returns(dbPrimaryStats);
            A.CallTo(() => _svcAutoMapper.MapToSvc(dbPrimaryStats)).Returns(svcPrimaryStats);

            //Act
            var result = _primaryStatsService.GetAllPrimaryStats();

            //Assert
            result.Should().BeEquivalentTo(svcPrimaryStats);
        }

        [TestCase(1, -5)]
        [TestCase(8, -1)]
        [TestCase(9, -1)]
        [TestCase(10, 0)]
        [TestCase(11, 0)]
        [TestCase(12, 1)]
        [TestCase(16, 3)]
        public void GetAllPrimaryStats_CorrectModifier(int abilityScore, int correctAbilityModifier)
        {
            //Arrange
            var svcPrimaryStats = new List<API.Dto.PrimaryStat>
            {
                new API.Dto.PrimaryStat
                {
                    Id = API.Dto.AbilityType.Cha,
                    Name = "PrimaryStat1",
                    AbilityScore = abilityScore
                }
            };

            A.CallTo(() => _svcAutoMapper.MapToSvc(A<IEnumerable<PrimaryStat>>.Ignored)).Returns(svcPrimaryStats);

            //Act
            var result = _primaryStatsService.GetAllPrimaryStats();
            var firstResult = result.FirstOrDefault();

            //Assert
            firstResult.Should().NotBe(null);
            firstResult.AbilityModifier.Should().Be(correctAbilityModifier);
        }
    }
}
