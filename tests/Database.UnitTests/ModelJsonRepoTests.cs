﻿
namespace Database.UnitTests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using API.Dto;
    using FakeItEasy;
    using FluentAssertions;
    using NUnit.Framework;
    using Utilities.API.DAL;

    [TestFixture]
    public class ModelJsonRepoTests
    {
        private ModelJsonRepo _modelJsonRepo;

        private IJsonFile<Model> _jsonFile;

        private Model _model;

        private List<PrimaryStat> _primaryStats;

        private List<Skill> _skills;

        [SetUp]
        public void Setup()
        {
            _jsonFile = A.Fake<IJsonFile<Model>>();

            _modelJsonRepo = new ModelJsonRepo(_jsonFile);

            GenerateBasicModel();
        }

        [Test]
        public async Task RequestX_OnlyReadsFromJsonFileOnce()
        {
            //Arrange
            A.CallTo(() => _jsonFile.ReadAsync()).Returns(_model);

            //Act
            await  _modelJsonRepo.GetPrimaryStatsAsync();
            await  _modelJsonRepo.GetPrimaryStatsAsync();
            await  _modelJsonRepo.GetSkillsAsync();
            await  _modelJsonRepo.GetSkillsAsync();

            //Assert
            A.CallTo(() => _jsonFile.ReadAsync()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task GetPrimaryStatsAsync_ReturnsJsonFileResult()
        {
            //Arrange
            A.CallTo(() => _jsonFile.ReadAsync()).Returns(_model);

            //Act
            var skills = await _modelJsonRepo.GetPrimaryStatsAsync();

            //Assert
            skills.Should().BeEquivalentTo(_primaryStats);
        }

        [Test]
        public async Task UpdatePrimaryStatsAsync_NextGetHasNewValues()
        {
            //Arrange
            A.CallTo(() => _jsonFile.ReadAsync()).Returns(_model);

            var newPrimaryStats = new List<PrimaryStat>
            {
                new PrimaryStat {Name = "PrimaryStat2"}
            };

            //Act
            await _modelJsonRepo.UpdatePrimaryStatsAsync(newPrimaryStats);
            var primaryStats = await _modelJsonRepo.GetPrimaryStatsAsync();

            //Assert
            primaryStats.Should().BeEquivalentTo(newPrimaryStats);
        }

        [Test]
        public async Task GetSkillsAsync_ReturnsJsonFileResult()
        {
            //Arrange
            A.CallTo(() => _jsonFile.ReadAsync()).Returns(_model);

            //Act
            var skills = await _modelJsonRepo.GetSkillsAsync();

            //Assert
            skills.Should().BeEquivalentTo(_skills);
        }

        [Test]
        public async Task UpdateSkillsAsync_NextGetHasNewValues()
        {
            //Arrange
            A.CallTo(() => _jsonFile.ReadAsync()).Returns(_model);

            var newSkills = new List<Skill>
            {
                new Skill {Name = "Skill2"}
            };

            //Act
            await _modelJsonRepo.UpdateSkillsAsync(newSkills);
            var skills = await _modelJsonRepo.GetSkillsAsync();

            //Assert
            skills.Should().BeEquivalentTo(newSkills);
        }

        private void GenerateBasicModel()
        {
            _primaryStats = new List<PrimaryStat>
            {
                new PrimaryStat {Name = "PrimaryStat1"}
            };

            _skills = new List<Skill>
            {
                new Skill {Name = "Skill1"}
            };

            _model = new Model
            {
                PrimaryStats = _primaryStats,
                Skills = _skills
            };
        }
    }
}