
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
            var abilityScores = _primaryStatsService.GetAllPrimaryStats().ToDictionary(ab => ab.Id);
            return skills.Select((s) => AddTotalToSkill(s, abilityScores));
        }

        private Skill AddTotalToSkill(Skill skill, Dictionary<AbilityType, PrimaryStat> abilityScores)
        {
            skill.Total = skill.Ranks;
            if (skill.Trained && skill.Ranks > 0)
            {
                skill.Total += 3;
            }

            if (abilityScores.ContainsKey(skill.PrimaryStatId))
            {
                skill.Total += abilityScores[skill.PrimaryStatId].AbilityModifier;
            }

            return skill;
        }
    }
}
