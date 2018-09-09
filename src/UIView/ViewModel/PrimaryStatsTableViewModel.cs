
namespace UIView.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using API;
    using Neutronium.MVVMComponents.Relay;
    using UIModel.API;
    using UIModel.API.Dto;
    using UIUtilities.API;
    using UIUtilities.API.AsyncCommands;
    using Utilities.API;

    public class PrimaryStatsTableViewModel : ViewModelBase
    {
        public ObservableCollection<IPrimaryStatViewModel> PrimaryStats { get; set; } = new ObservableCollection<IPrimaryStatViewModel>();

        public bool InEdit { get; set; } = true;

        public IAsyncCommandAdaptor Delete { get; private set; }

        private IAsyncTaskRunner<IEnumerable<UiPrimaryStat>> _primaryStatRequestTaskRunner;

        private readonly ILogger _logger;

        private readonly IPrimaryStatsTableModel _model;

        private readonly IObservableHelper _observableHelper;

        private readonly IUiThreadInvoker _uiThreadInvoker;

        private readonly IPrimaryStatViewModelFactory _primaryStatViewModelFactory;

        private readonly IUiStateController _uiStateController;

        public PrimaryStatsTableViewModel(ILogger logger, IPrimaryStatsTableModel model, IObservableHelper observableHelper, IAsyncCommandFactory asyncCommandFactory,
            IAsyncTaskRunnerFactory asyncTaskRunnerFactory, IUiThreadInvoker uiThreadInvoker, IPrimaryStatViewModelFactory primaryStatViewModelFactory, IUiStateController uiStateController) : base(uiThreadInvoker)
        {
            _logger = logger;
            _model = model;
            _observableHelper = observableHelper;
            _uiThreadInvoker = uiThreadInvoker;
            _primaryStatViewModelFactory = primaryStatViewModelFactory;
            _uiStateController = uiStateController;
            _model.PrimaryStatsUpdated += ModelOnPrimaryStatsUpdated;

            SetupTaskRunners(asyncTaskRunnerFactory);

            Delete = asyncCommandFactory.Create(() => { _logger.LogEntry(); });
            _uiStateController.UiLockUpdated += UiStateControllerOnUiLockUpdated;
        }

        private void UiStateControllerOnUiLockUpdated(object sender, EventArgs e)
        {
            Delete.ShouldExecute = !_uiStateController.UiLocked;
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
            _uiStateController.IncUiLock();

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

            var primaryStatViewModels = _primaryStatRequestTaskRunner.Result.Select(uiPrimaryStat => _primaryStatViewModelFactory.Create(uiPrimaryStat));
            _observableHelper.Rebind(PrimaryStats, primaryStatViewModels);
            DataAvailable = true;
            _uiStateController.DecUiLock();

            _logger.LogExit();
        }

        private void ModelOnPrimaryStatsUpdated(object sender, EventArgs e)
        {
            MakePrimaryStatRequest();
        }

        private async Task DeleteCommandAsync(PrimaryStatsTableViewModel uiSkill)
        {
            _logger.LogEntry();

            await Task.Delay(1000);

            _logger.LogExit();
        }

        public override void Dispose()
        {
            _primaryStatRequestTaskRunner.PropertyChanged -= PrimaryStatRequestTaskRunnerOnPropertyChanged;
            _primaryStatRequestTaskRunner.Dispose();
            _model.PrimaryStatsUpdated -= ModelOnPrimaryStatsUpdated;
        }
    }
}
