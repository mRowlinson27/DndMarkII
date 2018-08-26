
namespace Database.API
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Dto;

    public interface IPrimaryStatsRepo
    {
        Task<IEnumerable<PrimaryStat>> GetPrimaryStatsAsync();

        Task UpdatePrimaryStatsAsync(IEnumerable<PrimaryStat> stats);
    }
}
