
namespace UIModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using API;
    using API.Dto;
    using Services.API;
    using Utilities.API;

    public class PrimaryStatsTableModel : IPrimaryStatsTableModel
    {
        public event EventHandler PrimaryStatsUpdated
        {
            add => _primaryStatsService.PrimaryStatsUpdated += value;
            remove => _primaryStatsService.PrimaryStatsUpdated -= value;
        }

        private readonly ILogger _logger;

        private readonly IPrimaryStatsService _primaryStatsService;

        private readonly IAutoMapper _autoMapper;

        public PrimaryStatsTableModel(ILogger logger, IPrimaryStatsService primaryStatsService, IAutoMapper autoMapper)
        {
            _logger = logger;
            _primaryStatsService = primaryStatsService;
            _autoMapper = autoMapper;
        }

        public async Task<IEnumerable<UiPrimaryStat>> RequestPrimaryStatsAsync()
        {
            _logger.LogEntry();

            var svcPrimaryStats = await _primaryStatsService.GetAllPrimaryStatsAsync().ConfigureAwait(false);
            var uiPrimaryStats = _autoMapper.Map(svcPrimaryStats);

            _logger.LogExit();
            return uiPrimaryStats;
        }

        public async Task AddPrimaryStatAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
