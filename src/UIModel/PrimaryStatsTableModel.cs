
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

        private readonly List<UiPrimaryStat> _primaryStats;

        private readonly ILogger _logger;

        private readonly Random _generator = new Random(DateTime.Now.Millisecond);

        public PrimaryStatsTableModel(ILogger logger)
        {
            _logger = logger;
            _primaryStats = GenerateStats();
        }

        public async Task<IEnumerable<UiPrimaryStat>> RequestPrimaryStatsAsync()
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

        private List<UiPrimaryStat> GenerateStats()
        {
            return new List<UiPrimaryStat>
            {
                new UiPrimaryStat
                {
                    Name = "Str",
                    AbilityScore = 16,
                    AbilityModifier = 3
                },
                new UiPrimaryStat
                {
                    Name = "Con",
                    AbilityScore = 10,
                    AbilityModifier = 0
                },
                new UiPrimaryStat
                {
                    Name = "Dex",
                    AbilityScore = 18,
                    AbilityModifier = 4
                }
                ,new UiPrimaryStat
                {
                    Name = "Wis",
                    AbilityScore = 8,
                    AbilityModifier = -1
                },
                new UiPrimaryStat
                {
                    Name = "Cha",
                    AbilityScore = 16,
                    AbilityModifier = 3
                },
                new UiPrimaryStat
                {
                    Name = "Int",
                    AbilityScore = 14,
                    AbilityModifier = 2
                }
            };
        }
    }
}
