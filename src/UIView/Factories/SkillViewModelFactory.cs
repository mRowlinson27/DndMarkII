
namespace UIView.Factories
{
    using API;
    using UIModel.API.Dto;
    using UIUtilities.API;
    using UIUtilities.API.AsyncCommands;
    using Utilities.API;
    using ViewModel;

    public class SkillViewModelFactory : ISkillViewModelFactory
    {
        private readonly ILogger _logger;
        private readonly IObservableHelper _observableHelper;
        private readonly IAsyncCommandFactory _asyncCommandFactory;
        private readonly IAsyncTaskRunnerFactory _asyncTaskRunnerFactory;
        private readonly IUiThreadInvoker _uiThreadInvoker;

        public SkillViewModelFactory(ILogger logger, IObservableHelper observableHelper, IAsyncCommandFactory asyncCommandFactory,
            IAsyncTaskRunnerFactory asyncTaskRunnerFactory, IUiThreadInvoker uiThreadInvoker)
        {
            _logger = logger;
            _observableHelper = observableHelper;
            _asyncCommandFactory = asyncCommandFactory;
            _asyncTaskRunnerFactory = asyncTaskRunnerFactory;
            _uiThreadInvoker = uiThreadInvoker;
        }

        public ISkillViewModel Create(UiSkill skill)
        {
            var skillViewModel = new SkillViewModel(_logger, _observableHelper, _asyncCommandFactory, _asyncTaskRunnerFactory, _uiThreadInvoker) {Skill = skill};
            skillViewModel.Init();
            return skillViewModel;
        }
    }
}
