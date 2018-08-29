﻿
namespace UIView.UnitTests
{
    using System;
    using System.ComponentModel;
    using FakeItEasy;
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
        private IAsyncCommandFactory _asyncCommandFactory;
        private IAsyncTaskRunnerFactory _asyncTaskRunnerFactory;
        private IUiThreadInvoker _uiThreadInvoker;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _skillTableModel = A.Fake<ISkillTableModel>();
            _observableHelper = new ObservableHelper();
            var notifyTaskCompletionFactory = A.Fake<INotifyTaskCompletionFactory>();
            _asyncCommandFactory = new AsyncCommandFactory(notifyTaskCompletionFactory);
            _asyncTaskRunnerFactory = new AsyncTaskRunnerFactory(notifyTaskCompletionFactory);
            _uiThreadInvoker = new UiThreadInvoker(_logger);

            _skillTableViewModel = new SkillTableViewModel(_logger, _skillTableModel, _observableHelper, _asyncCommandFactory, _asyncTaskRunnerFactory, _uiThreadInvoker, null);
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
        public void AddSkill_RequestsFromModel()
        {
            //Arrange

            //Act
            _skillTableViewModel.AddSkill.Execute(null);

            //Assert
            A.CallTo(() => _skillTableModel.AddSkillAsync()).MustHaveHappened();
        }
    }
}
