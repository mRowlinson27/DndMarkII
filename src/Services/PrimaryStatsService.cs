
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

        public Dictionary<AbilityType, PrimaryStat> CachedPrimaryStats { get; set; }

        private readonly ILogger _logger;
        private readonly IPrimaryStatsRepo _primaryStatsRepo;
        private readonly ISvcAutoMapper _svcAutoMapper;

        public PrimaryStatsService(ILogger logger, IPrimaryStatsRepo primaryStatsRepo, ISvcAutoMapper svcAutoMapper)
        {
            _logger = logger;
            _primaryStatsRepo = primaryStatsRepo;
            _svcAutoMapper = svcAutoMapper;
        }

        public IEnumerable<PrimaryStat> GetAllPrimaryStats()
        {
            _logger.LogEntry();

            if (CachedPrimaryStats == null)
            {
                PopulatePrimaryStats();
            }

            _logger.LogExit();
            return CachedPrimaryStats.Values.OrderBy(stat => stat.Id);
        }

        public void UpdatePrimaryStat(PrimaryStatUpdateRequest skill)
        {
            if (skill.AbilityScore == CachedPrimaryStats[skill.Id].AbilityScore)
            {
                return;
            }
            CachedPrimaryStats[skill.Id].AbilityScore = skill.AbilityScore;
            AddModifierToPrimaryStat(CachedPrimaryStats[skill.Id]);

            PrimaryStatsUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void AddPrimaryStat(PrimaryStatUpdateRequest skill)
        {
            throw new NotImplementedException();
        }

        private void PopulatePrimaryStats()
        {
            var dbPrimaryStats = _primaryStatsRepo.GetPrimaryStats();
            var svcPrimaryStats = _svcAutoMapper.MapToSvc(dbPrimaryStats);
            CachedPrimaryStats = svcPrimaryStats.Select(AddModifierToPrimaryStat).ToDictionary(stat => stat.Id);
        }

        private PrimaryStat AddModifierToPrimaryStat(PrimaryStat primaryStat)
        {
            primaryStat.AbilityModifier = CalculateAbilityModifier(primaryStat);
            return primaryStat;
        }

        private int CalculateAbilityModifier(PrimaryStat primaryStat)
        {
            return (primaryStat.AbilityScore / 2) - 5;
        }
    }
}
