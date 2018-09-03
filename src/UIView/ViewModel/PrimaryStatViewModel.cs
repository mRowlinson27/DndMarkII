
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

        public ICommand UpdatePrimaryStat { get; private set; }
        public bool UpdatePrimaryStatCanExecute => UpdatePrimaryStat.CanExecute(null);

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

            SetupCommandBindings(asyncCommandFactory);
        }

        private void SetupCommandBindings(IAsyncCommandFactory asyncCommandFactory)
        {
            UpdatePrimaryStat = asyncCommandFactory.Create<PrimaryStatViewModel>(UpdatePrimaryStatCommandAsync);
            UpdatePrimaryStat.CanExecuteChanged += UpdatePrimaryStatOnCanExecuteChanged;
        }

        private async Task UpdatePrimaryStatCommandAsync(PrimaryStatViewModel primaryStatViewModel)
        {
            _logger.LogMessage($"AbilityScore: {primaryStatViewModel.AbilityScore}");
            await Task.Delay(1);
            //            await _model.UpdateStatAsync(PrimaryStat).ConfigureAwait(false);
        }

        private void UpdatePrimaryStatOnCanExecuteChanged(object sender, EventArgs e)
        {
            _uiThreadInvoker.Dispatch(() => OnPropertyChanged("UpdatePrimaryStatCanExecute"));
        }
    }
}
