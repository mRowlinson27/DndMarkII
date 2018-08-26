
namespace UIModel.API
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Dto;

    public interface IPrimaryStatsTableModel : INotifyPropertyChanged
    {
        Task<IEnumerable<PrimaryStat>> RequestPrimaryStatsAsync();

        Task AddPrimaryStatAsync();
    }
}
