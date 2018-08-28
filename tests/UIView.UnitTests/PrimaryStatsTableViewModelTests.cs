
namespace UIView.UnitTests
{
    using System;
    using System.ComponentModel;
    using FakeItEasy;
    using NUnit.Framework;
    using UIModel.API;
    using UIUtilities;
    using UIUtilities.API;
    using UIUtilities.API.AsyncCommands;
    using UIUtilities.AsyncCommands;
    using Utilities.API;
    using ViewModel;

    [TestFixture]
    public class PrimaryStatsTableViewModelTests
    {
        private PrimaryStatsTableViewModel _primaryStatsTableViewModel;

        private ILogger _logger;
        private IPrimaryStatsTableModel _primaryStatsTableModel;
        private IObservableHelper _observableHelper;
        private IAsyncCommandFactory _asyncCommandFactory;
        private IAsyncTaskRunnerFactory _asyncTaskRunnerFactory;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _primaryStatsTableModel = A.Fake<IPrimaryStatsTableModel>();
            var notifyTaskCompletionFactory = A.Fake<INotifyTaskCompletionFactory>();

            _observableHelper = new ObservableHelper();
            _asyncCommandFactory = new AsyncCommandFactory(notifyTaskCompletionFactory);
            _asyncTaskRunnerFactory = new AsyncTaskRunnerFactory(notifyTaskCompletionFactory);

            _primaryStatsTableViewModel = new PrimaryStatsTableViewModel(_logger, _primaryStatsTableModel, _observableHelper, _asyncCommandFactory,
                _asyncTaskRunnerFactory, new UiThreadInvoker(_logger));
        }

        [TearDown]
        public void TearDown()
        {
            _primaryStatsTableViewModel.Dispose();
        }

        [Test]
        public void Init_RequestPrimaryStatsAsync()
        {
            //Arrange

            //Act
            _primaryStatsTableViewModel.Init();

            //Assert
            A.CallTo(() => _primaryStatsTableModel.RequestPrimaryStatsAsync()).MustHaveHappened();
        }

        [Test]
        public void Model_OnPropertyChanged_RequestPrimaryStatsAsync()
        {
            //Arrange

            //Act
            _primaryStatsTableModel.PrimaryStatsUpdated += Raise.With(_primaryStatsTableModel, new EventArgs());

            //Assert
            A.CallTo(() => _primaryStatsTableModel.RequestPrimaryStatsAsync()).MustHaveHappened();
        }
    }
}
