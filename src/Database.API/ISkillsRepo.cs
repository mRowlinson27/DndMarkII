
namespace Database.API
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dto;

    public interface ISkillsRepo
    {
        Task<IEnumerable<Skill>> GetSkillsAsync();

        Task UpdateSkillsAsync(IEnumerable<Skill> skills);
    }
}
