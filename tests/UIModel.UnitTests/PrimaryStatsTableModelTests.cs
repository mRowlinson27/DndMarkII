﻿
namespace UIModel.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
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
    public class PrimaryStatsTableModelTests
    {
        private PrimaryStatsTableModel _primaryStatsTableModel;

        private ILogger _logger;
        private IPrimaryStatsService _primaryStatsService;
        private IAutoMapper _autoMapper;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _primaryStatsService = A.Fake<IPrimaryStatsService>();
            _autoMapper = A.Fake<IAutoMapper>();

            _primaryStatsTableModel = new PrimaryStatsTableModel(_logger, _primaryStatsService, _autoMapper);
        }

        [Test]
        public async Task RequestPrimaryStatsAsync_ReturnsDataProperly()
        {
            //Arrange
            var svcData = new List<PrimaryStat>
            {
                new PrimaryStat
                {
                    Id = AbilityType.Cha,
                    AbilityModifier = 5,
                    AbilityScore = 10,
                    Name = "Charisma"
                }
            };

            var correctUiPrimaryStats = new List<UiPrimaryStat>
            {
                new UiPrimaryStat()
                {
                    AbilityModifier = "+5",
                    AbilityScore = "10",
                    Name = "Charisma",
                    ShortName = "CHA"
                }
            };

            A.CallTo(() => _primaryStatsService.GetAllPrimaryStatsAsync()).Returns(svcData);
            A.CallTo(() => _autoMapper.Map(svcData)).Returns(correctUiPrimaryStats);

            //Act
            var result = await _primaryStatsTableModel.RequestPrimaryStatsAsync();

            //Assert
            result.Should().BeEquivalentTo(correctUiPrimaryStats);
        }
    }
}