
namespace Services
{
    using System.Collections.Generic;
    using System.Linq;
    using API;
    using API.Dto;

    public class SvcAutoMapper : ISvcAutoMapper
    {
        public IEnumerable<PrimaryStat> MapToSvc(IEnumerable<Database.API.Dto.PrimaryStat> dbPrimaryStats)
        {
            return dbPrimaryStats.Select(MapToSvc);
        }

        public PrimaryStat MapToSvc(Database.API.Dto.PrimaryStat dbPrimaryStat)
        {
            return new PrimaryStat
            {
                Id = (AbilityType)dbPrimaryStat.Id,
                Name = dbPrimaryStat.Name,
                AbilityScore = dbPrimaryStat.AbilityScore,
            };
        }

        public IEnumerable<Skill> MapToSvc(IEnumerable<Database.API.Dto.Skill> dbSkills)
        {
            return dbSkills.Select(MapToSvc);
        }

        public Skill MapToSvc(Database.API.Dto.Skill dbSkill)
        {
            return new Skill
            {
                Id = dbSkill.Id,
                Name = dbSkill.Name,
                Ranks = dbSkill.Ranks,
                HasArmourCheckPenalty = dbSkill.HasArmourCheckPenalty,
                UseUntrained = dbSkill.UseUntrained,
                Trained = dbSkill.Trained,
                PrimaryStatId = (AbilityType)dbSkill.PrimaryStatId,
            };
        }

        public IEnumerable<Database.API.Dto.PrimaryStat> MapToDb(IEnumerable<PrimaryStat> svcPrimaryStats)
        {
            return svcPrimaryStats.Select(MapToDb);
        }

        public Database.API.Dto.PrimaryStat MapToDb(PrimaryStat svcPrimaryStat)
        {
            return new Database.API.Dto.PrimaryStat
            {
                Id = (Database.API.Dto.AbilityType)svcPrimaryStat.Id,
                Name = svcPrimaryStat.Name,
                AbilityScore = svcPrimaryStat.AbilityScore,
            };
        }

        public IEnumerable<Database.API.Dto.Skill> MapToDb(IEnumerable<Skill> svcSkills)
        {
            return svcSkills.Select(MapToDb);
        }

        public Database.API.Dto.Skill MapToDb(Skill svcSkill)
        {
            return new Database.API.Dto.Skill
            {
                Id = svcSkill.Id,
                Name = svcSkill.Name,
                Ranks = svcSkill.Ranks,
                HasArmourCheckPenalty = svcSkill.HasArmourCheckPenalty,
                UseUntrained = svcSkill.UseUntrained,
                Trained = svcSkill.Trained,
                PrimaryStatId = (Database.API.Dto.AbilityType)svcSkill.PrimaryStatId,
            };
        }
    }
}
