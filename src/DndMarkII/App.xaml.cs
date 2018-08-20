﻿
namespace DndMarkII
{
    using System;
    using Chromium.Event;
    using Neutronium.Core.JavascriptFramework;
    using Neutronium.WebBrowserEngine.ChromiumFx;
    using Neutronium.JavascriptFramework.Knockout;
    using Utilities.Implementation;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ChromiumFxWebBrowserApp
    {
        [STAThread]
        static void Main()
        {
            var logger = LoggerFactory.GetInstance;
            logger.LogMessage("Program started");

            using (var bootstrapper = new BootStrapper(logger))
            {
                var application = bootstrapper.SetupApplication();
                var mainWindow = bootstrapper.CreateMainWindow();

                logger.LogMessage("Bootstrapping complete");

                application.Run(mainWindow);
            }

            logger.LogMessage("Program closed\n");
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
