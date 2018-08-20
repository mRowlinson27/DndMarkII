
namespace DndMarkII
{
    using System;
    using System.Windows;
    using Utilities.API;
    using ViewModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILogger _logger;

        public MainWindow(ILogger logger, MainPageViewModel mainPageViewModel)
        {
            _logger = logger;
            InitializeComponent();
            DataContext = mainPageViewModel;
        }

        protected override void OnClosed(EventArgs e)
        {
            _logger.LogEntry();
            base.OnClosed(e);
            this.HtmlView.Dispose();
        }
    }
}
