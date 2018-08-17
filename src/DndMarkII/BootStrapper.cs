
namespace DndMarkII
{
    using System.Windows;
    using Chromium;
    using Chromium.Event;
    using Model;
    using Neutronium.JavascriptFramework.Knockout;
    using Neutronium.WebBrowserEngine.ChromiumFx;
    using Neutronium.WPF;
    using Utilities.API;
    using Utilities.Implementation;
    using Utilities.Implementation.DAL;
    using ViewModel;

    public static class BootStrapper
    {
        public static ILogger Logger => _logger ?? (_logger = new Logger(new FileWriter(new StreamWriterWrapperFactory()), new DateTimeWrapper()));

        private static ILogger _logger;

        public static App SetupApplication()
        {
            InitializeChromium();

            var application = new App();
            application.InitializeComponent();
            return application;
        }

        public static MainWindow CreateMainWindow()
        {
            var mainPageViewModel = new MainPageViewModel
            {
                TitleZoneViewModel = new TitleZoneViewModel(),
                SkillTableViewModel = new SkillTableViewModel(new SkillTableModel())
            };

            return new MainWindow(mainPageViewModel);
        }

        private static void InitializeChromium()
        {
            var engine = HTMLEngineFactory.Engine;

            var windowFactory = new ChromiumFXWPFWebWindowFactory(UpdateChromiumSettings, PrivateUpdateLineCommandArg);
            engine.RegisterHTMLEngine(windowFactory);

            engine.RegisterJavaScriptFramework(new KnockoutFrameworkManager());
        }

        private static void UpdateChromiumSettings(CfxSettings settings)
        {

        }

        private static void PrivateUpdateLineCommandArg(CfxOnBeforeCommandLineProcessingEventArgs beforeLineCommand)
        {
            beforeLineCommand.CommandLine.AppendSwitch("disable-gpu");
            beforeLineCommand.CommandLine.AppendSwitch("disable-web-security");
        }
    }
}
