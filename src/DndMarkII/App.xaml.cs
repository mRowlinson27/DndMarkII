﻿
namespace DndMarkII
{
    using System;
    using Chromium.Event;
    using Neutronium.Core.JavascriptFramework;
    using Neutronium.WebBrowserEngine.ChromiumFx;
    using Neutronium.JavascriptFramework.Knockout;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ChromiumFxWebBrowserApp
    {
        [STAThread]
        static void Main()
        {
            BootStrapper.Logger.LogMessage("\n");
            BootStrapper.Logger.LogMessage("Program started");

            var application = BootStrapper.SetupApplication();
            var mainWindow = BootStrapper.CreateMainWindow();

            BootStrapper.Logger.LogMessage("Bootstrapping complete");
            application.Run(mainWindow);

            BootStrapper.Logger.LogMessage("Program closed");
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
