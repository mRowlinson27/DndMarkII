
namespace UIModel
{
    using API;
    using Services.API;

    public class PrimaryStatModelFactory : IPrimaryStatModelFactory
    {
        private readonly IPrimaryStatsService _primaryStatsService;
        private readonly IAutoMapper _autoMapper;

        public PrimaryStatModelFactory(IPrimaryStatsService primaryStatsService, IAutoMapper autoMapper)
        {
            _primaryStatsService = primaryStatsService;
            _autoMapper = autoMapper;
        }

        public IPrimaryStatModel Create()
        {
            return new PrimaryStatModel(_primaryStatsService, _autoMapper);
        }
    }
}
