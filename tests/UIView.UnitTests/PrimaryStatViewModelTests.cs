﻿
namespace UIView.UnitTests
{
    using System;
    using System.Threading.Tasks;
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
    public class PrimaryStatViewModelTests
    {
        private PrimaryStatViewModel _primaryStatViewModel;

        private ILogger _logger;

        private IPrimaryStatModel _model;

        private IUiThreadInvoker _uiThreadInvoker;
        private INotifyTaskCompletionFactory _fakeNotifyTaskCompletionFactory;
        private IAsyncCommandFactory _asyncCommandFactory;

        private INotifyTaskCompletion<object> _realNotifyTaskCompletion;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _model = A.Fake<IPrimaryStatModel>();
            _uiThreadInvoker = A.Fake<IUiThreadInvoker>();
            _fakeNotifyTaskCompletionFactory = A.Fake<INotifyTaskCompletionFactory>();

            _realNotifyTaskCompletion = new NotifyTaskCompletion<object>(_logger);
            A.CallTo(() => _fakeNotifyTaskCompletionFactory.Create<object>()).Returns(_realNotifyTaskCompletion);

            var uiStateController = new UiStateController(_logger, new UiLockerContextFactory());
            _asyncCommandFactory = new AsyncCommandFactory(_fakeNotifyTaskCompletionFactory, new AsyncCommandWatcherFactory(uiStateController), new TaskWrapper());
            var asyncCommandAdaptorFactory = new AsyncCommandAdaptorFactory(_asyncCommandFactory);

            _primaryStatViewModel = new PrimaryStatViewModel(_logger, _model, asyncCommandAdaptorFactory, _uiThreadInvoker);
        }

        [Test]
        public async Task UpdatePrimaryStatCommand_CallModelDataUpdated()
        {
            //Arrange
            var uiPrimaryStat = new UiPrimaryStat();
            _primaryStatViewModel.PrimaryStat = uiPrimaryStat;

            //Act
            var command = _primaryStatViewModel.UpdatePrimaryStat;
            await command.ExecuteAsync();
            await _realNotifyTaskCompletion.Task;

            //Assert
            A.CallTo(() => _model.Update(_primaryStatViewModel.PrimaryStat)).MustHaveHappened();
        }
    }
}
