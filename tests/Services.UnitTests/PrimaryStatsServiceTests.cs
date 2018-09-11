
namespace Services.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using API;
    using API.Dto;
    using Database.API;
    using FakeItEasy;
    using FluentAssertions;
    using NUnit.Framework;
    using Utilities.API;
    using AbilityType = Database.API.Dto.AbilityType;
    using PrimaryStat = Database.API.Dto.PrimaryStat;

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

        [Test]
        public void GetAllPrimaryStats_CalledTwice_GetsFromDatabaseOnce()
        {
            //Arrange

            //Act
            _primaryStatsService.GetAllPrimaryStats();
            _primaryStatsService.GetAllPrimaryStats();

            //Assert
            A.CallTo(() => _primaryStatsRepo.GetPrimaryStats()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void GetAllPrimaryStats_ReciveInCorrectOrder()
        {
            //Arrange
            A.CallTo(() => _svcAutoMapper.MapToSvc(A<IEnumerable<PrimaryStat>>.Ignored)).Returns(GenerateOutOfOrderPrimaryStats());

            //Act
            var result = _primaryStatsService.GetAllPrimaryStats();

            //Assert
            result.Should().BeEquivalentTo(GetCorrectSequenceSvcPrimaryStats());
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

        [Test]
        public void UpdatePrimaryStat_UpdatesRepo()
        {
            //Arrange
            var newScore = 12;
            var updateRequest = new PrimaryStatUpdateRequest
            {
                AbilityScore = newScore,
                Id = API.Dto.AbilityType.Cha
            };

            _primaryStatsService.CachedPrimaryStats = new Dictionary<API.Dto.AbilityType, API.Dto.PrimaryStat>
            {
                {API.Dto.AbilityType.Cha, new API.Dto.PrimaryStat { Id = API.Dto.AbilityType.Cha, AbilityScore = 30}}
            };

            //Act
            _primaryStatsService.UpdatePrimaryStat(updateRequest);
            var result = _primaryStatsService.GetAllPrimaryStats();

            //Assert
            result.FirstOrDefault().AbilityScore.Should().Be(newScore);
        }

        [Test]
        public void UpdatePrimaryStat_CallsStatChangedIfDifferent()
        {
            //Arrange
            var newScore = 12;
            var updateRequest = new PrimaryStatUpdateRequest
            {
                AbilityScore = newScore,
                Id = API.Dto.AbilityType.Cha
            };

            _primaryStatsService.CachedPrimaryStats = new Dictionary<API.Dto.AbilityType, API.Dto.PrimaryStat>
            {
                {API.Dto.AbilityType.Cha, new API.Dto.PrimaryStat { Id = API.Dto.AbilityType.Cha, AbilityScore = 30}}
            };

            bool called = false;
            _primaryStatsService.PrimaryStatsUpdated += (sender, args) => called = true;

            //Act
            _primaryStatsService.UpdatePrimaryStat(updateRequest);

            //Assert
            called.Should().BeTrue();
        }

        private IEnumerable<API.Dto.PrimaryStat> GenerateOutOfOrderPrimaryStats()
        {
            return new List<API.Dto.PrimaryStat>
            {
                new API.Dto.PrimaryStat { Id = API.Dto.AbilityType.Int, AbilityScore = 10},
                new API.Dto.PrimaryStat { Id = API.Dto.AbilityType.Con, AbilityScore = 10 },
                new API.Dto.PrimaryStat { Id = API.Dto.AbilityType.Cha, AbilityScore = 10 },
                new API.Dto.PrimaryStat { Id = API.Dto.AbilityType.Wis, AbilityScore = 10 },
                new API.Dto.PrimaryStat { Id = API.Dto.AbilityType.Str, AbilityScore = 10 },
                new API.Dto.PrimaryStat { Id = API.Dto.AbilityType.Dex, AbilityScore = 10 },
            };
        }

        private IEnumerable<API.Dto.PrimaryStat> GetCorrectSequenceSvcPrimaryStats()
        {
            return  new List<API.Dto.PrimaryStat>
            {
                new API.Dto.PrimaryStat { Id = API.Dto.AbilityType.Str, AbilityScore = 10 },
                new API.Dto.PrimaryStat { Id = API.Dto.AbilityType.Dex, AbilityScore = 10 },
                new API.Dto.PrimaryStat { Id = API.Dto.AbilityType.Con, AbilityScore = 10 },
                new API.Dto.PrimaryStat { Id = API.Dto.AbilityType.Int, AbilityScore = 10 },
                new API.Dto.PrimaryStat { Id = API.Dto.AbilityType.Wis, AbilityScore = 10 },
                new API.Dto.PrimaryStat { Id = API.Dto.AbilityType.Cha, AbilityScore = 10 },
            };
        }
    }
}
