
namespace UIView.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using API;
    using UIModel.API;
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
            set
            {
                PrimaryStat.AbilityScore = value;
                OnPropertyChanged("AbilityScore");
            }
        }

        public string AbilityModifier => PrimaryStat.AbilityModifier;

        public bool InEdit { get; set; } = true;

        public IAsyncCommandAdaptor UpdatePrimaryStat { get; private set; }

        [Bindable(false)]
        public UiPrimaryStat PrimaryStat { get; set; }

        private readonly ILogger _logger;

        private readonly IPrimaryStatModel _model;

        private readonly IUiThreadInvoker _uiThreadInvoker;

        public PrimaryStatViewModel(ILogger logger, IPrimaryStatModel model, IAsyncCommandAdaptorFactory asyncCommandAdaptorFactory, IUiThreadInvoker uiThreadInvoker): base(uiThreadInvoker)
        {
            _logger = logger;
            _model = model;
            _uiThreadInvoker = uiThreadInvoker;

            SetupCommandBindings(asyncCommandAdaptorFactory);
        }

        private void SetupCommandBindings(IAsyncCommandAdaptorFactory asyncCommandAdaptorFactory)
        {
            UpdatePrimaryStat = asyncCommandAdaptorFactory.CreateWithContext((Action) UpdatePrimaryStatCommand);
        }

        private void UpdatePrimaryStatCommand()
        {
            _logger.LogMessage($"AbilityScore: {AbilityScore}");
            _model.UpdateStat(PrimaryStat);
        }

        public override void Dispose()
        {
            UpdatePrimaryStat.Dispose();
        }
    }
}
