
namespace UIUtilities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using API;

    public class ObservableHelper : IObservableHelper
    {
        public void Refresh<T>(IList<T> collection)
        {
            throw new NotImplementedException();
        }

        public void RefreshElement<T>(IList<T> collection, T element)
        {
            var i = collection.IndexOf(element);

            collection.RemoveAt(i);
            collection.Insert(i, element);
        }

        public void Rebind<T>(IList<T> collection, IEnumerable<T> newCollection)
        {
            SafeClear(collection);
            SafeAddRange(collection, newCollection);
        }

        public void SmartRebind<T1, T2>(Dictionary<T1, IRebindable<T2>> currentViewModelsDict, Dictionary<T1, T2> dtoDict, IRebindableFactory<IRebindable<T2>, T2> rebindableFactory)
        {
            throw new NotImplementedException();
        }

        public void SafeClear<T>(IList<T> collection)
        {
            for (int i = collection.Count - 1; i >= 0; i--)
            {
                collection.RemoveAt(i);
            }
        }

        public void SafeAddRange<T>(IList<T> collection, IEnumerable<T> range)
        {
            foreach (var val in range)
            {
                collection.Add(val);
            }
        }
    }
}
