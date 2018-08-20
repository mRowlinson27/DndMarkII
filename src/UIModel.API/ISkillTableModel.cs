
namespace UIModel.API
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dto;

    public interface ISkillTableModel
    {
        IList<Skill> Skills { get; }

        Task AddSkillAsync();

        Task RemoveSkillAsync(Skill skill);
    }
}
