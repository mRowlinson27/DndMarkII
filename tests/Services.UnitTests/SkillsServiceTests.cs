
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
    using NUnit.Framework;
    using Utilities.API;

    [TestFixture]
    public class SkillsServiceTests
    {
        private SkillsService _skillsService;

        private ILogger _logger;
        private ISkillsRepo _skillsRepo;
        private ISvcAutoMapper _svcAutoMapper;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _skillsRepo = A.Fake<ISkillsRepo>();
            _svcAutoMapper = A.Fake<ISvcAutoMapper>();

            _skillsService = new SkillsService(_logger, _skillsRepo, _svcAutoMapper);
        }

        [Test]
        public async Task GetAllSkillsAsync_GetsFromDatabase()
        {
            //Arrange
            var skillId = new Guid();

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

            var correctSvcSkills = new List<Services.API.Dto.Skill>
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
            A.CallTo(() => _svcAutoMapper.Map(dbSkills)).Returns(correctSvcSkills);

            //Act
            var result = await _skillsService.GetAllSkillsAsync();

            //Assert
            result.Should().BeEquivalentTo(correctSvcSkills);
        }

        [Test]
        public async Task GetAllSkillsAsync_CalculatesTotal_AddsRanks()
        {
            //Arrange
            var correctSvcSkills = new List<Services.API.Dto.Skill>
            {
                new API.Dto.Skill
                {
                    Id = new Guid(),
                    Name = "Skill1",
                    PrimaryStatId = API.Dto.AbilityType.Cha,
                    HasArmourCheckPenalty = false,
                    Ranks = 5,
                    Trained = false,
                    UseUntrained = false
                }
            };

            A.CallTo(() => _svcAutoMapper.Map(A<IEnumerable<Skill>>.Ignored)).Returns(correctSvcSkills);

            //Act
            var result = await _skillsService.GetAllSkillsAsync();
            var firstResult = result.FirstOrDefault();

            //Assert
            firstResult.Total.Should().Be(5);
        }

        [TestCase(true, 1, 4)]
        [TestCase(false, 1, 1)]
        [TestCase(true, 0, 0)]
        [TestCase(true, 10, 13)]
        public async Task GetAllSkillsAsync_CalculatesTotal_Adds3ForTrainedWithAtLeastOneRank(bool trained, int ranks, int total)
        {
            //Arrange
            var correctSvcSkills = new List<Services.API.Dto.Skill>
            {
                new API.Dto.Skill
                {
                    Id = new Guid(),
                    Name = "Skill1",
                    PrimaryStatId = API.Dto.AbilityType.Cha,
                    HasArmourCheckPenalty = false,
                    Ranks = ranks,
                    Trained = trained,
                    UseUntrained = false
                }
            };

            A.CallTo(() => _svcAutoMapper.Map(A<IEnumerable<Skill>>.Ignored)).Returns(correctSvcSkills);

            //Act
            var result = await _skillsService.GetAllSkillsAsync();
            var firstResult = result.FirstOrDefault();

            //Assert
            firstResult.Total.Should().Be(total);
        }
    }
}
