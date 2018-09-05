
namespace UIView.UnitTests
{
    using System;
    using System.Collections.Generic;
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
    using ViewModel;

    [TestFixture]
    public class SkillTableViewModelTests
    {
        private SkillTableViewModel _skillTableViewModel;

        private ILogger _logger;
        private ISkillTableModel _skillTableModel;
        private IObservableHelper _observableHelper;
        private INotifyTaskCompletionFactory _fakeNotifyTaskCompletionFactory;
        private IAsyncCommandFactory _asyncCommandFactory;
        private IAsyncTaskRunnerFactory _asyncTaskRunnerFactory;
        private IUiThreadInvoker _uiThreadInvoker;
        private ISkillViewModelFactory _skillViewModelFactory;

        private INotifyTaskCompletion<IEnumerable<UiSkill>> _fakeNotifyTaskCompletion;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _skillTableModel = A.Fake<ISkillTableModel>();
            _fakeNotifyTaskCompletionFactory = A.Fake<INotifyTaskCompletionFactory>();
            _observableHelper = new ObservableHelper();
            _asyncCommandFactory = new AsyncCommandFactory(_fakeNotifyTaskCompletionFactory, new UiStateController(_logger, new UiLockerContextFactory()));
            _asyncTaskRunnerFactory = new AsyncTaskRunnerFactory(_fakeNotifyTaskCompletionFactory);
            _uiThreadInvoker = new UiThreadInvoker(_logger);
            _skillViewModelFactory = A.Fake<ISkillViewModelFactory>();

            _skillTableViewModel = new SkillTableViewModel(_logger, _skillTableModel, _observableHelper, _asyncCommandFactory, _asyncTaskRunnerFactory, _uiThreadInvoker, _skillViewModelFactory,
                new UiStateController(_logger, new UiLockerContextFactory()));
        }

        [TearDown]
        public void TearDown()
        {
            _skillTableViewModel.Dispose();
        }

        [Test]
        public void Init_RequestsSkillData()
        {
            //Arrange

            //Act
            _skillTableViewModel.Init();

            //Assert
            A.CallTo(() => _skillTableModel.RequestSkillsAsync()).MustHaveHappened();
        }

        [Test]
        public void Model_OnPropertyChanged_RequestsSkillData()
        {
            //Arrange

            //Act
            _skillTableModel.PropertyChanged += Raise.FreeForm<PropertyChangedEventHandler>.With(_skillTableModel, new PropertyChangedEventArgs("SkillViewModels"));

            //Assert
            A.CallTo(() => _skillTableModel.RequestSkillsAsync()).MustHaveHappened();
        }

        [Test]
        public async Task AddSkill_RequestsFromModel()
        {
            //Arrange
            var realNotifyTaskCompletion = new NotifyTaskCompletion<object>(_logger);
            A.CallTo(() => _fakeNotifyTaskCompletionFactory.Create<object>()).Returns(realNotifyTaskCompletion);

            //Act
            var command = (IAsyncCommand)_skillTableViewModel.AddSkill;
            await command.ExecuteAsync(null);
            await realNotifyTaskCompletion.Task;

            //Assert
            A.CallTo(() => _skillTableModel.AddSkillAsync()).MustHaveHappened();
        }

        [Test]
        public void Model_OnSkillRequestTaskCompletion_CreatesSkillViewModelsAndDataIsAvailable()
        {
            //Arrange
            _fakeNotifyTaskCompletion = A.Fake<INotifyTaskCompletion<IEnumerable<UiSkill>>>();
            A.CallTo(() => _fakeNotifyTaskCompletionFactory.Create<IEnumerable<UiSkill>>()).Returns(_fakeNotifyTaskCompletion);

            //Act
            _skillTableModel.PropertyChanged += Raise.FreeForm<PropertyChangedEventHandler>.With(_skillTableModel, new PropertyChangedEventArgs("SkillViewModels"));
            _fakeNotifyTaskCompletion.PropertyChanged += Raise.FreeForm<PropertyChangedEventHandler>.With(_fakeNotifyTaskCompletion, new PropertyChangedEventArgs("IsSuccessfullyCompleted"));

            //Assert
            _skillTableViewModel.DataAvailable.Should().BeTrue();
        }

        [Test]
        public void Model_OnSkillRequestTaskCompletion_SetsSkillViewModelBackgroundColour()
        {
            //Arrange
            _fakeNotifyTaskCompletion = A.Fake<INotifyTaskCompletion<IEnumerable<UiSkill>>>();
            A.CallTo(() => _fakeNotifyTaskCompletionFactory.Create<IEnumerable<UiSkill>>()).Returns(_fakeNotifyTaskCompletion);

            A.CallTo(() => _fakeNotifyTaskCompletion.Result).Returns(new List<UiSkill> { new UiSkill(), new UiSkill() });

            var fakeViewModel0 = A.Fake<ISkillViewModel>();
            var fakeViewModel1 = A.Fake<ISkillViewModel>();
            A.CallTo(() => _skillViewModelFactory.Create(A<UiSkill>.Ignored)).ReturnsNextFromSequence(fakeViewModel0, fakeViewModel1);

            //Act
            _skillTableModel.PropertyChanged += Raise.FreeForm<PropertyChangedEventHandler>.With(_skillTableModel, new PropertyChangedEventArgs("SkillViewModels"));
            _fakeNotifyTaskCompletion.PropertyChanged += Raise.FreeForm<PropertyChangedEventHandler>.With(_fakeNotifyTaskCompletion, new PropertyChangedEventArgs("IsSuccessfullyCompleted"));

            //Assert
            A.CallToSet(() => fakeViewModel0.BackGroundColour).To(Constants.SkillModelEvenIndexBackGroundColour).MustHaveHappened();
            A.CallToSet(() => fakeViewModel1.BackGroundColour).To(Constants.SkillModelOddIndexBackGroundColour).MustHaveHappened();
        }
    }
}
