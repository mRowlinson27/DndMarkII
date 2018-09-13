
namespace UIModel
{
    using System.Threading.Tasks;
    using API;
    using API.Dto;
    using Services.API;

    public class PrimaryStatModel : IPrimaryStatModel
    {
        private readonly IPrimaryStatsService _primaryStatsService;
        private readonly IAutoMapper _autoMapper;

        public PrimaryStatModel(IPrimaryStatsService primaryStatsService, IAutoMapper autoMapper)
        {
            _primaryStatsService = primaryStatsService;
            _autoMapper = autoMapper;
        }

        public void Update(UiPrimaryStat primaryStat)
        {
            _primaryStatsService.UpdatePrimaryStat(_autoMapper.MapToSvcRequest(primaryStat));
        }
    }
}
