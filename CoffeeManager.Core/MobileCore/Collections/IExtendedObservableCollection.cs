using System;

namespace MobileCore.Collections
{
    public interface IExtendedObservableCollection
    {
        event EventHandler<CollectionChangedEventArgs> OnExtendedCollectionChanged;

        object ElementAt(int index);
        T ElementAt<T>(int index) where T : class;
        int Count { get; }
    }
}
