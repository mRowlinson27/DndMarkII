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

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _model = A.Fake<IPrimaryStatModel>();
            _uiThreadInvoker = A.Fake<IUiThreadInvoker>();
            _fakeNotifyTaskCompletionFactory = A.Fake<INotifyTaskCompletionFactory>();
            _asyncCommandFactory = new AsyncCommandFactory(_fakeNotifyTaskCompletionFactory);

            _primaryStatViewModel = new PrimaryStatViewModel(_logger, _model, _asyncCommandFactory, _uiThreadInvoker);
        }

        [Test]
        public async Task UpdatePrimaryStatCommandAsync_CallModelDataUpdated()
        {
            //Arrange
            var uiPrimaryStat = new UiPrimaryStat();
            _primaryStatViewModel.PrimaryStat = uiPrimaryStat;

            var realNotifyTaskCompletion = new NotifyTaskCompletion<object>(_logger);
            A.CallTo(() => _fakeNotifyTaskCompletionFactory.Create<object>()).Returns(realNotifyTaskCompletion);

            //Act
            var command = (IAsyncCommand) _primaryStatViewModel.UpdatePrimaryStat;
            await command.ExecuteAsync(null);
            await realNotifyTaskCompletion.Task;

            //Assert
            A.CallTo(() => _model.UpdateStatAsync(_primaryStatViewModel.PrimaryStat)).MustHaveHappened();
        }
    }
}
