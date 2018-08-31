
namespace Database.API
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dto;

    public interface ISkillsRepo
    {
        Task<IEnumerable<Skill>> GetSkillsAsync();

        Task AddSkillAsync(Skill skill);

        Task AddSkillsAsync(IEnumerable<Skill> skills);

        Task UpdateSkillAsync(Skill skill);

        Task UpdateSkillsAsync(IEnumerable<Skill> skills);
    }
}
