
namespace Services.API
{
    using System;
    using System.Collections.Generic;
    using Database.API.Dto;

    public interface IPrimaryStatsService
    {
        event EventHandler PrimaryStatsUpdated;

        IEnumerable<PrimaryStat> GetAllSkills();

        void AddOrUpdatePrimaryStat(PrimaryStat skill);
    }
}
