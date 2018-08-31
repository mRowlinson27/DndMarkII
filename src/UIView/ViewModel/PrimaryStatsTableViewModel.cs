
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
        public ObservableCollection<UiPrimaryStat> PrimaryStats { get; set; } = new ObservableCollection<UiPrimaryStat>();

        private IAsyncTaskRunner<IEnumerable<UiPrimaryStat>> _primaryStatRequestTaskRunner;

        private readonly ILogger _logger;

        private readonly IPrimaryStatsTableModel _model;

        private readonly IObservableHelper _observableHelper;

        private readonly IUiThreadInvoker _uiThreadInvoker;

        public PrimaryStatsTableViewModel(ILogger logger, IPrimaryStatsTableModel model, IObservableHelper observableHelper, IAsyncCommandFactory asyncCommandFactory,
            IAsyncTaskRunnerFactory asyncTaskRunnerFactory, IUiThreadInvoker uiThreadInvoker)
        {
            _logger = logger;
            _model = model;
            _observableHelper = observableHelper;
            _uiThreadInvoker = uiThreadInvoker;
            _model.PrimaryStatsUpdated += ModelOnPrimaryStatsUpdated;

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
            _uiThreadInvoker.Dispatch(() => DataAvailable = false);
            _primaryStatRequestTaskRunner.StartTask();
        }

        private void PrimaryStatRequestTaskRunnerOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSuccessfullyCompleted")
            {
                _uiThreadInvoker.Dispatch(RebindPrimaryStatsToResult);
            }
        }

        private void RebindPrimaryStatsToResult()
        {
            _logger.LogEntry();

            _observableHelper.Rebind(PrimaryStats, _primaryStatRequestTaskRunner.Result);
            DataAvailable = true;

            _logger.LogExit();
        }

        private void ModelOnPrimaryStatsUpdated(object sender, EventArgs e)
        {
            MakePrimaryStatRequest();
        }

        public void Dispose()
        {
            _primaryStatRequestTaskRunner.PropertyChanged -= PrimaryStatRequestTaskRunnerOnPropertyChanged;
            _primaryStatRequestTaskRunner.Dispose();
            _model.PrimaryStatsUpdated -= ModelOnPrimaryStatsUpdated;
        }
    }
}
