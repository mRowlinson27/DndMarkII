
namespace UIUtilities
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using API;
    using Utilities.API;

    public class ObservableDataBinder<T>
    {
        private readonly ILogger _logger;

        private readonly IObservableHelper _observableHelper;

        private readonly ObservableCollection<T> _boundValue;

        private IAsyncTaskRunner<IEnumerable<T>> _requestTaskRunner;

        public ObservableDataBinder(ILogger logger, IObservableHelper observableHelper, ObservableCollection<T> boundValue, IAsyncTaskRunner<IEnumerable<T>> taskRunner)
        {
            _logger = logger;
            _observableHelper = observableHelper;
            _boundValue = boundValue;
            _requestTaskRunner = taskRunner;

            _requestTaskRunner.PropertyChanged += RequestTaskRunnerOnPropertyChanged;
        }

        public void Update()
        {
//            DataAvailable = false;
            _requestTaskRunner.StartTask();
        }

        private void RequestTaskRunnerOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsSuccessfullyCompleted")
            {
                return;
            }

            _observableHelper.Rebind(_boundValue, _requestTaskRunner.Result);

//            DataAvailable = true;
            _logger.LogExit();
        }

        public void Dispose()
        {
            _requestTaskRunner.PropertyChanged -= RequestTaskRunnerOnPropertyChanged;
            _requestTaskRunner.Dispose();
        }
    }
}
