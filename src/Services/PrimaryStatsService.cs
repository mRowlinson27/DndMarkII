
namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using API;
    using API.Dto;
    using Database.API;
    using Utilities.API;

    public class PrimaryStatsService : IPrimaryStatsService
    {
        public event EventHandler PrimaryStatsUpdated;

        private readonly ILogger _logger;
        private readonly IPrimaryStatsRepo _primaryStatsRepo;

        public PrimaryStatsService(ILogger logger, IPrimaryStatsRepo primaryStatsRepo)
        {
            _logger = logger;
            _primaryStatsRepo = primaryStatsRepo;
        }

        public async Task<IEnumerable<PrimaryStat>> GetAllPrimaryStatsAsync()
        {
            var dbPrimaryStats = await _primaryStatsRepo.GetPrimaryStatsAsync();

            return CalculatePrimaryStats(dbPrimaryStats);
        }

        public Task AddOrUpdatePrimaryStatAsync(PrimaryStat skill)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<PrimaryStat> CalculatePrimaryStats(IEnumerable<Database.API.Dto.PrimaryStat> dbPrimaryStats)
        {
            var svcPrimaryStats = dbPrimaryStats.Select(p => new PrimaryStat(p)).ToList();
            foreach (var stat in svcPrimaryStats)
            {
                stat.AbilityModifier = (stat.AbilityScore / 2) - 5;
            }

            return svcPrimaryStats;
        }
    }
}
