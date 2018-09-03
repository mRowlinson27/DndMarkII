
namespace UIModel
{
    using System.Threading.Tasks;
    using API;
    using API.Dto;
    using Services.API;

    public class PrimaryStatModel : IPrimaryStatModel
    {
        private readonly IPrimaryStatsService _primaryStatsService;

        public PrimaryStatModel(IPrimaryStatsService primaryStatsService)
        {
            _primaryStatsService = primaryStatsService;
        }

        public Task UpdateStatAsync(UiPrimaryStat primaryStat)
        {

            throw new System.NotImplementedException();
        }
    }
}
