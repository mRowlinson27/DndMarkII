
namespace UIView.Helpers
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using API;
    using UIModel.API.Dto;
    using UIUtilities.API;

    public class SkillTableViewModelBindingHelper : ISkillTableViewModelBindingHelper
    {
        private ObservableCollection<ISkillViewModel> _currentCollection;

        private readonly ISkillViewModelFactory _skillViewModelFactory;

        public SkillTableViewModelBindingHelper(ISkillViewModelFactory skillViewModelFactory)
        {
            _skillViewModelFactory = skillViewModelFactory;
        }

        public void Rebind(ObservableCollection<ISkillViewModel> currentCollection, IEnumerable<UiSkill> skillUpdate)
        {
            _currentCollection = currentCollection;
            var skillUpdateList = skillUpdate.ToList();

            RemoveExcessViewModelsIfNeeded(skillUpdateList);

            UpdateValuesInRange(skillUpdateList);

            CreateNewViewModelsWhereNeeded(skillUpdateList);

            SetViewModelStyles();
        }

        private void RemoveExcessViewModelsIfNeeded(IEnumerable<UiSkill> skillUpdate)
        {
            var numberToBeRemoved = _currentCollection.Count - skillUpdate.Count();
            if (numberToBeRemoved <= 0)
            {
                return;
            }
            for (var i = 0; i < numberToBeRemoved; i++)
            {
                _currentCollection.RemoveAt(_currentCollection.Count - numberToBeRemoved - 1);
            }
        }

        private void UpdateValuesInRange(List<UiSkill> skillUpdateList)
        {
            for (int i = 0; i < _currentCollection.Count; i++)
            {
                _currentCollection[i].Skill = skillUpdateList[i];
            }
        }

        private void CreateNewViewModelsWhereNeeded(List<UiSkill> skillUpdateList)
        {
            for (int i = _currentCollection.Count; i < skillUpdateList.Count; i++)
            {
                _currentCollection.Add(_skillViewModelFactory.Create(skillUpdateList[i]));
            }
        }

        private void SetViewModelStyles()
        {
            for (int i = 0; i < _currentCollection.Count; i++)
            {
                _currentCollection[i].BackGroundColour = i % 2 == 0 ? Constants.SkillModelEvenIndexBackGroundColour : Constants.SkillModelOddIndexBackGroundColour;
            }
        }
    }
}
