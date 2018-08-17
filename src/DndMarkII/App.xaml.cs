﻿
namespace DndMarkII
{
    using Chromium.Event;
    using Neutronium.Core.JavascriptFramework;
    using Neutronium.WebBrowserEngine.ChromiumFx;
    using Neutronium.JavascriptFramework.Knockout;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ChromiumFxWebBrowserApp
    {
//        [STAThread]
//        public static void Main()
//        {
//            var logger = new Logger(new FileWriter(new StreamWriterWrapperFactory()), new DateTimeWrapper());
//
//            logger.LogMessage("Program started");
//        }

        protected override IJavascriptFrameworkManager GetJavascriptUIFrameworkManager()
        {
            return new KnockoutFrameworkManager();
        }

        protected override void UpdateLineCommandArg(CfxOnBeforeCommandLineProcessingEventArgs beforeLineCommand)
        {
            beforeLineCommand.CommandLine.AppendSwitch("disable-web-security");
        }
    }
}
