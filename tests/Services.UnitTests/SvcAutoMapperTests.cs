
namespace Services.UnitTests
{
    using System;
    using System.Collections.Generic;
    using Database.API.Dto;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class SvcAutoMapperTests
    {
        private SvcAutoMapper _svcAutoMapper;

        [SetUp]
        public void Setup()
        {
            _svcAutoMapper = new SvcAutoMapper();
        }

        [Test]
        public void MapPrimaryStat_TransformsDataCorrectly()
        {
            //Arrange
            var dbPrimaryStats = new List<PrimaryStat>
            {
                new PrimaryStat
                {
                    Id = AbilityType.Cha,
                    AbilityScore = 10,
                    Name = "Charisma"
                }
            };

            var correctSvcPrimaryStats = new List<Services.API.Dto.PrimaryStat>
            {
                new API.Dto.PrimaryStat
                {
                    Id = API.Dto.AbilityType.Cha,
                    AbilityScore = 10,
                    Name = "Charisma"
                }
            };

            //Act
            var result = _svcAutoMapper.Map(dbPrimaryStats);

            //Assert
            result.Should().BeEquivalentTo(correctSvcPrimaryStats);
        }

        [Test]
        public void MapSkills_TransformsDataCorrectly()
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

            //Act
            var result = _svcAutoMapper.Map(dbSkills);

            //Assert
            result.Should().BeEquivalentTo(correctSvcSkills);
        }
    }
}
