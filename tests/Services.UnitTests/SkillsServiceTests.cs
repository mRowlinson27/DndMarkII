
namespace Services.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using API;
    using API.Dto;
    using Database.API;
    using FakeItEasy;
    using FluentAssertions;
    using FluentAssertions.Common;
    using NUnit.Framework;
    using Utilities.API;
    using AbilityType = Database.API.Dto.AbilityType;
    using Skill = Database.API.Dto.Skill;

    [TestFixture]
    public class SkillsServiceTests
    {
        private SkillsService _skillsService;

        private ILogger _logger;
        private ISkillsRepo _skillsRepo;
        private ISvcAutoMapper _svcAutoMapper;
        private ISkillTotalCalculator _skillTotalCalculator;
        private IPrimaryStatsService _primaryStatsService;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _skillsRepo = A.Fake<ISkillsRepo>();
            _svcAutoMapper = A.Fake<ISvcAutoMapper>();
            _skillTotalCalculator = A.Fake<ISkillTotalCalculator>();
            _primaryStatsService = A.Fake<IPrimaryStatsService>();

            _skillsService = new SkillsService(_logger, _skillsRepo, _svcAutoMapper, _skillTotalCalculator, _primaryStatsService);
        }

        [Test]
        public void GetAllSkills_GetsFromDatabaseAndAddsTotals()
        {
            //Arrange
            var skillId = Guid.NewGuid();

            var dbSkills = new List<Skill>
            {
                new Skill
                {
                    Id = skillId,
                    Name = "Skill1",
                    PrimaryStatId  = AbilityType.Cha,
                    HasArmourCheckPenalty = true,
                    Ranks = 5,
                    Trained = true,
                    UseUntrained = true
                }
            };

            var correctSvcSkills = new List<API.Dto.Skill>
            {
                new API.Dto.Skill
                {
                    Id = skillId,
                    Name = "Skill1",
                    PrimaryStatId = API.Dto.AbilityType.Cha,
                    HasArmourCheckPenalty = true,
                    Ranks = 5,
                    Class = true,
                    UseUntrained = true
                }
            };

            A.CallTo(() => _skillsRepo.GetSkills()).Returns(dbSkills);
            A.CallTo(() => _svcAutoMapper.MapToSvc(dbSkills)).Returns(correctSvcSkills);
            A.CallTo(() => _skillTotalCalculator.AddTotals(correctSvcSkills)).Returns(correctSvcSkills);

            //Act
            var result = _skillsService.GetAllSkills();

            //Assert
            result.Should().BeEquivalentTo(correctSvcSkills);
        }

        [Test]
        public void GetAllSkills_PreservesOrder()
        {
            //Arrange
            var correctSvcSkills = new List<API.Dto.Skill>
            {
                new API.Dto.Skill { Id = Guid.NewGuid() },
                new API.Dto.Skill { Id = Guid.NewGuid() },
                new API.Dto.Skill { Id = Guid.NewGuid() },
                new API.Dto.Skill { Id = Guid.NewGuid() },
            };
            A.CallTo(() => _skillTotalCalculator.AddTotals(A<IEnumerable<API.Dto.Skill>>.Ignored)).Returns(correctSvcSkills);

            //Act
            var result = _skillsService.GetAllSkills();

            //Assert
            result.Should().BeEquivalentTo(correctSvcSkills);
        }

        [Test]
        public void GetAllSkills_GetsFromDatabaseOnce()
        {
            //Arrange

            //Act
            _skillsService.GetAllSkills();
            _skillsService.GetAllSkills();

            //Assert
            A.CallTo(() => _skillsRepo.GetSkills()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void AddSkill_AppendsNewSkill()
        {
            //Arrange
            var skillId = Guid.NewGuid();
            var newSkill = new API.Dto.Skill
            {
                Id = skillId,
                Name = "Skill1",
                PrimaryStatId = API.Dto.AbilityType.Cha,
                HasArmourCheckPenalty = true,
                Ranks = 5,
                Class = true,
                UseUntrained = true,
                Total = 8
            };

            _skillsService.CachedSvcSkills = new Dictionary<Guid, API.Dto.Skill>();

            //Act
            _skillsService.AddSkill(newSkill);
            var result = _skillsService.GetAllSkills();

            //Assert
            result.FirstOrDefault().Should().Be(newSkill);
        }

        [Test]
        public void AddSkill_RaisesSkillsUpdated()
        {
            //Arrange
            var wasCalled = false;
            _skillsService.SkillsUpdated += (o, e) => wasCalled = true;
            _skillsService.CachedSvcSkills = new Dictionary<Guid, API.Dto.Skill>();

            //Act
            _skillsService.AddSkill(new API.Dto.Skill());

            //Assert
            wasCalled.Should().BeTrue();
        }

        [Test]
        public void PrimaryStatsService_RaisesPrimaryStatsUpdated_UpdatesSkills()
        {
            //Arrange
            var wasCalled = false;
            _skillsService.SkillsUpdated += (o, e) => wasCalled = true;
            _skillsService.CachedSvcSkills = new Dictionary<Guid, API.Dto.Skill>();

            //Act
            _primaryStatsService.PrimaryStatsUpdated += Raise.FreeForm<EventHandler>.With(_primaryStatsService, EventArgs.Empty);

            //Assert
            wasCalled.Should().BeTrue();
            A.CallTo(() => _skillTotalCalculator.AddTotals(A<IEnumerable<API.Dto.Skill>>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void UpdateSkill_WhenRequested_HasUpdatedInfo()
        {
            //Arrange
            var guid = Guid.NewGuid();
            _skillsService.CachedSvcSkills = new Dictionary<Guid, API.Dto.Skill>
            {
                {guid, new API.Dto.Skill {Id = guid, Ranks = 0}}
            };

            var correctSkill = new API.Dto.Skill
            {
                Id = guid,
                Ranks = 4,
                Class = true
            };

            //Act
            _skillsService.UpdateSkill(new SkillUpdateRequest { Id = guid, Ranks = 4, Class = true});
            var result = _skillsService.GetAllSkills();

            //Assert
            result.FirstOrDefault().Should().BeEquivalentTo(correctSkill);
        }

        [Test]
        public void UpdateSkill_CalculatesTotalForUpdatedSkill()
        {
            //Arrange
            var guid = Guid.NewGuid();
            _skillsService.CachedSvcSkills = new Dictionary<Guid, API.Dto.Skill>
            {
                {guid, new API.Dto.Skill {Id = guid, Ranks = 0}}
            };

            //Act
            _skillsService.UpdateSkill(new SkillUpdateRequest { Id = guid, Ranks = 4 });

            //Assert
            A.CallTo(() => _skillTotalCalculator.AddTotal(A<API.Dto.Skill>.That.Matches(skill => 
                skill.Id == guid &&
                skill.Ranks == 4))).MustHaveHappened();
        }

        [Test]
        public void UpdateSkill_RaisesSkillsUpdated()
        {
            //Arrange
            var wasCalled = false;
            _skillsService.SkillsUpdated += (o, e) => wasCalled = true;

            var guid = Guid.NewGuid();
            _skillsService.CachedSvcSkills = new Dictionary<Guid, API.Dto.Skill>
            {
                {guid, new API.Dto.Skill {Id = guid, Ranks = 0}}
            };

            //Act
            _skillsService.UpdateSkill(new SkillUpdateRequest {Id = guid, Ranks = 4});

            //Assert
            wasCalled.Should().BeTrue();
        }
    }
}
