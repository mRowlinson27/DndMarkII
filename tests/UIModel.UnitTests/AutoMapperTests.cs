
namespace UIModel.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using API.Dto;
    using FakeItEasy;
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
        public void RequestPrimaryStatsAsync_TransformsDataProperly(int abilityMod, string uiAbilityMod)
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
    }
}
