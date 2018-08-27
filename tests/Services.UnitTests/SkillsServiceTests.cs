
namespace Services.UnitTests
{
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

        private List<Skill> _skills;

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
            A.CallTo(() => _skillsRepo.GetSkillsAsync()).Returns(_skills);

            //Act
            var result = await _skillsService.GetAllSkillsAsync();

            //Assert
            result.Should().BeEquivalentTo(_skills);
        }

        private void GenerateBasicModel()
        {
            _skills = new List<Skill>
            {
                new Skill {Name = "Skill1"}
            };
        }
    }
}
