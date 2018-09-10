
namespace UIModel.API
{
    using System;
    using System.Collections.Generic;
    using Dto;

    public interface IPrimaryStatsTableModel
    {
        event EventHandler PrimaryStatsUpdated;

        IEnumerable<UiPrimaryStat> RequestPrimaryStats();

        void AddPrimaryStat();
    }
}
