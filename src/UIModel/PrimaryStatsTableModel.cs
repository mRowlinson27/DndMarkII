
namespace UIModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using API;
    using API.Dto;
    using Utilities.API;

    public class PrimaryStatsTableModel : IPrimaryStatsTableModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly List<PrimaryStat> _primaryStats;

        private readonly ILogger _logger;

        private readonly Random _generator = new Random(DateTime.Now.Millisecond);

        public PrimaryStatsTableModel(ILogger logger)
        {
            _logger = logger;
            _primaryStats = GenerateStats();
        }

        public async Task<IEnumerable<PrimaryStat>> RequestPrimaryStatsAsync()
        {
            _logger.LogEntry();
            await Task.Delay(_generator.Next(0, 4000)).ConfigureAwait(true);
            _logger.LogExit();
            return _primaryStats;
        }

        public async Task AddPrimaryStatAsync()
        {
            throw new System.NotImplementedException();
        }

        private List<PrimaryStat> GenerateStats()
        {
            return new List<PrimaryStat>();
        }
    }
}
