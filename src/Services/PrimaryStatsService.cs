
namespace Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using API;
    using Database.API;
    using Database.API.Dto;
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
            return await _primaryStatsRepo.GetPrimaryStatsAsync();
        }

        public Task AddOrUpdatePrimaryStatAsync(PrimaryStat skill)
        {
            throw new NotImplementedException();
        }
    }
}
