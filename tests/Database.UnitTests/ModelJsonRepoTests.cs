
namespace Database.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        private Skill _skill1;
        private Skill _skill2;

        [SetUp]
        public void Setup()
        {
            _jsonFile = A.Fake<IJsonFile<Model>>();

            _modelJsonRepo = new ModelJsonRepo(_jsonFile);

            GenerateBasicModel();
        }

        [Test]
        public void RequestX_OnlyReadsFromJsonFileOnce()
        {
            //Arrange
            A.CallTo(() => _jsonFile.Read()).Returns(_model);

            //Act
             _modelJsonRepo.GetPrimaryStats();
             _modelJsonRepo.GetPrimaryStats();
             _modelJsonRepo.GetSkills();
             _modelJsonRepo.GetSkills();

            //Assert
            A.CallTo(() => _jsonFile.Read()).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void GetPrimaryStats_ReturnsJsonFileResult()
        {
            //Arrange
            A.CallTo(() => _jsonFile.Read()).Returns(_model);

            //Act
            var skills = _modelJsonRepo.GetPrimaryStats();

            //Assert
            skills.Should().BeEquivalentTo(_primaryStats);
        }

        [Test]
        public void UpdatePrimaryStats_NextGetHasNewValues()
        {
            //Arrange
            A.CallTo(() => _jsonFile.Read()).Returns(_model);

            var newPrimaryStats = new List<PrimaryStat>
            {
                new PrimaryStat {Name = "PrimaryStat2"}
            };

            //Act
            _modelJsonRepo.UpdatePrimaryStats(newPrimaryStats);
            var primaryStats = _modelJsonRepo.GetPrimaryStats();

            //Assert
            primaryStats.Should().BeEquivalentTo(newPrimaryStats);
        }

        [Test]
        public void GetSkills_ReturnsJsonFileResult()
        {
            //Arrange
            A.CallTo(() => _jsonFile.Read()).Returns(_model);

            //Act
            var skills = _modelJsonRepo.GetSkills();

            //Assert
            skills.Should().BeEquivalentTo(_skills);
        }

        [Test]
        public void UpdateSkills_NextGetHasNewValues()
        {
            //Arrange
            A.CallTo(() => _jsonFile.Read()).Returns(_model);

            var newSkills = new List<Skill>
            {
                new Skill {Name = "Skill2"}
            };

            //Act
            _modelJsonRepo.UpdateSkills(newSkills);
            var skills = _modelJsonRepo.GetSkills();

            //Assert
            skills.Should().BeEquivalentTo(newSkills);
        }

        [Test]
        public void AddSkill_AddsUniqueKeys()
        {
            //Arrange
            A.CallTo(() => _jsonFile.Read()).Returns(_model);

            var correctResult = new List<Skill>
            {
                _skill1,
                _skill2
            };

            //Act
            _modelJsonRepo.AddSkill(_skill2);
            var skills = _modelJsonRepo.GetSkills();

            //Assert
            skills.Should().BeEquivalentTo(correctResult);
        }

        [Test]
        public void AddSkill_CannotAddSameKey()
        {
            //Arrange
            A.CallTo(() => _jsonFile.Read()).Returns(_model);

            //Act
            Assert.Throws<ArgumentException>(() => _modelJsonRepo.AddSkill(_skill1));

            //Assert
        }

        private void GenerateBasicModel()
        {
            _primaryStats = new List<PrimaryStat>
            {
                new PrimaryStat {Name = "PrimaryStat1"}
            };

            _skill1 = new Skill
            {
                Id = Guid.NewGuid()
            };

            _skill2 = new Skill
            {
                Id = Guid.NewGuid()
            };

            _skills = new List<Skill> {_skill1};

            var skillDict = _skills.ToDictionary(skill => skill.Id);

            _model = new Model
            {
                PrimaryStats = _primaryStats,
                Skills = skillDict
            };
        }
    }
}
