
namespace BootStrapper
{
    using System;
    using Chromium;
    using Chromium.Event;
    using Database;
    using Neutronium.JavascriptFramework.Knockout;
    using Neutronium.WebBrowserEngine.ChromiumFx;
    using Neutronium.WPF;
    using Services;
    using Services.API;
    using UIModel;
    using UIModel.API;
    using UIUtilities;
    using UIUtilities.API;
    using UIUtilities.API.AsyncCommands;
    using UIUtilities.AsyncCommands;
    using UIView;
    using UIView.ViewModel;
    using Utilities.API;

    public class BootStrapper : IDisposable
    {
        private readonly ILogger _logger;

        //Utilities
        private IObservableHelper _observableHelper;
        private IUiThreadInvoker _uiThreadInvoker;

        //UI Utilities
        private INotifyTaskCompletionFactory _notifyTaskCompletionFactory;
        private IAsyncCommandFactory _asyncCommandFactory;
        private IAsyncTaskRunnerFactory _asyncTaskRunnerFactory;

        //Database
        private ModelJsonRepo _masterRepo;

        //Services
        private ISkillsService _skillsService;
        private IPrimaryStatsService _primaryStatsService;

        //UI Model
        private IAutoMapper _uiModelAutoMapper;
        private ITitleZoneModel _titleZoneModel;
        private ISkillTableModel _skillTableModel;
        private IPrimaryStatsTableModel _primaryStatsTableModel;

        //UI ViewModel
        private TitleZoneViewModel _titleZoneViewModel;
        private SkillTableViewModel _skillTableViewModel;
        private PrimaryStatsTableViewModel _primaryStatsTableViewModel;


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
            SetupUtilities();

            SetupUiUtilities();

            SetupDatabase();

            SetupServices();

            SetupUiModel();

            SetupUiView();

            var mainPageViewModel = new MainPageViewModel(new MainPageModel())
            {
                TitleZoneViewModel = _titleZoneViewModel,
                SkillTableViewModel = _skillTableViewModel,
                PrimaryStatsTableViewModel = _primaryStatsTableViewModel
            };

            return new MainWindow(_logger, mainPageViewModel, _uiThreadInvoker);
        }

        private void SetupUtilities()
        {

        }

        private void SetupUiUtilities()
        {
            _observableHelper = new ObservableHelper();
            _uiThreadInvoker = new UiThreadInvoker(_logger);

            _notifyTaskCompletionFactory = new NotifyTaskCompletionFactory(_logger);
            _asyncCommandFactory = new AsyncCommandFactory(_notifyTaskCompletionFactory);
            _asyncTaskRunnerFactory = new AsyncTaskRunnerFactory(_notifyTaskCompletionFactory);
        }

        private void SetupDatabase()
        {
            _masterRepo = new ModelJsonRepo(new DummyJsonFile());
        }

        private void SetupServices()
        {
            _skillsService = new SkillsService(_logger, _masterRepo);
            _primaryStatsService = new PrimaryStatsService(_logger, _masterRepo);
        }

        private void SetupUiModel()
        {
            _uiModelAutoMapper = new AutoMapper();
            _titleZoneModel = new TitleZoneModel();
            _skillTableModel = new SkillTableModel(_logger, _skillsService, _uiModelAutoMapper);
            _primaryStatsTableModel = new PrimaryStatsTableModel(_logger, _primaryStatsService, _uiModelAutoMapper);

        }

        private void SetupUiView()
        {
            _titleZoneViewModel = new TitleZoneViewModel(_titleZoneModel);

            _skillTableViewModel = new SkillTableViewModel(_logger, _skillTableModel, _observableHelper, _asyncCommandFactory,
                _asyncTaskRunnerFactory, _uiThreadInvoker);

            _primaryStatsTableViewModel = new PrimaryStatsTableViewModel(_logger, _primaryStatsTableModel, _observableHelper,
                _asyncCommandFactory, _asyncTaskRunnerFactory, _uiThreadInvoker);
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
