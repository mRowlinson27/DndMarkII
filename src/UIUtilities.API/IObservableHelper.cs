
namespace UIUtilities.API
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public interface IObservableHelper
    {
        void Refresh<T>(IList<T> collection);

        void RefreshElement<T>(IList<T> collection, T element);

        void Rebind<T>(IList<T> collection, IEnumerable<T> newCollection);

        void SafeClear<T>(IList<T> collection);

        void SafeAddRange<T>(IList<T> collection, IEnumerable<T> range);
    }
}
