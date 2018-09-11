
namespace UIView.Factories
{
    using API;
    using UIModel.API;
    using UIModel.API.Dto;
    using UIUtilities.API;
    using UIUtilities.API.AsyncCommands;
    using Utilities.API;
    using ViewModel;

    public class SkillViewModelFactory : ISkillViewModelFactory
    {
        private readonly ILogger _logger;
        private readonly ISkillModelFactory _skillModelFactory;
        private readonly IAsyncCommandAdaptorFactory _asyncCommandAdaptorFactory;
        private readonly IUiThreadInvoker _uiThreadInvoker;

        public SkillViewModelFactory(ILogger logger, ISkillModelFactory skillModelFactory, IAsyncCommandAdaptorFactory asyncCommandAdaptorFactory, IUiThreadInvoker uiThreadInvoker)
        {
            _logger = logger;
            _skillModelFactory = skillModelFactory;
            _asyncCommandAdaptorFactory = asyncCommandAdaptorFactory;
            _uiThreadInvoker = uiThreadInvoker;
        }

        public ISkillViewModel Create(UiSkill skill)
        {
            var skillViewModel = new SkillViewModel(_logger, _skillModelFactory.Create(), _asyncCommandAdaptorFactory, _uiThreadInvoker) {Skill = skill};
            skillViewModel.Init();
            return skillViewModel;
        }
    }
}
