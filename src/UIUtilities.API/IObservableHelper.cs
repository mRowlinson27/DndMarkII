
namespace UIUtilities.API
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public interface IObservableHelper
    {
        void Rebind<T>(ObservableCollection<T> collection, IEnumerable<T> newCollection);

        void SafeClear<T>(ObservableCollection<T> collection);

        void SafeAddRange<T>(ObservableCollection<T> collection, IEnumerable<T> range);
    }
}
