
namespace UIView.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using UIModel.API;
    using UIModel.API.Dto;
    using UIUtilities.API;
    using UIUtilities.API.AsyncCommands;
    using Utilities.API;

    public class SkillTableViewModel : ViewModelBase, IDisposable
    {
        public ObservableCollection<Skill> Skills { get; set; } = new ObservableCollection<Skill>();

        private IAsyncTaskRunner<IEnumerable<Skill>> _skillsRequestTaskRunner;


        public ICommand AddSkill { get; private set; }

        public bool AddSkillCanExecute => AddSkill.CanExecute(null);


        public ICommand RemoveSkill { get; private set; }

        public bool RemoveSkillCanExecute => RemoveSkill.CanExecute(null);


        public bool DataAvailable
        {
            get => _dataAvailable;
            set => Set(ref _dataAvailable, value, "DataAvailable");
        }
        private bool _dataAvailable = false;


        private readonly ILogger _logger;

        private readonly ISkillTableModel _model;

        private readonly IObservableHelper _observableHelper;

        public SkillTableViewModel(ILogger logger, ISkillTableModel model, IObservableHelper observableHelper, IAsyncCommandFactory asyncCommandFactory, 
            IAsyncTaskRunnerFactory asyncTaskRunnerFactory)
        {
            _logger = logger;
            _observableHelper = observableHelper;

            _model = model;
            _model.PropertyChanged += ModelOnPropertyChanged;

            SetupTaskRunners(asyncTaskRunnerFactory);

            SetupCommandBindings(asyncCommandFactory);
        }

        private void SetupTaskRunners(IAsyncTaskRunnerFactory asyncTaskRunnerFactory)
        {
            _skillsRequestTaskRunner = asyncTaskRunnerFactory.Create(_model.RequestSkillsAsync);
            _skillsRequestTaskRunner.PropertyChanged += SkillsRequestTaskRunnerOnPropertyChanged;
        }

        private void SetupCommandBindings(IAsyncCommandFactory asyncCommandFactory)
        {
            AddSkill = asyncCommandFactory.Create(AddSkillCommandAsync);
            AddSkill.CanExecuteChanged += AddSkillOnCanExecuteChanged;

            RemoveSkill = asyncCommandFactory.Create<Skill>(RemoveSkillCommandAsync);
            RemoveSkill.CanExecuteChanged += RemoveSkillOnCanExecuteChanged;
        }

        public override void Init()
        {
            MakeSkillRequest();
        }

        private async Task AddSkillCommandAsync()
        {
            await _model.AddSkillAsync();
        }

        private async Task RemoveSkillCommandAsync(Skill skill)
        {
            await _model.RemoveSkillAsync(skill);
        }

        private void ModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MakeSkillRequest();
        }

        private void MakeSkillRequest()
        {
            DataAvailable = false;
            _skillsRequestTaskRunner.StartTask();
        }

        private void SkillsRequestTaskRunnerOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsSuccessfullyCompleted")
            {
                return;
            }

            _observableHelper.Rebind(Skills, _skillsRequestTaskRunner.Result);

            DataAvailable = true;
            _logger.LogExit();
        }

        private void AddSkillOnCanExecuteChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("AddSkillCanExecute");
        }

        private void RemoveSkillOnCanExecuteChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("RemoveSkillCanExecute");
        }

        public void Dispose()
        {
            AddSkill.CanExecuteChanged -= AddSkillOnCanExecuteChanged;
            RemoveSkill.CanExecuteChanged -= RemoveSkillOnCanExecuteChanged;
            _skillsRequestTaskRunner.PropertyChanged -= SkillsRequestTaskRunnerOnPropertyChanged;
            _skillsRequestTaskRunner.Dispose();
            _model.PropertyChanged -= ModelOnPropertyChanged;
        }
    }
}
