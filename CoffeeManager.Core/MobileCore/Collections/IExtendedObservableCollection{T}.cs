using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace MobileCore.Collections
{
    public interface IExtendedObservableCollection<T> : ICollection<T>, INotifyCollectionChanged, INotifyPropertyChanged, IExtendedObservableCollection
    {
        void ReplaceWith(IEnumerable<T> items);

        void AddRange(IEnumerable<T> items);

        void ReplaceRange(IEnumerable<T> items, int firstIndex, int oldSize);

        void SwitchTo(IEnumerable<T> items);

        void RemoveItems(IEnumerable<T> items);

        void RemoveRange(int start, int count);
    }
}
