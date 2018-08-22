
namespace BootStrapper
{
    using System;
    using Chromium.Event;
    using Neutronium.Core.JavascriptFramework;
    using Neutronium.JavascriptFramework.Knockout;
    using Neutronium.WebBrowserEngine.ChromiumFx;
    using Neutronium.WPF;
    using UIView;
    using Utilities.Implementation;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ChromiumFxWebBrowserApp
    {
        public new MainWindow MainWindow { get; set; }

        [STAThread]
        static void Main()
        {
            var logger = LoggerFactory.GetInstance;
            logger.LogMessage("Program started");

            using (var bootstrapper = new BootStrapper(logger))
            {
                var application = bootstrapper.SetupApplication();
                var mainWindow = bootstrapper.CreateMainWindow();

                application.MainWindow = mainWindow;

                logger.LogMessage("Bootstrapping complete");

                application.Run(mainWindow);
            }

            logger.LogMessage("Program closed\n");
        }

        protected override void OnStartUp(IHTMLEngineFactory factory)
        {
            base.OnStartUp(factory);
            MainWindow.Init();
        }

        //These do nothing
        protected override IJavascriptFrameworkManager GetJavascriptUIFrameworkManager()
        {
            return new KnockoutFrameworkManager();
        }

        //These do nothing
        protected override void UpdateLineCommandArg(CfxOnBeforeCommandLineProcessingEventArgs beforeLineCommand)
        {
        }
    }
}
