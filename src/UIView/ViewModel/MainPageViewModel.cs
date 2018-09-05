
namespace UIView.ViewModel
{
    using System;
    using UIModel.API;
    using UIUtilities.API;
    using Utilities.API;

    public class MainPageViewModel : ViewModelBase
    {
        public TitleZoneViewModel TitleZoneViewModel { get; set; }

        public PrimaryStatsTableViewModel PrimaryStatsTableViewModel { get; set; }

        public SkillTableViewModel SkillTableViewModel { get; set; }

        private readonly ILogger _logger;

        private readonly IMainPageModel _model;

        public MainPageViewModel(ILogger logger, IMainPageModel model, IUiThreadInvoker uiThreadInvoker) : base(uiThreadInvoker)
        {
            _logger = logger;
            _model = model;
        }

        public override void Init()
        {
            TitleZoneViewModel.Init();
            PrimaryStatsTableViewModel.Init();
            SkillTableViewModel.Init();
        }

        public override void Dispose()
        {
            _logger.LogEntry();
            SkillTableViewModel?.Dispose();
            PrimaryStatsTableViewModel?.Dispose();
        }
    }
}
