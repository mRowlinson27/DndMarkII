
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
        public void MapPrimaryStatToUi_TransformsDataProperly(int abilityMod, string uiAbilityMod)
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
            var result = _autoMapper.MapToUi(inputData);

            //Assert
            result.Should().BeEquivalentTo(correctUiPrimaryStats);
        }

        [Test]
        [TestCase(0, "0")]
        [TestCase(1, "+1")]
        [TestCase(-1, "-1")]
        [TestCase(6, "+6")]
        public void MapSkillToUi_TransformsRegularDataProperly(int abilityMod, string uiAbilityMod)
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
                    Class = true,
                    UseUntrained = true,
                    Total = 3,
                    PrimaryStatModifier = abilityMod
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
                    Class = true,
                    UseUntrained = true,
                    Total = 3,
                    PrimaryStatModifier = uiAbilityMod
                }
            };

            //Act
            var result = _autoMapper.MapToUi(inputData);

            //Assert
            result.Should().BeEquivalentTo(correctUiSkills);
        }

        [Test]
        public void MapPrimaryStatToSvcRequest_TransformsDataProperly()
        {
            //Arrange
            var inputData = new List<UiPrimaryStat>
            {
                new UiPrimaryStat()
                {
                    AbilityModifier = "0",
                    AbilityScore = "10",
                    Name = "Charisma",
                    ShortName = "CHA"
                }
            };

            var correctSvcPrimaryStats = new List<PrimaryStatUpdateRequest>
            {
                new PrimaryStatUpdateRequest
                {
                    Id = AbilityType.Cha,
                    AbilityScore = 10,
                }
            };

            //Act
            var result = _autoMapper.MapToSvcRequest(inputData);

            //Assert
            result.Should().BeEquivalentTo(correctSvcPrimaryStats);
        }

        [Test]
        public void MapSkillToSvcRequest_TransformsDataProperly()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var inputData = new List<UiSkill>
            {
                new UiSkill()
                {
                    Id = guid,
                    Ranks = 10,
                    Class = true
                }
            };

            var correctSkillUpdateRequests = new List<SkillUpdateRequest>
            {
                new SkillUpdateRequest
                {
                    Id = guid,
                    Ranks = 10,
                    Class = true
                }
            };

            //Act
            var result = _autoMapper.MapToSvcRequest(inputData);

            //Assert
            result.Should().BeEquivalentTo(correctSkillUpdateRequests);
        }
    }
}
