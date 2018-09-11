
namespace Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using API;
    using API.Dto;

    public class SkillTotalCalculator : ISkillTotalCalculator
    {
        private readonly IPrimaryStatsService _primaryStatsService;

        public SkillTotalCalculator(IPrimaryStatsService primaryStatsService)
        {
            _primaryStatsService = primaryStatsService;
        }

        public IEnumerable<Skill> AddTotals(IEnumerable<Skill> skills)
        {
            var skillsList = skills.ToList();
            var abilityScores = _primaryStatsService.GetAllPrimaryStats().ToDictionary(ab => ab.Id);
            foreach (var skill in skillsList)
            {
                AddTotalToSkill(skill, abilityScores);
            }

            return skillsList;
        }

        private void AddTotalToSkill(Skill skill, Dictionary<AbilityType, PrimaryStat> abilityScores)
        {
            skill.Total = skill.Ranks;
            if (skill.Class && skill.Ranks > 0)
            {
                skill.Total += 3;
            }

            if (abilityScores.ContainsKey(skill.PrimaryStatId))
            {
                skill.Total += abilityScores[skill.PrimaryStatId].AbilityModifier;
                skill.PrimaryStatModifier = abilityScores[skill.PrimaryStatId].AbilityModifier;
            }
        }
    }
}
