
namespace Services.API
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Database.API.Dto;

    public interface IPrimaryStatsService
    {
        event EventHandler PrimaryStatsUpdated;

        Task<IEnumerable<PrimaryStat>> GetAllSkillsAsync();

        Task AddOrUpdatePrimaryStatAsync(PrimaryStat skill);
    }
}
