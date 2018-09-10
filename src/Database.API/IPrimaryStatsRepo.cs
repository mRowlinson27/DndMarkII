
namespace Database.API
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dto;

    public interface IPrimaryStatsRepo
    {
        IEnumerable<PrimaryStat> GetPrimaryStats();

        void UpdatePrimaryStats(IEnumerable<PrimaryStat> stats);
    }
}
