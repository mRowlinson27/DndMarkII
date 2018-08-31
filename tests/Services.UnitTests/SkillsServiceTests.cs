
namespace Services.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using API;
    using Database.API;
    using Database.API.Dto;
    using FakeItEasy;
    using FluentAssertions;
    using FluentAssertions.Common;
    using NUnit.Framework;
    using Utilities.API;

    [TestFixture]
    public class SkillsServiceTests
    {
        private SkillsService _skillsService;

        private ILogger _logger;
        private ISkillsRepo _skillsRepo;
        private ISvcAutoMapper _svcAutoMapper;
        private ISkillTotalCalculator _skillTotalCalculator;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _skillsRepo = A.Fake<ISkillsRepo>();
            _svcAutoMapper = A.Fake<ISvcAutoMapper>();
            _skillTotalCalculator = A.Fake<ISkillTotalCalculator>();

            _skillsService = new SkillsService(_logger, _skillsRepo, _svcAutoMapper, _skillTotalCalculator);
        }

        [Test]
        public async Task GetAllSkillsAsync_GetsFromDatabaseAndAddsTotals()
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
                    Trained = true,
                    UseUntrained = true
                }
            };

            A.CallTo(() => _skillsRepo.GetSkillsAsync()).Returns(dbSkills);
            A.CallTo(() => _svcAutoMapper.MapToSvc(dbSkills)).Returns(correctSvcSkills);
            A.CallTo(() => _skillTotalCalculator.AddTotalsAsync(correctSvcSkills)).Returns(correctSvcSkills);

            //Act
            var result = await _skillsService.GetAllSkillsAsync();

            //Assert
            result.Should().BeEquivalentTo(correctSvcSkills);
        }

        [Test]
        public async Task AddSkillAsync_GetsCurrentDb_AppendsNewSkill()
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
                Trained = true,
                UseUntrained = true
            };

            var autoMapperResult = new Skill
            {
                Id = skillId,
                Name = "Skill1",
                PrimaryStatId = AbilityType.Cha,
                HasArmourCheckPenalty = true,
                Ranks = 5,
                Trained = true,
                UseUntrained = true
            };

            A.CallTo(() => _svcAutoMapper.MapToDb(newSkill)).Returns(autoMapperResult);
            
            //Act
            await _skillsService.AddSkillAsync(newSkill);

            //Assert
            A.CallTo(() => _skillsRepo.AddSkillAsync(autoMapperResult)).MustHaveHappened();
        }

        [Test]
        public async Task AddSkillAsync_RaisesSkillsUpdated()
        {
            //Arrange
            var wasCalled = false;
            _skillsService.SkillsUpdated += (o, e) => wasCalled = true;

            //Act
            await _skillsService.AddSkillAsync(new API.Dto.Skill());

            //Assert
            wasCalled.Should().BeTrue();
        }
    }
}
