
namespace Database.API
{
    using System.Collections.Generic;
    using Dto;

    public interface IPrimaryStatsRepo
    {
        IEnumerable<PrimaryStat> Get();

        void Update(IEnumerable<PrimaryStat> stats);
    }
}
