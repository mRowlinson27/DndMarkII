
namespace UIView.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using API;
    using FakeItEasy;
    using FluentAssertions;
    using NUnit.Framework;
    using UIModel.API;
    using UIModel.API.Dto;
    using UIUtilities;
    using UIUtilities.API;
    using UIUtilities.API.AsyncCommands;
    using UIUtilities.AsyncCommands;
    using Utilities.API;
    using Utilities.Implementation;
    using ViewModel;

    [TestFixture]
    public class PrimaryStatsTableViewModelTests
    {
        private PrimaryStatsTableViewModel _primaryStatsTableViewModel;

        private ILogger _logger;
        private IPrimaryStatsTableModel _fakePrimaryStatsTableModel;
        private INotifyTaskCompletionFactory _fakeNotifyTaskCompletionFactory;
        private IObservableHelper _observableHelper;
        private IAsyncCommandFactory _asyncCommandFactory;
        private IAsyncTaskRunnerFactory _asyncTaskRunnerFactory;
        private IPrimaryStatViewModelFactory _fakePrimaryStatViewModelFactory;

        private INotifyTaskCompletion<IEnumerable<UiPrimaryStat>> _dataRequestNotifyTaskCompletion;
        private IPrimaryStatViewModel _fakeViewModel0;
        private IPrimaryStatViewModel _fakeViewModel1;

        

        [TearDown]
        public void TearDown()
        {
            _primaryStatsTableViewModel.Dispose();
        }

        [Test]
        public void Init_RequestPrimaryStatsAsync()
        {
            //Arrange
            Setup();

            //Act
            _primaryStatsTableViewModel.Init();

            //Assert
            A.CallTo(() => _fakePrimaryStatsTableModel.RequestPrimaryStatsAsync()).MustHaveHappened();
        }

        [Test]
        public void Model_OnPropertyChanged_RequestPrimaryStatsAsync()
        {
            //Arrange
            Setup();

            //Act
            _fakePrimaryStatsTableModel.PrimaryStatsUpdated += Raise.With(_fakePrimaryStatsTableModel, new EventArgs());

            //Assert
            A.CallTo(() => _fakePrimaryStatsTableModel.RequestPrimaryStatsAsync()).MustHaveHappened();
        }

        [Test]
        public void Model_OnPrimaryStatsRequestTaskCompletion_CreatesPrimaryStatsViewModelsAndDataIsAvailable()
        {
            //Arrange
            Setup(A.Fake<INotifyTaskCompletion<IEnumerable<UiPrimaryStat>>>());
            SetUpModelToReturnFakeViewModels();

            //Act
            RaiseModelDataRetrievedSuccessfullyEvents();

            //Assert
            _primaryStatsTableViewModel.DataAvailable.Should().BeTrue();
            _primaryStatsTableViewModel.PrimaryStats[0].Should().Be(_fakeViewModel0);
            _primaryStatsTableViewModel.PrimaryStats[1].Should().Be(_fakeViewModel1);
        }

        public void Setup(INotifyTaskCompletion<IEnumerable<UiPrimaryStat>> dataRequestNotifyTaskCompletion = null)
        {
            SetupContantFakes();

            SetupNotifyTaskCompletion(dataRequestNotifyTaskCompletion);

            SetupPrimaryStatsViewModel();
        }

        public void SetupContantFakes()
        {
            _logger = A.Fake<ILogger>();
            _fakePrimaryStatsTableModel = A.Fake<IPrimaryStatsTableModel>();
            _fakeNotifyTaskCompletionFactory = A.Fake<INotifyTaskCompletionFactory>();
            _fakePrimaryStatViewModelFactory = A.Fake<IPrimaryStatViewModelFactory>();
        }

        private void SetupNotifyTaskCompletion(INotifyTaskCompletion<IEnumerable<UiPrimaryStat>> dataRequestNotifyTaskCompletion)
        {
            if (dataRequestNotifyTaskCompletion == null)
            {
                dataRequestNotifyTaskCompletion = new NotifyTaskCompletion<IEnumerable<UiPrimaryStat>>(_logger);
            }

            _dataRequestNotifyTaskCompletion = dataRequestNotifyTaskCompletion;

            A.CallTo(() => _fakeNotifyTaskCompletionFactory.Create<IEnumerable<UiPrimaryStat>>()).Returns(_dataRequestNotifyTaskCompletion);
        }

        public void SetupPrimaryStatsViewModel()
        {
            _observableHelper = new ObservableHelper();
            _asyncCommandFactory = new AsyncCommandFactory(_fakeNotifyTaskCompletionFactory, new UiStateController(_logger, new UiLockerContextFactory()), new TaskWrapper());
            _asyncTaskRunnerFactory = new AsyncTaskRunnerFactory(_fakeNotifyTaskCompletionFactory);

            _primaryStatsTableViewModel = new PrimaryStatsTableViewModel(_logger, _fakePrimaryStatsTableModel, _observableHelper, _asyncCommandFactory,
                _asyncTaskRunnerFactory, new UiThreadInvoker(_logger), _fakePrimaryStatViewModelFactory, new UiStateController(_logger, new UiLockerContextFactory()));
        }

        private void SetUpModelToReturnFakeViewModels()
        {
            A.CallTo(() => _dataRequestNotifyTaskCompletion.Result).Returns(new List<UiPrimaryStat> { new UiPrimaryStat(), new UiPrimaryStat() });

            _fakeViewModel0 = A.Fake<IPrimaryStatViewModel>();
            _fakeViewModel1 = A.Fake<IPrimaryStatViewModel>();
            A.CallTo(() => _fakePrimaryStatViewModelFactory.Create(A<UiPrimaryStat>.Ignored)).ReturnsNextFromSequence(_fakeViewModel0, _fakeViewModel1);
        }

        private void RaiseModelDataRetrievedSuccessfullyEvents()
        {
            _fakePrimaryStatsTableModel.PrimaryStatsUpdated += Raise.FreeForm<EventHandler>.With(_fakePrimaryStatsTableModel, EventArgs.Empty);
            _dataRequestNotifyTaskCompletion.PropertyChanged += Raise.FreeForm<PropertyChangedEventHandler>.With(_dataRequestNotifyTaskCompletion, new PropertyChangedEventArgs("IsSuccessfullyCompleted"));
        }
    }
}
