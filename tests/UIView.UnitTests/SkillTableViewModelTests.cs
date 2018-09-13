
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
    public class SkillTableViewModelTests
    {
        private SkillTableViewModel _skillTableViewModel;

        private ILogger _logger;
        private ISkillTableModel _skillTableModel;
        private INotifyTaskCompletionFactory _fakeNotifyTaskCompletionFactory;
        private IAsyncCommandFactory _asyncCommandFactory;
        private IUiThreadInvoker _uiThreadInvoker;
        private IAsyncCommandAdaptorFactory _asyncCommandAdaptorFactory;
        private ISkillTableViewModelBindingHelper _bindingHelper;

        private INotifyTaskCompletion<IEnumerable<UiSkill>> _skillRequestNotifyTaskCompletion;
        private INotifyTaskCompletion<object> _addSkillCommandNotifyTaskCompletion;

        [Test]
        public async Task Init_RequestsSkillData()
        {
            //Arrange
            Setup();

            //Act
            _skillTableViewModel.Init();
            await _skillRequestNotifyTaskCompletion.Task;

            //Assert
            A.CallTo(() => _skillTableModel.RequestSkills()).MustHaveHappened();
        }

        [Test]
        public async Task Model_OnPropertyChanged_RequestsSkillData()
        {
            //Arrange
            Setup();

            //Act
            _skillTableModel.PropertyChanged += Raise.FreeForm<PropertyChangedEventHandler>.With(_skillTableModel, new PropertyChangedEventArgs("SkillViewModels"));
            await _skillRequestNotifyTaskCompletion.Task;

            //Assert
            A.CallTo(() => _skillTableModel.RequestSkills()).MustHaveHappened();
        }

        [Test]
        public async Task AddSkill_RequestsFromModel()
        {
            //Arrange
            Setup();

            //Act
            var command = _skillTableViewModel.AddSkill;
            await command.ExecuteAsync();
            await _addSkillCommandNotifyTaskCompletion.Task;

            //Assert
            A.CallTo(() => _skillTableModel.AddSkill()).MustHaveHappened();
        }

        [Test]
        public void Model_OnSkillRequestTaskCompletion_CreatesSkillViewModelsAndDataIsAvailable()
        {
            //Arrange
            Setup(skillRequestNotifyTaskCompletion: A.Fake<INotifyTaskCompletion<IEnumerable<UiSkill>>>());
            var newUiSkills = new List<UiSkill> {new UiSkill(), new UiSkill()};
            A.CallTo(() => _skillRequestNotifyTaskCompletion.Result).Returns(newUiSkills);

            //Act
            _skillTableModel.PropertyChanged += Raise.FreeForm<PropertyChangedEventHandler>.With(_skillTableModel, new PropertyChangedEventArgs("SkillViewModels"));
            _skillRequestNotifyTaskCompletion.PropertyChanged += Raise.FreeForm<PropertyChangedEventHandler>.With(_skillRequestNotifyTaskCompletion, new PropertyChangedEventArgs("IsSuccessfullyCompleted"));

            //Assert
            A.CallTo(() => _bindingHelper.Rebind(A<ObservableCollection<ISkillViewModel>>.Ignored, newUiSkills)).MustHaveHappened();
            _skillTableViewModel.DataAvailable.Should().BeTrue();
        }

        public void Setup(INotifyTaskCompletion<IEnumerable<UiSkill>> skillRequestNotifyTaskCompletion = null, INotifyTaskCompletion<object> addSkillCommandNotifyTaskCompletion = null)
        {
            SetupStaticFakes();

            SetupNotifyTaskCompletions(skillRequestNotifyTaskCompletion, addSkillCommandNotifyTaskCompletion);

            SetupSkillTableViewModel();
        }

        private void SetupStaticFakes()
        {
            _logger = A.Fake<ILogger>();
            _skillTableModel = A.Fake<ISkillTableModel>();
            _bindingHelper = A.Fake<ISkillTableViewModelBindingHelper>();
            _fakeNotifyTaskCompletionFactory = A.Fake<INotifyTaskCompletionFactory>();
        }

        private void SetupNotifyTaskCompletions(INotifyTaskCompletion<IEnumerable<UiSkill>> skillRequestNotifyTaskCompletion, INotifyTaskCompletion<object> addSkillCommandNotifyTaskCompletion)
        {
            if (skillRequestNotifyTaskCompletion == null)
            {
                skillRequestNotifyTaskCompletion = new NotifyTaskCompletion<IEnumerable<UiSkill>>(_logger);
            }
            _skillRequestNotifyTaskCompletion = skillRequestNotifyTaskCompletion;
            A.CallTo(() => _fakeNotifyTaskCompletionFactory.Create<IEnumerable<UiSkill>>()).Returns(_skillRequestNotifyTaskCompletion);

            if (addSkillCommandNotifyTaskCompletion == null)
            {
                addSkillCommandNotifyTaskCompletion = new NotifyTaskCompletion<object>(_logger);
            }
            _addSkillCommandNotifyTaskCompletion = addSkillCommandNotifyTaskCompletion;
            A.CallTo(() => _fakeNotifyTaskCompletionFactory.Create<object>()).Returns(_addSkillCommandNotifyTaskCompletion);
        }

        public void SetupSkillTableViewModel()
        {
            var uiStateController = new UiStateController(_logger, new UiLockerContextFactory());
            _asyncCommandFactory = new AsyncCommandFactory(_fakeNotifyTaskCompletionFactory, new AsyncCommandWatcherFactory(uiStateController), new TaskWrapper());
            _asyncCommandAdaptorFactory = new AsyncCommandAdaptorFactory(_asyncCommandFactory);
            _uiThreadInvoker = new UiThreadInvoker(_logger);

            _skillTableViewModel = new SkillTableViewModel(_logger, _skillTableModel, _asyncCommandFactory, _asyncCommandAdaptorFactory, _uiThreadInvoker,
                new UiStateController(_logger, new UiLockerContextFactory()), _bindingHelper);
        }

        [TearDown]
        public void Teardown()
        {
            _skillTableViewModel.Dispose();
        }
    }
}
