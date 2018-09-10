
namespace Services.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using API;
    using API.Dto;
    using FakeItEasy;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class SkillTotalCalculatorTests
    {
        private SkillTotalCalculator _skillTotalCalculator;

        private IPrimaryStatsService _primaryStatsService;

        [SetUp]
        public void Setup()
        {
            _primaryStatsService = A.Fake<IPrimaryStatsService>();

            _skillTotalCalculator = new SkillTotalCalculator(_primaryStatsService);
        }

        [Test]
        public void AddTotals_FactorsInRanks()
        {
            //Arrange
            var skills = new List<API.Dto.Skill>
            {
                new API.Dto.Skill
                {
                    Id = Guid.NewGuid(),
                    Name = "Skill1",
                    PrimaryStatId = API.Dto.AbilityType.Cha,
                    HasArmourCheckPenalty = false,
                    Ranks = 5,
                    Trained = false,
                    UseUntrained = false
                }
            };

            //Act
            var result = _skillTotalCalculator.AddTotals(skills);
            var firstResult = result.FirstOrDefault();

            //Assert
            firstResult.Total.Should().Be(5);
        }

        [Test]
        public void AddTotalsAsync_FactorsInAbilityScores()
        {
            //Arrange
            var skills = new List<API.Dto.Skill>
            {
                new API.Dto.Skill
                {
                    Id = Guid.NewGuid(),
                    Name = "Skill1",
                    PrimaryStatId = AbilityType.Cha,
                    HasArmourCheckPenalty = false,
                    Ranks = 5,
                    Trained = false,
                    UseUntrained = false
                }
            };
            var abilityScores = new List<PrimaryStat>
            {
                new PrimaryStat
                {
                    AbilityModifier = 5,
                    Id = AbilityType.Cha
                }
            };

            A.CallTo(() => _primaryStatsService.GetAllPrimaryStats()).Returns(abilityScores);

            //Act
            var result = _skillTotalCalculator.AddTotals(skills);
            var firstResult = result.FirstOrDefault();

            //Assert
            firstResult.Total.Should().Be(10);
        }

        [TestCase(true, 1, 4)]
        [TestCase(false, 1, 1)]
        [TestCase(true, 0, 0)]
        [TestCase(true, 10, 13)]
        public void AddTotals_Adds3ForTrainedWithAtLeastOneRank(bool trained, int ranks, int total)
        {
            //Arrange
            var skills = new List<API.Dto.Skill>
            {
                new API.Dto.Skill
                {
                    Id = Guid.NewGuid(),
                    Name = "Skill1",
                    PrimaryStatId = API.Dto.AbilityType.Cha,
                    HasArmourCheckPenalty = false,
                    Ranks = ranks,
                    Trained = trained,
                    UseUntrained = false
                }
            };

            //Act
            var result = _skillTotalCalculator.AddTotals(skills);
            var firstResult = result.FirstOrDefault();

            //Assert
            firstResult.Total.Should().Be(total);
        }
    }
}
