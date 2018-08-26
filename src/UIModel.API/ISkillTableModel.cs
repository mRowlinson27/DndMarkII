
namespace UIModel.API
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Dto;

    public interface ISkillTableModel : INotifyPropertyChanged
    {
        Task<IEnumerable<UiSkill>> RequestSkillsAsync();

        Task AddSkillAsync();

        Task RemoveSkillAsync(UiSkill uiSkill);
    }
}
