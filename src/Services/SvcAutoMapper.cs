
namespace Services
{
    using System.Collections.Generic;
    using System.Linq;
    using API;
    using API.Dto;

    public class SvcAutoMapper : ISvcAutoMapper
    {
        public IEnumerable<PrimaryStat> Map(IEnumerable<Database.API.Dto.PrimaryStat> dbPrimaryStats)
        {
            return dbPrimaryStats.Select(MapPrimaryStat);
        }

        public IEnumerable<Skill> Map(IEnumerable<Database.API.Dto.Skill> dbSkills)
        {
            return dbSkills.Select(MapSkill);
        }

        private PrimaryStat MapPrimaryStat(Database.API.Dto.PrimaryStat dbPrimaryStat)
        {
            return new PrimaryStat
            {
                Id = (AbilityType) dbPrimaryStat.Id,
                Name = dbPrimaryStat.Name,
                AbilityScore = dbPrimaryStat.AbilityScore,
            };
        }

        private Skill MapSkill(Database.API.Dto.Skill dbSkill)
        {
            return new Skill
            {
                Id = dbSkill.Id,
                Name = dbSkill.Name,
                Ranks = dbSkill.Ranks,
                HasArmourCheckPenalty = dbSkill.HasArmourCheckPenalty,
                UseUntrained = dbSkill.UseUntrained,
                Trained = dbSkill.Trained,
                PrimaryStatId = (AbilityType) dbSkill.PrimaryStatId,
            };
        }
    }
}
