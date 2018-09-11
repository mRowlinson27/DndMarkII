
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

        public PrimaryStatModel(IPrimaryStatsService primaryStatsService, IAutoMapper _autoMapper)
        {
            _primaryStatsService = primaryStatsService;
            this._autoMapper = _autoMapper;
        }

        public void Update(UiPrimaryStat primaryStat)
        {
            _primaryStatsService.UpdatePrimaryStat(_autoMapper.MapToSvcRequest(primaryStat));
        }
    }
}
