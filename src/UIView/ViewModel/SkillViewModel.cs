
namespace UIView.ViewModel
{
    using System;
    using System.Threading;
    using API;
    using UIModel.API;
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
        public string PrimaryStatModifier => Skill.PrimaryStatModifier;

        public bool Class
        {
            get => Skill.Class;
            set => Skill.Class = value;
        }

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

        public bool InEdit { get; } = true;

        private string _backGroundColour;

        public IAsyncCommandAdaptor Delete { get; private set; }

        public IAsyncCommandAdaptor UpdateSkill { get; private set; }

        private readonly ILogger _logger;
        private readonly ISkillModel _model;
        private readonly IUiThreadInvoker _uiThreadInvoker;

        public SkillViewModel(ILogger logger, ISkillModel model, IAsyncCommandAdaptorFactory asyncCommandAdaptorFactory, IUiThreadInvoker uiThreadInvoker) : base(uiThreadInvoker)
        {
            _logger = logger;
            _model = model;
            _uiThreadInvoker = uiThreadInvoker;

            SetupCommandBindings(asyncCommandAdaptorFactory);
        }

        private void SetupCommandBindings(IAsyncCommandAdaptorFactory asyncCommandAdaptorFactory)
        {
            Delete = asyncCommandAdaptorFactory.CreateWithContext((Action) DeleteCommand);
            UpdateSkill = asyncCommandAdaptorFactory.CreateWithContext((Action) UpdateSkillCommand);
        }

        private void DeleteCommand()
        {
            _logger.LogEntry();

            Thread.Sleep(1000);

            _logger.LogExit();
        }

        private void UpdateSkillCommand()
        {
            _logger.LogEntry();

            _model.Update(Skill);

            _logger.LogExit();
        }

        public override void Dispose()
        {
            Delete.Dispose();
            UpdateSkill.Dispose();
        }
    }
}
