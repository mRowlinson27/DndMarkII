
namespace UIView.Factories
{
    using API;
    using UIModel.API;
    using UIModel.API.Dto;
    using UIUtilities.API;
    using UIUtilities.API.AsyncCommands;
    using Utilities.API;
    using ViewModel;

    public class PrimaryStatViewModelFactory : IPrimaryStatViewModelFactory
    {
        private readonly ILogger _logger;

        private readonly IAsyncCommandFactory _asyncCommandFactory;

        private readonly IUiThreadInvoker _uiThreadInvoker;

        private readonly IPrimaryStatModelFactory _primaryStatModelFactory;

        public PrimaryStatViewModelFactory(ILogger logger, IAsyncCommandFactory asyncCommandFactory,
            IUiThreadInvoker uiThreadInvoker, IPrimaryStatModelFactory primaryStatModelFactory)
        {
            _logger = logger;
            _asyncCommandFactory = asyncCommandFactory;
            _uiThreadInvoker = uiThreadInvoker;
            _primaryStatModelFactory = primaryStatModelFactory;
        }

        public IPrimaryStatViewModel Create(UiPrimaryStat primaryStat)
        {
            var primaryStatViewModel = new PrimaryStatViewModel(_logger, _primaryStatModelFactory.Create(), _asyncCommandFactory, _uiThreadInvoker) { PrimaryStat = primaryStat };
            primaryStatViewModel.Init();
            return primaryStatViewModel;
        }
    }
}
