
namespace Services.API
{
    using System.Collections.Generic;
    using Dto;

    public interface ISkillTotalCalculator
    {
        IEnumerable<Skill> AddTotals(IEnumerable<Skill> skills);
        Skill AddTotal(Skill skills);
    }
}
