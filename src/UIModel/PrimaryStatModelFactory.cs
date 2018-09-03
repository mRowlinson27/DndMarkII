
namespace UIModel
{
    using API;
    using Services.API;

    public class PrimaryStatModelFactory : IPrimaryStatModelFactory
    {
        private readonly IPrimaryStatsService _primaryStatsService;

        public PrimaryStatModelFactory(IPrimaryStatsService primaryStatsService)
        {
            _primaryStatsService = primaryStatsService;
        }

        public IPrimaryStatModel Create()
        {
            return new PrimaryStatModel(_primaryStatsService);
        }
    }
}
