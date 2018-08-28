
namespace UIModel.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using API;
    using API.Dto;
    using FakeItEasy;
    using FluentAssertions;
    using NUnit.Framework;
    using Services.API;
    using Services.API.Dto;
    using Utilities.API;

    [TestFixture]
    public class SkillTableModelTests
    {
        private SkillTableModel _skillTableModel;

        private ILogger _logger;
        private ISkillsService _skillsService;
        private IAutoMapper _autoMapper;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _skillsService = A.Fake<ISkillsService>();
            _autoMapper = A.Fake<IAutoMapper>();

            _skillTableModel = new SkillTableModel(_logger, _skillsService, _autoMapper);
        }

        [Test]
        public async Task RequestSkillsAsync_GetsFromServiceAndReturnsCorrectly()
        {
            //Arrange
            var svcData = new List<Skill>
            {
                new Skill
                {
                    Id = new Guid(),
                    ArmourCheckPenalty = 1,
                    HasArmourCheckPenalty = true,
                    Name = "Acro",
                    PrimaryStatId = AbilityType.Cha,
                    Ranks = 1,
                    Trained = true,
                    UseUntrained = true,
                    Total = 3
                }
            };

            var uiSkills = new List<UiSkill>
            {
                new UiSkill
                {
                    ArmourCheckPenalty = 1,
                    HasArmourCheckPenalty = true,
                    Name = "Acro",
                    PrimaryStatName = "CHA",
                    Ranks = 1,
                    Trained = true,
                    UseUntrained = true,
                    Total = 3
                }
            };

            A.CallTo(() => _skillsService.GetAllSkillsAsync()).Returns(svcData);
            A.CallTo(() => _autoMapper.Map(svcData)).Returns(uiSkills);

            //Act
            var result = await _skillTableModel.RequestSkillsAsync();

            //Assert
            result.Should().BeEquivalentTo(uiSkills);
        }
    }
}
