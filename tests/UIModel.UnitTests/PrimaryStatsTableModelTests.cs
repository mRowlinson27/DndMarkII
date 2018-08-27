
namespace UIModel.UnitTests
{
    using FakeItEasy;
    using NUnit.Framework;
    using Utilities.API;

    [TestFixture]
    public class PrimaryStatsTableModelTests
    {
        private PrimaryStatsTableModel _primaryStatsTableModel;

        private ILogger _logger;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();

            _primaryStatsTableModel = new PrimaryStatsTableModel(_logger);
        }
    }
}
