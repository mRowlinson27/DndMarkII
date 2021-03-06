﻿
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
        public void MapToSvcPrimaryStat_TransformsDataCorrectly()
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
            var result = _svcAutoMapper.MapToSvc(dbPrimaryStats);

            //Assert
            result.Should().BeEquivalentTo(correctSvcPrimaryStats);
        }

        [Test]
        public void MapToSvcSkills_TransformsDataCorrectly()
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

            var correctSvcSkills = new List<Services.API.Dto.Skill>
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

            //Act
            var result = _svcAutoMapper.MapToSvc(dbSkills);

            //Assert
            result.Should().BeEquivalentTo(correctSvcSkills);
        }

        [Test]
        public void MapToDbPrimaryStat_TransformsDataCorrectly()
        {
            //Arrange
            var svcPrimaryStats = new List<Services.API.Dto.PrimaryStat>
            {
                new API.Dto.PrimaryStat
                {
                    Id = API.Dto.AbilityType.Cha,
                    AbilityScore = 10,
                    Name = "Charisma"
                }
            };

            var correctDbPrimaryStats = new List<PrimaryStat>
            {
                new PrimaryStat
                {
                    Id = AbilityType.Cha,
                    AbilityScore = 10,
                    Name = "Charisma"
                }
            };

            //Act
            var result = _svcAutoMapper.MapToDb(svcPrimaryStats);

            //Assert
            result.Should().BeEquivalentTo(correctDbPrimaryStats);
        }

        [Test]
        public void MapToDbSkills_TransformsDataCorrectly()
        {
            //Arrange
            var skillId = Guid.NewGuid();
            var svcSkills = new List<Services.API.Dto.Skill>
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
            var correctDbSkills = new List<Skill>
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

            

            //Act
            var result = _svcAutoMapper.MapToDb(svcSkills);

            //Assert
            result.Should().BeEquivalentTo(correctDbSkills);
        }
    }
}
