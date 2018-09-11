
namespace UIView.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using API;
    using UIModel.API;
    using UIModel.API.Dto;
    using UIUtilities.API;
    using UIUtilities.API.AsyncCommands;
    using Utilities.API;

    public class SkillTableViewModel : ViewModelBase
    {
        public IList<ISkillViewModel> SkillViewModels { get; set; } = new ObservableCollection<ISkillViewModel>();

        private IAsyncCommand<IEnumerable<UiSkill>> _skillsRequestCommand;

        public IAsyncCommandAdaptor AddSkill { get; private set; }

        private readonly ILogger _logger;

        private readonly ISkillTableModel _model;

        private readonly IObservableHelper _observableHelper;

        private readonly IUiThreadInvoker _uiThreadInvoker;

        private readonly ISkillViewModelFactory _skillViewModelFactory;

        private readonly IUiStateController _uiStateController;

        public SkillTableViewModel(ILogger logger, ISkillTableModel model, IObservableHelper observableHelper, IAsyncCommandFactory asyncCommandFactory,
            IAsyncCommandAdaptorFactory asyncCommandAdaptorFactory, IUiThreadInvoker uiThreadInvoker, ISkillViewModelFactory skillViewModelFactory, IUiStateController uiStateController) : base(uiThreadInvoker)
        {
            _logger = logger;
            _observableHelper = observableHelper;
            _uiThreadInvoker = uiThreadInvoker;
            _skillViewModelFactory = skillViewModelFactory;
            _uiStateController = uiStateController;

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

            var newSkillModelList = _skillsRequestCommand.Execution.Result.Select(s => _skillViewModelFactory.Create(s)).ToList();
            for (int i = 0; i < newSkillModelList.Count; i++)
            {
                newSkillModelList[i].BackGroundColour = i % 2 == 0 ? Constants.SkillModelEvenIndexBackGroundColour : Constants.SkillModelOddIndexBackGroundColour;
            }
            _observableHelper.Rebind(SkillViewModels, newSkillModelList);

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
