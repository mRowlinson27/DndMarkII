
namespace UIModel.API
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Dto;

    public interface IPrimaryStatsTableModel
    {
        event EventHandler PrimaryStatsUpdated;

        Task<IEnumerable<UiPrimaryStat>> RequestPrimaryStatsAsync();

        Task AddPrimaryStatAsync();
    }
}
