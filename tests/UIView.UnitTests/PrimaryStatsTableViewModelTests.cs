
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

        private INotifyTaskCompletion<IEnumerable<UiPrimaryStat>> _fakeNotifyTaskCompletion;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _fakePrimaryStatsTableModel = A.Fake<IPrimaryStatsTableModel>();
            _fakeNotifyTaskCompletionFactory = A.Fake<INotifyTaskCompletionFactory>();
            _fakePrimaryStatViewModelFactory = A.Fake<IPrimaryStatViewModelFactory>();

            _observableHelper = new ObservableHelper();
            _asyncCommandFactory = new AsyncCommandFactory(_fakeNotifyTaskCompletionFactory);
            _asyncTaskRunnerFactory = new AsyncTaskRunnerFactory(_fakeNotifyTaskCompletionFactory);

            _primaryStatsTableViewModel = new PrimaryStatsTableViewModel(_logger, _fakePrimaryStatsTableModel, _observableHelper, _asyncCommandFactory,
                _asyncTaskRunnerFactory, new UiThreadInvoker(_logger), _fakePrimaryStatViewModelFactory);
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
            A.CallTo(() => _fakePrimaryStatsTableModel.RequestPrimaryStatsAsync()).MustHaveHappened();
        }

        [Test]
        public void Model_OnPropertyChanged_RequestPrimaryStatsAsync()
        {
            //Arrange

            //Act
            _fakePrimaryStatsTableModel.PrimaryStatsUpdated += Raise.With(_fakePrimaryStatsTableModel, new EventArgs());

            //Assert
            A.CallTo(() => _fakePrimaryStatsTableModel.RequestPrimaryStatsAsync()).MustHaveHappened();
        }

        [Test]
        public void Model_OnPrimaryStatsRequestTaskCompletion_CreatesPrimaryStatsViewModelsAndDataIsAvailable()
        {
            //Arrange
            _fakeNotifyTaskCompletion = A.Fake<INotifyTaskCompletion<IEnumerable<UiPrimaryStat>>>();
            A.CallTo(() => _fakeNotifyTaskCompletionFactory.Create<IEnumerable<UiPrimaryStat>>()).Returns(_fakeNotifyTaskCompletion);

            A.CallTo(() => _fakeNotifyTaskCompletion.Result).Returns(new List<UiPrimaryStat> { new UiPrimaryStat(), new UiPrimaryStat() });

            var fakeViewModel0 = A.Fake<IPrimaryStatViewModel>();
            var fakeViewModel1 = A.Fake<IPrimaryStatViewModel>();
            A.CallTo(() => _fakePrimaryStatViewModelFactory.Create(A<UiPrimaryStat>.Ignored)).ReturnsNextFromSequence(fakeViewModel0, fakeViewModel1);

            //Act
            _fakePrimaryStatsTableModel.PrimaryStatsUpdated += Raise.FreeForm<EventHandler>.With(_fakePrimaryStatsTableModel, EventArgs.Empty);
            _fakeNotifyTaskCompletion.PropertyChanged += Raise.FreeForm<PropertyChangedEventHandler>.With(_fakeNotifyTaskCompletion, new PropertyChangedEventArgs("IsSuccessfullyCompleted"));

            //Assert
            _primaryStatsTableViewModel.DataAvailable.Should().BeTrue();
            _primaryStatsTableViewModel.PrimaryStats[0].Should().Be(fakeViewModel0);
            _primaryStatsTableViewModel.PrimaryStats[1].Should().Be(fakeViewModel1);
        }
    }
}
