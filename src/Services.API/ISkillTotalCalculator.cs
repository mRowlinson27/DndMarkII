
namespace Services.API
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dto;

    public interface ISkillTotalCalculator
    {
        Task<IEnumerable<Skill>> AddTotalsAsync(IEnumerable<Skill> skills);
    }
}
