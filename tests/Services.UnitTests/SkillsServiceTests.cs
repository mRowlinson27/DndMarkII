
namespace Services.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
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

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _skillsRepo = A.Fake<ISkillsRepo>();

            _skillsService = new SkillsService(_logger, _skillsRepo);
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

            //Act
            var result = await _skillsService.GetAllSkillsAsync();

            //Assert
            result.Should().BeEquivalentTo(correctSvcSkills);
        }
    }
}
