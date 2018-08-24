
namespace UIUtilities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using API;

    public class ObservableDataBinderFactory
    {
        private readonly IAsyncTaskRunnerFactory _asyncTaskRunnerFactory;

        public ObservableDataBinderFactory(IAsyncTaskRunnerFactory asyncTaskRunnerFactory)
        {
            _asyncTaskRunnerFactory = asyncTaskRunnerFactory;
        }

        public ObservableDataBinder<T> Create<T>(ObservableCollection<T> boundValue, Func<Task<IEnumerable<T>>> taskFuncWithReturnValue)
        {
            return new ObservableDataBinder<T>(boundValue, _asyncTaskRunnerFactory.Create(taskFuncWithReturnValue));
        }
    }
}
