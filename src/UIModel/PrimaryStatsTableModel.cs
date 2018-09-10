
namespace UIModel
{
    using System;
    using System.Collections.Generic;
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

        public IEnumerable<UiPrimaryStat> RequestPrimaryStats()
        {
            _logger.LogEntry();

            var svcPrimaryStats = _primaryStatsService.GetAllPrimaryStats();
            var uiPrimaryStats = _autoMapper.Map(svcPrimaryStats);

            _logger.LogExit();
            return uiPrimaryStats;
        }

        public void AddPrimaryStat()
        {
            throw new System.NotImplementedException();
        }
    }
}
