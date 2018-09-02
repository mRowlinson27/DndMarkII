
namespace UIView.ViewModel
{
    using API;
    using UIModel.API.Dto;
    using UIUtilities.API;
    using UIUtilities.API.AsyncCommands;
    using Utilities.API;

    public class PrimaryStatViewModel : ViewModelBase, IPrimaryStatViewModel
    {
        public string ShortName => PrimaryStat.ShortName;

        public string Name => PrimaryStat.Name;

        public string AbilityScore
        {
            get => PrimaryStat.AbilityScore;
            set => PrimaryStat.AbilityScore = value;
        }

        public string AbilityModifier => PrimaryStat.AbilityModifier;

        public bool InEdit { get; set; } = false;

        public UiPrimaryStat PrimaryStat { get; set; }

        private readonly ILogger _logger;

        private readonly IUiThreadInvoker _uiThreadInvoker;

        public PrimaryStatViewModel(ILogger logger, IAsyncCommandFactory asyncCommandFactory, IUiThreadInvoker uiThreadInvoker)
        {
            _logger = logger;
            _uiThreadInvoker = uiThreadInvoker;
        }
    }
}
