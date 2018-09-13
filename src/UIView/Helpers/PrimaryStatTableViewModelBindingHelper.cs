
namespace UIView.Helpers
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using API;
    using UIModel.API.Dto;

    public class PrimaryStatTableViewModelBindingHelper : IPrimaryStatTableViewModelBindingHelper
    {
        private ObservableCollection<IPrimaryStatViewModel> _currentCollection;

        private readonly IPrimaryStatViewModelFactory _primaryStatViewModelFactory;

        public PrimaryStatTableViewModelBindingHelper(IPrimaryStatViewModelFactory primaryStatViewModelFactory)
        {
            _primaryStatViewModelFactory = primaryStatViewModelFactory;
        }

        public void Rebind(ObservableCollection<IPrimaryStatViewModel> currentCollection, IEnumerable<UiPrimaryStat> statUpdate)
        {
            _currentCollection = currentCollection;
            var statUpdateList = statUpdate.ToList();

            RemoveExcessViewModelsIfNeeded(statUpdateList);

            UpdateValuesInRange(statUpdateList);

            CreateNewViewModelsWhereNeeded(statUpdateList);
        }

        private void RemoveExcessViewModelsIfNeeded(List<UiPrimaryStat> updateList)
        {
            var numberToBeRemoved = _currentCollection.Count - updateList.Count();
            if (numberToBeRemoved <= 0)
            {
                return;
            }
            for (var i = 0; i < numberToBeRemoved; i++)
            {
                _currentCollection.RemoveAt(_currentCollection.Count - numberToBeRemoved - 1);
            }
        }

        private void UpdateValuesInRange(List<UiPrimaryStat> updateList)
        {
            for (int i = 0; i < _currentCollection.Count; i++)
            {
                _currentCollection[i].PrimaryStat = updateList[i];
            }
        }

        private void CreateNewViewModelsWhereNeeded(List<UiPrimaryStat> skillUpdateList)
        {
            for (int i = _currentCollection.Count; i < skillUpdateList.Count; i++)
            {
                _currentCollection.Add(_primaryStatViewModelFactory.Create(skillUpdateList[i]));
            }
        }
    }
}
