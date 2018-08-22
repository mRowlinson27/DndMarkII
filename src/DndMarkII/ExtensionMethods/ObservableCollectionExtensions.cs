
namespace UIView.ExtensionMethods
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public static class ObservableCollectionExtensions
    {
        public static void RebindTo<T>(this ObservableCollection<T> collection, IEnumerable<T> newCollection)
        {
            collection.SafeClear();
            collection.SafeAddRange(newCollection);
        }

        public static void SafeClear<T>(this ObservableCollection<T> collection)
        {
            for (int i = collection.Count - 1; i >= 0; i--)
            {
                collection.RemoveAt(i);
            }
        }

        public static void SafeAddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> range)
        {
            foreach (var val in range)
            {
                collection.Add(val);
            }
        }
    }
}
