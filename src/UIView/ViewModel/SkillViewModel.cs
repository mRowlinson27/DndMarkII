
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

        public bool ShowingDetails
        {
            get => _showingDetails;
            set => Set(ref _showingDetails, value, "ShowingDetails");
        }
        private bool _showingDetails;

        public string BackGroundColour
        {
            get => _backGroundColour;
            set => Set(ref _backGroundColour, value, "BackGroundColour");
        }
        private string _backGroundColour;

        //        public IAsyncCommand Delete { get; private set; }
        //        public bool DeleteCanExecute => Delete.CanExecute(null);

        public RelayToogleCommand Delete { get; private set; }
        public bool DeleteCanExecute => Delete.ShouldExecute;

        public IAsyncCommandAdaptor ShowDetail { get; private set; }
        public bool ShowDetailCanExecute => ShowDetail.CanExecute(null);

        private readonly ILogger _logger;

        private readonly IUiThreadInvoker _uiThreadInvoker;
        private readonly IUiStateController _uiStateController;

        public SkillViewModel(ILogger logger, IAsyncCommandFactory asyncCommandFactory, IUiThreadInvoker uiThreadInvoker, IUiStateController uiStateController) : base(uiThreadInvoker)
        {
            _logger = logger;
            _uiThreadInvoker = uiThreadInvoker;
            _uiStateController = uiStateController;

            SetupCommandBindings(asyncCommandFactory);
        }

        private void SetupCommandBindings(IAsyncCommandFactory asyncCommandFactory)
        {
//            Delete = asyncCommandFactory.Create<SkillViewModel>(DeleteCommandAsync);
            Delete = new RelayToogleCommand(DoAction);
            Delete.CanExecuteChanged += DeleteOnCanExecuteChanged;

            ShowDetail = asyncCommandFactory.Create(ShowDetailsCommandAsync);
            ShowDetail.CanExecuteChanged += ShowDetailOnCanExecuteChanged;
        }

        private bool _toggle = true;
        private void DoAction()
        {
            _logger.LogEntry();
            if (_toggle)
            {
                _uiStateController.IncUiLock();
            }
            else
            {
                _uiStateController.DecUiLock();
                Delete.ShouldExecute = false;
            }

            _toggle = !_toggle;
            _logger.LogExit();
        }

        private async Task DeleteCommandAsync(SkillViewModel uiSkill)
        {
            _logger.LogEntry();

            await Task.Delay(1000);

            _logger.LogExit();
        }

        private async Task ShowDetailsCommandAsync()
        {
            await Task.Delay(1000);

            ShowingDetails = !ShowingDetails;
            _logger.LogExit();
        }

        private void DeleteOnCanExecuteChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("DeleteCanExecute");
        }

        private void ShowDetailOnCanExecuteChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("ShowDetailCanExecute");
        }

        public override void Dispose()
        {
            Delete.CanExecuteChanged -= DeleteOnCanExecuteChanged;
//            Delete.Dispose();
            ShowDetail.CanExecuteChanged -= ShowDetailOnCanExecuteChanged;
            ShowDetail.Dispose();
        }
    }
}
