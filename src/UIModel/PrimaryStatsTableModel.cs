
namespace UIModel
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using API;
    using API.Dto;

    public class PrimaryStatsTableModel : IPrimaryStatsTableModel
    {
        public Task RequestStartingDataAsync()
        {
            throw new System.NotImplementedException();
        }

        public IList<PrimaryStat> PrimaryStats { get; set; }
    }
}
