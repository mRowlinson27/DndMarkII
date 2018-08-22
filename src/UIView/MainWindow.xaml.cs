
namespace UIView
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

        private readonly MainPageViewModel _mainPageViewModel;

        public MainWindow(ILogger logger, MainPageViewModel mainPageViewModel)
        {
            _logger = logger;
            _mainPageViewModel = mainPageViewModel;
            InitializeComponent();
            DataContext = mainPageViewModel;
        }

        public void Init()
        {
            _mainPageViewModel.Init();
        }

        protected override void OnClosed(EventArgs e)
        {
            _logger.LogEntry();
            base.OnClosed(e);
            this.HtmlView.Dispose();
        }
    }
}
