
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

        private readonly ISvcAutoMapper _svcAutoMapper;

        public PrimaryStatsService(ILogger logger, IPrimaryStatsRepo primaryStatsRepo, ISvcAutoMapper svcAutoMapper)
        {
            _logger = logger;
            _primaryStatsRepo = primaryStatsRepo;
            _svcAutoMapper = svcAutoMapper;
        }

        public async Task<IEnumerable<PrimaryStat>> GetAllPrimaryStatsAsync()
        {
            _logger.LogEntry();

            var dbPrimaryStats = await _primaryStatsRepo.GetPrimaryStatsAsync().ConfigureAwait(false);
            var svcPrimaryStats = _svcAutoMapper.MapToSvc(dbPrimaryStats);
            var svcPrimaryStatsWithModifier = svcPrimaryStats.Select(AddModifierToPrimaryStat);

            _logger.LogExit();
            return svcPrimaryStatsWithModifier;
        }

        public Task AddOrUpdatePrimaryStatAsync(PrimaryStat skill)
        {
            throw new NotImplementedException();
        }

        private PrimaryStat AddModifierToPrimaryStat(PrimaryStat primaryStat)
        {
            primaryStat.AbilityModifier = (primaryStat.AbilityScore / 2) - 5;

            return primaryStat;
        }
    }
}
