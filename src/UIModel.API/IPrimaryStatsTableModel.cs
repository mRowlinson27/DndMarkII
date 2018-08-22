
namespace UIModel.API
{
    using System.Collections.Generic;
    using Dto;

    public interface IPrimaryStatsTableModel
    {
        IList<PrimaryStat> PrimaryStats { get; set; }
    }
}
