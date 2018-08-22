
namespace UIView
{
    using System;
    using Chromium;
    using Chromium.Event;
    using Neutronium.JavascriptFramework.Knockout;
    using Neutronium.WebBrowserEngine.ChromiumFx;
    using Neutronium.WPF;
    using UIModel;
    using UIUtilities;
    using Utilities.API;
    using ViewModel;

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
            var mainPageViewModel = new MainPageViewModel(new MainPageModel())
            {
                TitleZoneViewModel = new TitleZoneViewModel(new TitleZoneModel()),
                SkillTableViewModel = new SkillTableViewModel(_logger, new SkillTableModel(_logger), new ObservableBinder()),
                PrimaryStatsTableViewModel = new PrimaryStatsTableViewModel(new PrimaryStatsTableModel())
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
