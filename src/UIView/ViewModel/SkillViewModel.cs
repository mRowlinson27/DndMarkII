
namespace UIView.ViewModel
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using API;
    using Neutronium.MVVMComponents.Relay;
    using UIModel.API.Dto;
    using UIUtilities.API;
    using UIUtilities.API.AsyncCommands;
    using Utilities.API;

    public class SkillViewModel : ViewModelBase, ISkillViewModel
    {
        public Guid Id => Skill.Id;
        public int Total => Skill.Total;
        public string Name => Skill.Name;
        public int Ranks => Skill.Ranks;
        public string PrimaryStatName => Skill.PrimaryStatName;
        public bool HasArmourCheckPenalty => Skill.HasArmourCheckPenalty;
        public int ArmourCheckPenalty => Skill.ArmourCheckPenalty;
        public bool UseUntrained => Skill.UseUntrained;
        public bool Trained => Skill.Trained;

        public UiSkill Skill { get; set; }

        public bool ShowDetails
        {
            get => _showDetails;
            set => Set(ref _showDetails, value, "ShowDetails");
        }
        private bool _showDetails;

        public string BackGroundColour
        {
            get => _backGroundColour;
            set => Set(ref _backGroundColour, value, "BackGroundColour");
        }
        private string _backGroundColour;

        public ICommand Delete { get; private set; }
        public bool DeleteSkillCanExecute => Delete.CanExecute(null);

        public ICommand ShowDetail { get; private set; }

        private readonly ILogger _logger;

        private readonly IObservableHelper _observableHelper;

        private readonly IUiThreadInvoker _uiThreadInvoker;

        public SkillViewModel(ILogger logger, IObservableHelper observableHelper, IAsyncCommandFactory asyncCommandFactory,
            IAsyncTaskRunnerFactory asyncTaskRunnerFactory, IUiThreadInvoker uiThreadInvoker)
        {
            _logger = logger;
            _observableHelper = observableHelper;
            _uiThreadInvoker = uiThreadInvoker;

            SetupCommandBindings(asyncCommandFactory);
        }

        private void SetupCommandBindings(IAsyncCommandFactory asyncCommandFactory)
        {
            Delete = asyncCommandFactory.Create<UiSkill>(DeleteCommandAsync);
            Delete.CanExecuteChanged += DeleteOnCanExecuteChanged;

            ShowDetail = new RelaySimpleCommand(ShowDetailsCommand);
        }

        public override void Init()
        {
        }

        private async Task DeleteCommandAsync(UiSkill uiSkill)
        {
            
        }

        private void ShowDetailsCommand()
        {
            ShowDetails = !ShowDetails;
            _logger.LogExit();
        }

        private void AddSkillOnCanExecuteChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("AddSkillCanExecute");
        }

        private void DeleteOnCanExecuteChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("RemoveSkillCanExecute");
        }

        public void Dispose()
        {
            Delete.CanExecuteChanged -= DeleteOnCanExecuteChanged;
        }
    }
}
