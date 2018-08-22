
namespace UIView.ViewModel
{
    using System.Collections.Generic;
    using UIModel.API;
    using UIModel.API.Dto;

    public class PrimaryStatsTableViewModel : ViewModelBase
    {
        public IList<PrimaryStat> PrimaryStats => _model.PrimaryStats;

        private readonly IPrimaryStatsTableModel _model;

        public PrimaryStatsTableViewModel(IPrimaryStatsTableModel model)
        {
            _model = model;
        }
    }
}
