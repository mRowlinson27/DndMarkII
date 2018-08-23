
namespace UIUtilities
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using API;

    public class ObservableHelper : IObservableHelper
    {
        public void Rebind<T>(ObservableCollection<T> collection, IEnumerable<T> newCollection)
        {
            SafeClear(collection);
            SafeAddRange(collection, newCollection);
        }

        public void SafeClear<T>(ObservableCollection<T> collection)
        {
            for (int i = collection.Count - 1; i >= 0; i--)
            {
                collection.RemoveAt(i);
            }
        }

        public void SafeAddRange<T>(ObservableCollection<T> collection, IEnumerable<T> range)
        {
            foreach (var val in range)
            {
                collection.Add(val);
            }
        }
    }
}
