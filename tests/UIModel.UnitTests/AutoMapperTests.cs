
namespace UIModel.UnitTests
{
    using System;
    using System.Collections.Generic;
    using API.Dto;
    using FluentAssertions;
    using NUnit.Framework;
    using Services.API.Dto;

    [TestFixture]
    public class AutoMapperTests
    {
        private AutoMapper _autoMapper;

        [SetUp]
        public void Setup()
        {
            _autoMapper = new AutoMapper();
        }

        [Test]
        [TestCase(0, "0")]
        [TestCase(1, "+1")]
        [TestCase(-1, "-1")]
        [TestCase(6, "+6")]
        public void MapPrimaryStat_TransformsDataProperly(int abilityMod, string uiAbilityMod)
        {
            //Arrange
            var inputData = new List<PrimaryStat>
            {
                new PrimaryStat
                {
                    Id = AbilityType.Cha,
                    AbilityModifier = abilityMod,
                    AbilityScore = 10,
                    Name = "Charisma"
                }
            };

            var correctUiPrimaryStats = new List<UiPrimaryStat>
            {
                new UiPrimaryStat()
                {
                    AbilityModifier = uiAbilityMod,
                    AbilityScore = "10",
                    Name = "Charisma",
                    ShortName = "CHA"
                }
            };

            //Act
            var result = _autoMapper.Map(inputData);

            //Assert
            result.Should().BeEquivalentTo(correctUiPrimaryStats);
        }

        [Test]
        public void MapSkill_TransformsRegularDataProperly()
        {
            //Arrange
            var skillId = Guid.NewGuid();
            var inputData = new List<Skill>
            {
                new Skill
                {
                    Id = skillId,
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

            var correctUiSkills = new List<UiSkill>
            {
                new UiSkill
                {
                    Id = skillId,
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

            //Act
            var result = _autoMapper.Map(inputData);

            //Assert
            result.Should().BeEquivalentTo(correctUiSkills);
        }
    }
}
