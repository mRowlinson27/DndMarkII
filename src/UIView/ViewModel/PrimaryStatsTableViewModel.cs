
namespace UIView.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using UIModel.API;
    using UIModel.API.Dto;
    using UIUtilities.API;

    public class PrimaryStatsTableViewModel : ViewModelBase, IDisposable
    {
        public ObservableCollection<PrimaryStat> PrimaryStats { get; set; } = new ObservableCollection<PrimaryStat>();

        private IAsyncTaskRunner<IEnumerable<PrimaryStat>> _primaryStatRequestTaskRunner;

        private readonly IPrimaryStatsTableModel _model;

        public PrimaryStatsTableViewModel(IPrimaryStatsTableModel model)
        {
            _model = model;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
