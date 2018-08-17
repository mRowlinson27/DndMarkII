
namespace DndMarkII.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using AsyncCommands;
    using Dto;
    using Model;

    public class SkillTableViewModel : ViewModelBase, IDisposable
    {
        public IList<Skill> Skills => _model.Skills;

        public ICommand AddSkill { get; }

        public bool AddSkillCanExecute => AddSkill.CanExecute(null);

        public ICommand RemoveSkill { get; }

        public bool RemoveSkillCanExecute => RemoveSkill.CanExecute(null);

//        public ICommand ToggleEditMode { get; }

//        public bool ToggleEditModeCanExecute => ToggleEditMode.CanExecute(null);

        private readonly SkillTableModel _model;

        public SkillTableViewModel(SkillTableModel model)
        {
            _model = model;

            AddSkill = AsyncCommandFactory.Create(AddSkillCommandAsync);
            AddSkill.CanExecuteChanged += AddSkillOnCanExecuteChanged;

            RemoveSkill = AsyncCommandFactory.Create<Skill>(RemoveSkillCommandAsync);
            RemoveSkill.CanExecuteChanged += RemoveSkillOnCanExecuteChanged;
        }

        private async Task AddSkillCommandAsync()
        {
            await _model.AddSkillAsync();
        }

        private async Task RemoveSkillCommandAsync(Skill skill)
        {
            await _model.RemoveSkillAsync(skill);
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
        }
    }
}
