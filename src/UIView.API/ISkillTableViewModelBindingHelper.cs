
namespace UIView.API
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using UIModel.API.Dto;

    public interface  ISkillTableViewModelBindingHelper
    {
        void Rebind(ObservableCollection<ISkillViewModel> currentCollection, IEnumerable<UiSkill> skillUpdate);
    }
}
