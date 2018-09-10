
namespace Services.API
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dto;

    public interface IPrimaryStatsService
    {
        event EventHandler PrimaryStatsUpdated;

        IEnumerable<PrimaryStat> GetAllPrimaryStats();

        void AddOrUpdatePrimaryStat(PrimaryStat skill);
    }
}
