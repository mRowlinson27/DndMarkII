
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
        public void RequestSkills_GetsFromServiceAndReturnsCorrectly()
        {
            //Arrange
            var svcData = new List<Skill>
            {
                new Skill
                {
                    Id = Guid.NewGuid(),
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

            A.CallTo(() => _skillsService.GetAllSkills()).Returns(svcData);
            A.CallTo(() => _autoMapper.Map(svcData)).Returns(uiSkills);

            //Act
            var result = _skillTableModel.RequestSkills();

            //Assert
            result.Should().BeEquivalentTo(uiSkills);
        }

        [Test]
        public void AddSkill_AddsBlankSkill()
        {
            //Arrange
            var blankSkill = new Skill
            {
                ArmourCheckPenalty = 0,
                HasArmourCheckPenalty = false,
                Name = "",
                PrimaryStatId = AbilityType.Str,
                Ranks = 0,
                Trained = false,
                UseUntrained = true
            };

            //Act
            _skillTableModel.AddSkill();

            //Assert
            A.CallTo(() => _skillsService.AddSkill(A<Skill>.That.Matches(s =>
                s.ArmourCheckPenalty == blankSkill.ArmourCheckPenalty &&
                s.HasArmourCheckPenalty == blankSkill.HasArmourCheckPenalty &&
                s.Name == blankSkill.Name &&
                s.PrimaryStatId == blankSkill.PrimaryStatId &&
                s.Ranks == blankSkill.Ranks &&
                s.UseUntrained == blankSkill.UseUntrained))).MustHaveHappened();
        }

        [Test]
        public void SkillsUpdated_PropertyChangedInvoked()
        {
            //Arrange
            var wasCalled = false;
            _skillTableModel.PropertyChanged += (o, e) => wasCalled = true;

            //Act
            _skillsService.SkillsUpdated += Raise.WithEmpty();

            //Assert
            wasCalled.Should().BeTrue();
        }
    }
}
