
namespace UIView.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using API;
    using UIModel.API;
    using UIModel.API.Dto;
    using UIUtilities.API;
    using UIUtilities.API.AsyncCommands;
    using Utilities.API;

    public class SkillTableViewModel : ViewModelBase
    {
        public ObservableCollection<ISkillViewModel> SkillViewModels { get; set; } = new ObservableCollection<ISkillViewModel>();

        private IAsyncCommand<IEnumerable<UiSkill>> _skillsRequestCommand;

        public IAsyncCommandAdaptor AddSkill { get; private set; }

        private readonly ILogger _logger;
        private readonly ISkillTableModel _model;
        private readonly IUiThreadInvoker _uiThreadInvoker;
        private readonly IUiStateController _uiStateController;
        private readonly ISkillTableViewModelBindingHelper _bindingHelper;

        public SkillTableViewModel(ILogger logger, ISkillTableModel model, IAsyncCommandFactory asyncCommandFactory, IAsyncCommandAdaptorFactory asyncCommandAdaptorFactory,
            IUiThreadInvoker uiThreadInvoker, IUiStateController uiStateController, ISkillTableViewModelBindingHelper bindingHelper) : base(uiThreadInvoker)
        {
            _logger = logger;
            _uiThreadInvoker = uiThreadInvoker;
            _uiStateController = uiStateController;
            _bindingHelper = bindingHelper;

            _model = model;
            _model.PropertyChanged += ModelOnPropertyChanged;

            SetupTaskRunners(asyncCommandFactory);

            SetupCommandBindings(asyncCommandAdaptorFactory);
        }

        private void SetupTaskRunners(IAsyncCommandFactory asyncTaskRunnerFactory)
        {
            _skillsRequestCommand = asyncTaskRunnerFactory.CreateResultCommand(_model.RequestSkills);
            _skillsRequestCommand.PropertyChanged += SkillsRequestCommandOnPropertyChanged;
        }

        private void SetupCommandBindings(IAsyncCommandAdaptorFactory asyncCommandAdaptorFactory)
        {
            AddSkill = asyncCommandAdaptorFactory.CreateWithContext((Action) AddSkillCommand);
        }

        public override void Init()
        {
            MakeSkillRequest();
        }

        private void AddSkillCommand()
        {
            _model.AddSkill();
        }

        private void ModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MakeSkillRequest();
        }

        private void MakeSkillRequest()
        {
            _uiStateController.IncUiLock();

            _uiThreadInvoker.Dispatch(() => DataAvailable = false);
            _skillsRequestCommand.Execute(null);
        }

        private void SkillsRequestCommandOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSuccessfullyCompleted")
            {
                _uiThreadInvoker.Dispatch(RebindSkillsToResult);
            }
        }

        private void RebindSkillsToResult()
        {
            _logger.LogEntry();

            _bindingHelper.Rebind(SkillViewModels, _skillsRequestCommand.Execution.Result);

            DataAvailable = true;

            _uiStateController.DecUiLock();
            _logger.LogExit();
        }

        public override void Dispose()
        {
            AddSkill.Dispose();
            _skillsRequestCommand.PropertyChanged -= SkillsRequestCommandOnPropertyChanged;
            _skillsRequestCommand.Dispose();
            _model.PropertyChanged -= ModelOnPropertyChanged;
            _model.Dispose();
        }
    }
}
