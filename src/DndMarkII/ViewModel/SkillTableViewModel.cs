
namespace DndMarkII.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using AsyncCommands;
    using Extension;
    using UIModel.API;
    using UIModel.API.Dto;
    using Utilities.API;

    public class SkillTableViewModel : ViewModelBase, IDisposable
    {
        public IList<Skill> Skills => _skills;

        private readonly ObservableCollection<Skill> _skills = new ObservableCollection<Skill>();

        private NotifyTaskCompletion<IEnumerable<Skill>> _skillsRequest;


        public ICommand AddSkill { get; }

        public bool AddSkillCanExecute => AddSkill.CanExecute(null);


        public ICommand RemoveSkill { get; }

        public bool RemoveSkillCanExecute => RemoveSkill.CanExecute(null);


        public bool DataAvailable
        {
            get => _dataAvailable;
            set => Set(ref _dataAvailable, value, "DataAvailable");
        }
        private bool _dataAvailable = false;


        private readonly ILogger _logger;

        private readonly ISkillTableModel _model;

        public SkillTableViewModel(ILogger logger,ISkillTableModel model)
        {
            _logger = logger;
            _model = model;

            AddSkill = AsyncCommandFactory.Create(AddSkillCommandAsync);
            AddSkill.CanExecuteChanged += AddSkillOnCanExecuteChanged;

            RemoveSkill = AsyncCommandFactory.Create<Skill>(RemoveSkillCommandAsync);
            RemoveSkill.CanExecuteChanged += RemoveSkillOnCanExecuteChanged;

            _model.PropertyChanged += ModelOnPropertyChanged;
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
            if (_skillsRequest != null)
            {
                _skillsRequest.PropertyChanged -= SkillsRequestOnPropertyChanged;
            }

            DataAvailable = false;

            _skillsRequest = new NotifyTaskCompletion<IEnumerable<Skill>>(_model.RequestSkillsAsync());
            _skillsRequest.PropertyChanged += SkillsRequestOnPropertyChanged;
        }

        private void SkillsRequestOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsSuccessfullyCompleted")
            {
                return;
            }

            _skills.RebindTo(_skillsRequest.Result);

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
            _skillsRequest.PropertyChanged -= SkillsRequestOnPropertyChanged;
            _model.PropertyChanged -= ModelOnPropertyChanged;
        }
    }
}
