
namespace UIView.ViewModel
{
    using System.ComponentModel;
    using System.Threading.Tasks;
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
        
        [Bindable(false)]
        public UiPrimaryStat PrimaryStat { get; set; }

        private readonly ILogger _logger;

        private readonly IPrimaryStatModel _model;

        private readonly IUiThreadInvoker _uiThreadInvoker;

        public PrimaryStatViewModel(ILogger logger, IPrimaryStatModel model, IAsyncCommandFactory asyncCommandFactory, IUiThreadInvoker uiThreadInvoker)
        {
            _logger = logger;
            _model = model;
            _uiThreadInvoker = uiThreadInvoker;
            PropertyChanged += async (s, e) => await _model.UpdateStatAsync(PrimaryStat);
        }

        private async Task OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

    }
}
