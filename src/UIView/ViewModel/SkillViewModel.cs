
namespace UIView.ViewModel
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using API;
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

        public IAsyncCommandAdaptor Delete { get; private set; }

        public IAsyncCommandAdaptor ShowDetail { get; private set; }

        private readonly ILogger _logger;

        private readonly IUiThreadInvoker _uiThreadInvoker;

        public SkillViewModel(ILogger logger, IAsyncCommandFactory asyncCommandFactory, IUiThreadInvoker uiThreadInvoker) : base(uiThreadInvoker)
        {
            _logger = logger;
            _uiThreadInvoker = uiThreadInvoker;

            SetupCommandBindings(asyncCommandFactory);
        }

        private void SetupCommandBindings(IAsyncCommandFactory asyncCommandFactory)
        {
            Delete = asyncCommandFactory.Create((Action) DeleteCommand);
            ShowDetail = asyncCommandFactory.Create((Action) ShowDetailsCommand);
        }

        private void DeleteCommand(/*SkillViewModel uiSkill*/)
        {
            _logger.LogEntry();

            Thread.Sleep(1000);

            _logger.LogExit();
        }

        private void ShowDetailsCommand()
        {
            _logger.LogEntry();

            Thread.Sleep(1000);
            _uiThreadInvoker.Dispatch(() => ShowingDetails = !ShowingDetails);

            _logger.LogExit();
        }

        public override void Dispose()
        {
            Delete.Dispose();
            ShowDetail.Dispose();
        }
    }
}
