﻿
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
    using UIView.Factories;
    using UIView.Helpers;
    using UIView.ViewModel;
    using Utilities.API;
    using Utilities.Implementation;

    public class BootStrapper : IDisposable
    {
        private readonly ILogger _logger;

        //Utilities
        private IObservableHelper _observableHelper;
        private IUiThreadInvoker _uiThreadInvoker;

        //UI Utilities
        private IAutoMapper _autoMapper;
        private INotifyTaskCompletionFactory _notifyTaskCompletionFactory;
        private IUiStateController _uiStateController;
        private IAsyncCommandFactory _asyncCommandFactory;
        private IAsyncCommandAdaptorFactory _asyncCommandAdaptorFactory;

        //Database
        private ModelJsonRepo _masterRepo;

        //Services
        private ISkillsService _skillsService;
        private IPrimaryStatsService _primaryStatsService;

        //UI Model
        private ITitleZoneModel _titleZoneModel;
        private ISkillTableModel _skillTableModel;
        private IPrimaryStatsTableModel _primaryStatsTableModel;
        private IPrimaryStatModelFactory _primaryStatModelFactory;

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

            var mainPageViewModel = new MainPageViewModel(_logger, new MainPageModel(), _uiThreadInvoker)
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
            _uiStateController = new UiStateController(_logger, new UiLockerContextFactory());
            var asyncCommandWatcherFactory = new AsyncCommandWatcherFactory(_uiStateController);

            _notifyTaskCompletionFactory = new NotifyTaskCompletionFactory(_logger);
            _asyncCommandFactory = new AsyncCommandFactory(_notifyTaskCompletionFactory, asyncCommandWatcherFactory, new TaskWrapper());
            _asyncCommandAdaptorFactory = new AsyncCommandAdaptorFactory(_asyncCommandFactory);
        }

        private void SetupDatabase()
        {
            _masterRepo = new ModelJsonRepo(new DummyJsonFile());
        }

        private void SetupServices()
        {
            var svcAutoMapper = new SvcAutoMapper();
            _primaryStatsService = new PrimaryStatsService(_logger, _masterRepo, svcAutoMapper);
            _skillsService = new SkillsService(_logger, _masterRepo, svcAutoMapper, new SkillTotalCalculator(_primaryStatsService), _primaryStatsService);
        }

        private void SetupUiModel()
        {
            _autoMapper = new AutoMapper();
            _titleZoneModel = new TitleZoneModel();
            _skillTableModel = new SkillTableModel(_logger, _skillsService, _autoMapper);
            _primaryStatsTableModel = new PrimaryStatsTableModel(_logger, _primaryStatsService, _autoMapper);
            _primaryStatModelFactory = new PrimaryStatModelFactory(_primaryStatsService, _autoMapper);
        }

        private void SetupUiView()
        {
            var skillViewModelFactory = new SkillViewModelFactory(_logger, new SkillModelFactoryFactory(_skillsService, _autoMapper),  _asyncCommandAdaptorFactory, _uiThreadInvoker);
            var primaryStatViewModelFactory = new PrimaryStatViewModelFactory(_logger, _asyncCommandAdaptorFactory, _uiThreadInvoker, _primaryStatModelFactory);

            _titleZoneViewModel = new TitleZoneViewModel(_titleZoneModel, _uiThreadInvoker);

            _skillTableViewModel = new SkillTableViewModel(_logger, _skillTableModel, _asyncCommandFactory, _asyncCommandAdaptorFactory,
                _uiThreadInvoker, _uiStateController, new SkillTableViewModelBindingHelper(skillViewModelFactory));

            _primaryStatsTableViewModel = new PrimaryStatsTableViewModel(_logger, _primaryStatsTableModel, new PrimaryStatTableViewModelBindingHelper(primaryStatViewModelFactory),
                _asyncCommandFactory, _asyncCommandAdaptorFactory, _uiThreadInvoker, _uiStateController);
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
