
namespace UIView
{
    using System;
    using System.Windows;
    using UIUtilities.API;
    using Utilities.API;
    using ViewModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILogger _logger;

        private readonly MainPageViewModel _mainPageViewModel;

        private readonly IUiThreadInvoker _uiThreadInvoker;

        public MainWindow(ILogger logger, MainPageViewModel mainPageViewModel, IUiThreadInvoker uiThreadInvoker)
        {
            _logger = logger;
            _mainPageViewModel = mainPageViewModel;
            _uiThreadInvoker = uiThreadInvoker;
            InitializeComponent();
            DataContext = mainPageViewModel;
        }

        public void Init()
        {
            _mainPageViewModel.Init();
            _uiThreadInvoker.Init();
        }

        protected override void OnClosed(EventArgs e)
        {
            _logger.LogEntry();
            base.OnClosed(e);
            this.HtmlView.Dispose();
        }
    }
}
