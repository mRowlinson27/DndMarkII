
namespace Services.API
{
    using System.Collections.Generic;
    using Dto;
        
    public interface ISvcAutoMapper
    {
        IEnumerable<PrimaryStat> MapToSvc(IEnumerable<Database.API.Dto.PrimaryStat> dbPrimaryStats);

        PrimaryStat MapToSvc(Database.API.Dto.PrimaryStat dbPrimaryStat);

        IEnumerable<Skill> MapToSvc(IEnumerable<Database.API.Dto.Skill> dbSkills);

        Skill MapToSvc(Database.API.Dto.Skill dbSkill);

        IEnumerable<Database.API.Dto.PrimaryStat> MapToDb(IEnumerable<PrimaryStat> svcPrimaryStats);

        Database.API.Dto.PrimaryStat MapToDb(PrimaryStat svcPrimaryStat);

        IEnumerable<Database.API.Dto.Skill> MapToDb(IEnumerable<Skill> svcSkills);

        Database.API.Dto.Skill MapToDb(Skill svcSkill);
    }
}
