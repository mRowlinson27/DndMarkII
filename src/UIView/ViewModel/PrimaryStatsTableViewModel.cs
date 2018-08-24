
namespace UIView.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using UIModel.API;
    using UIModel.API.Dto;
    using UIUtilities.API;
    using UIUtilities.API.AsyncCommands;
    using Utilities.API;

    public class PrimaryStatsTableViewModel : ViewModelBase, IDisposable
    {
        public ObservableCollection<PrimaryStat> PrimaryStats { get; set; } = new ObservableCollection<PrimaryStat>();

        private IAsyncTaskRunner<IEnumerable<PrimaryStat>> _primaryStatRequestTaskRunner;

        private readonly ILogger _logger;

        private readonly IPrimaryStatsTableModel _model;

        private readonly IObservableHelper _observableHelper;

        public PrimaryStatsTableViewModel(ILogger logger, IPrimaryStatsTableModel model, IObservableHelper observableHelper, IAsyncCommandFactory asyncCommandFactory,
            IAsyncTaskRunnerFactory asyncTaskRunnerFactory)
        {
            _logger = logger;
            _model = model;
            _observableHelper = observableHelper;
            _model.PropertyChanged += ModelOnPropertyChanged;

            SetupTaskRunners(asyncTaskRunnerFactory);
        }

        public override void Init()
        {
            MakePrimaryStatRequest();
        }

        private void SetupTaskRunners(IAsyncTaskRunnerFactory asyncTaskRunnerFactory)
        {
            _primaryStatRequestTaskRunner = asyncTaskRunnerFactory.Create(_model.RequestPrimaryStatsAsync);
            _primaryStatRequestTaskRunner.PropertyChanged += PrimaryStatRequestTaskRunnerOnPropertyChanged;
        }

        private void MakePrimaryStatRequest()
        {
            _primaryStatRequestTaskRunner.StartTask();
            DataAvailable = false;
        }

        private void PrimaryStatRequestTaskRunnerOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsSuccessfullyCompleted")
            {
                return;
            }

            _observableHelper.Rebind(PrimaryStats, _primaryStatRequestTaskRunner.Result);

            DataAvailable = true;
            _logger.LogExit();
        }

        private void ModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MakePrimaryStatRequest();
        }

        public void Dispose()
        {
            _primaryStatRequestTaskRunner.PropertyChanged -= PrimaryStatRequestTaskRunnerOnPropertyChanged;
            _primaryStatRequestTaskRunner.Dispose();
            _model.PropertyChanged -= ModelOnPropertyChanged;
        }
    }
}
