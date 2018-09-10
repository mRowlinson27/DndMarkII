
namespace UIModel.API
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Dto;

    public interface ISkillTableModel : INotifyPropertyChanged, IDisposable
    {
        IEnumerable<UiSkill> RequestSkills();

        void AddSkill();

        void RemoveSkill(UiSkill uiSkill);
    }
}
