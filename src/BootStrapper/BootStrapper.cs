﻿
namespace BootStrapper
{
    using System;
    using Chromium;
    using Chromium.Event;
    using Neutronium.JavascriptFramework.Knockout;
    using Neutronium.WebBrowserEngine.ChromiumFx;
    using Neutronium.WPF;
    using UIModel;
    using UIUtilities;
    using UIUtilities.AsyncCommands;
    using UIView;
    using UIView.ViewModel;
    using Utilities.API;

    public class BootStrapper : IDisposable
    {
        private readonly ILogger _logger;

        public BootStrapper(ILogger logger)
        {
            _logger = logger;
        }

        public App SetupApplication()
        {
            InitializeChromium();

            var application = new App();
            application.InitializeComponent();
            return application;
        }

        public MainWindow CreateMainWindow()
        {
            var observableBinder = new ObservableHelper();

            var notifyTaskCompletionFactory = new NotifyTaskCompletionFactory(_logger);
            var asyncCommandFactory = new AsyncCommandFactory(notifyTaskCompletionFactory);
            var asyncTaskRunnerFactory = new AsyncTaskRunnerFactory(notifyTaskCompletionFactory);

            var titleZoneViewModel = new TitleZoneViewModel(new TitleZoneModel());

            var skillTableViewModel = new SkillTableViewModel(_logger, new SkillTableModel(_logger), observableBinder, asyncCommandFactory, asyncTaskRunnerFactory);

            var primaryStatsTableViewModel = new PrimaryStatsTableViewModel(new PrimaryStatsTableModel(_logger));

            var mainPageViewModel = new MainPageViewModel(new MainPageModel())
            {
                TitleZoneViewModel = titleZoneViewModel,
                SkillTableViewModel = skillTableViewModel,
                PrimaryStatsTableViewModel = primaryStatsTableViewModel
            };

            return new MainWindow(_logger, mainPageViewModel);
        }

        public void Dispose()
        {
            HTMLEngineFactory.Engine.Dispose();
            _logger.LogEntry();
        }

        private void InitializeChromium()
        {
            var engine = HTMLEngineFactory.Engine;

            var windowFactory = new ChromiumFXWPFWebWindowFactory(UpdateChromiumSettings, UpdateLineCommandArg);
            engine.RegisterHTMLEngine(windowFactory);

            engine.RegisterJavaScriptFramework(new KnockoutFrameworkManager());
        }

        private void UpdateChromiumSettings(CfxSettings settings)
        {

        }

        private void UpdateLineCommandArg(CfxOnBeforeCommandLineProcessingEventArgs beforeLineCommand)
        {
            beforeLineCommand.CommandLine.AppendSwitch("disable-gpu");
            beforeLineCommand.CommandLine.AppendSwitch("disable-web-security");
        }
    }
}
