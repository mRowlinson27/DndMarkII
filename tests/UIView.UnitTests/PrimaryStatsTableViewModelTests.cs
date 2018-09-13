
namespace UIView.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
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
        private IPrimaryStatTableViewModelBindingHelper _bindingHelper;
        private IAsyncCommandFactory _asyncCommandFactory;
        private IAsyncCommandAdaptorFactory _asyncCommandAdaptorFactory;

        private INotifyTaskCompletion<IEnumerable<UiPrimaryStat>> _dataRequestNotifyTaskCompletion;

        [TearDown]
        public void TearDown()
        {
            _primaryStatsTableViewModel.Dispose();
        }

        [Test]
        public async Task Init_RequestPrimaryStatsAsync()
        {
            //Arrange
            Setup();

            //Act
            _primaryStatsTableViewModel.Init();
            await _dataRequestNotifyTaskCompletion.Task;

            //Assert
            A.CallTo(() => _fakePrimaryStatsTableModel.RequestPrimaryStats()).MustHaveHappened();
        }

        [Test]
        public void Model_OnPropertyChanged_RequestPrimaryStatsAsync()
        {
            //Arrange
            Setup();

            //Act
            _fakePrimaryStatsTableModel.PrimaryStatsUpdated += Raise.With(_fakePrimaryStatsTableModel, new EventArgs());

            //Assert
            A.CallTo(() => _fakePrimaryStatsTableModel.RequestPrimaryStats()).MustHaveHappened();
        }

        [Test]
        public void Model_OnPrimaryStatsRequestTaskCompletion_CreatesPrimaryStatsViewModelsAndDataIsAvailable()
        {
            //Arrange
            Setup(A.Fake<INotifyTaskCompletion<IEnumerable<UiPrimaryStat>>>());
            var dataList = new List<UiPrimaryStat> {new UiPrimaryStat(), new UiPrimaryStat()};
            A.CallTo(() => _dataRequestNotifyTaskCompletion.Result).Returns(dataList);

            //Act
            RaiseModelDataRetrievedSuccessfullyEvents();

            //Assert
            _primaryStatsTableViewModel.DataAvailable.Should().BeTrue();

            A.CallTo(() => _bindingHelper.Rebind(A<ObservableCollection<IPrimaryStatViewModel>>.Ignored, dataList)).MustHaveHappened();
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
            _bindingHelper = A.Fake<IPrimaryStatTableViewModelBindingHelper>();
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
            var uiStateController = new UiStateController(_logger, new UiLockerContextFactory());
            _asyncCommandFactory = new AsyncCommandFactory(_fakeNotifyTaskCompletionFactory, new AsyncCommandWatcherFactory(uiStateController), new TaskWrapper());
            _asyncCommandAdaptorFactory = new AsyncCommandAdaptorFactory(_asyncCommandFactory);

            _primaryStatsTableViewModel = new PrimaryStatsTableViewModel(_logger, _fakePrimaryStatsTableModel, _bindingHelper, _asyncCommandFactory,
                _asyncCommandAdaptorFactory, new UiThreadInvoker(_logger), new UiStateController(_logger, new UiLockerContextFactory()));
        }

        private void RaiseModelDataRetrievedSuccessfullyEvents()
        {
            _fakePrimaryStatsTableModel.PrimaryStatsUpdated += Raise.FreeForm<EventHandler>.With(_fakePrimaryStatsTableModel, EventArgs.Empty);
            _dataRequestNotifyTaskCompletion.PropertyChanged += Raise.FreeForm<PropertyChangedEventHandler>.With(_dataRequestNotifyTaskCompletion, new PropertyChangedEventArgs("IsSuccessfullyCompleted"));
        }
    }
}
