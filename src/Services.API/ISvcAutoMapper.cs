
namespace Services.API
{
    using System.Collections.Generic;
    using Dto;
        
    public interface ISvcAutoMapper
    {
        IEnumerable<PrimaryStat> Map(IEnumerable<Database.API.Dto.PrimaryStat> dbPrimaryStats);

        IEnumerable<Skill> Map(IEnumerable<Database.API.Dto.Skill> dbSkills);
    }
}
