﻿
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

        private IAsyncCommand<IEnumerable<UiPrimaryStat>> _primaryStatRequestCommand;

        private readonly ILogger _logger;
        private readonly IPrimaryStatsTableModel _model;
        private readonly IPrimaryStatTableViewModelBindingHelper _bindingHelper;
        private readonly IUiThreadInvoker _uiThreadInvoker;
        private readonly IUiStateController _uiStateController;

        public PrimaryStatsTableViewModel(ILogger logger, IPrimaryStatsTableModel model, IPrimaryStatTableViewModelBindingHelper bindingHelper, IAsyncCommandFactory asyncCommandFactory,
            IAsyncCommandAdaptorFactory asyncCommandAdaptorFactory, IUiThreadInvoker uiThreadInvoker, IUiStateController uiStateController) : base(uiThreadInvoker)
        {
            _logger = logger;
            _model = model;
            _bindingHelper = bindingHelper;
            _uiThreadInvoker = uiThreadInvoker;
            _uiStateController = uiStateController;
            _model.PrimaryStatsUpdated += ModelOnPrimaryStatsUpdated;

            SetupTaskRunners(asyncCommandFactory);

            Delete = asyncCommandAdaptorFactory.CreateWithContext(() => { _logger.LogEntry(); });
        }

        public override void Init()
        {
            MakePrimaryStatRequest();
        }

        private void SetupTaskRunners(IAsyncCommandFactory asyncCommandFactory)
        {
            _primaryStatRequestCommand = asyncCommandFactory.CreateResultCommand(_model.RequestPrimaryStats);
            _primaryStatRequestCommand.PropertyChanged += PrimaryStatRequestCommandOnPropertyChanged;
        }

        private void MakePrimaryStatRequest()
        {
            _uiStateController.IncUiLock();

            _uiThreadInvoker.Dispatch(() => DataAvailable = false);
            _primaryStatRequestCommand.Execute(null);
        }

        private void PrimaryStatRequestCommandOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSuccessfullyCompleted")
            {
                _uiThreadInvoker.Dispatch(RebindPrimaryStatsToResult);
            }
        }

        private void RebindPrimaryStatsToResult()
        {
            _logger.LogEntry();

            _bindingHelper.Rebind(PrimaryStats, _primaryStatRequestCommand.Execution.Result);

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
            _primaryStatRequestCommand.PropertyChanged -= PrimaryStatRequestCommandOnPropertyChanged;
            _primaryStatRequestCommand.Dispose();
            _model.PrimaryStatsUpdated -= ModelOnPrimaryStatsUpdated;
        }
    }
}
