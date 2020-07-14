using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace MobileCore.Collections
{
    public class ExtendedObservableCollection<T> : ObservableCollection<T>, IExtendedObservableCollection<T>
        where T : class
    {
        protected struct SuppressEventsDisposable : IDisposable
        {
            private readonly ExtendedObservableCollection<T> collection;

            public SuppressEventsDisposable(ExtendedObservableCollection<T> collection)
            {
                this.collection = collection;
                ++collection.suppressEventsCounter;
            }

            public void Dispose()
            {
                --collection.suppressEventsCounter;
            }
        }

        private int suppressEventsCounter;
        private int cachedTotalCount;

        public event EventHandler<CollectionChangedEventArgs> OnExtendedCollectionChanged;

        public ExtendedObservableCollection() : this(new T[0])
        {
        }

        public ExtendedObservableCollection(IEnumerable<T> source) : base(source)
        {
            CollectionChanged += OnCollectionChanged;
        }

        protected SuppressEventsDisposable SuppressEvents()
        {
            return new SuppressEventsDisposable(this);
        }

        public bool EventsAreSuppressed => suppressEventsCounter > 0;

        public bool IsEmpty => Count == 0;

        public object ElementAt(int index)
        {
            var element = ElementAt<object>(index);
            return element;
        }

        public TItem ElementAt<TItem>(int index)
            where TItem : class
        {
            var element = this[index];
            var typedElement = element as TItem;

            return typedElement;
        }

        public void ReplaceWith(IEnumerable<T> items)
        {
            using (SuppressEvents())
            {
                Clear();
                AddRange(items);
            }
        }

        public void AddRange(IEnumerable<T> items)
        {
            var itemsArray = items.ToArray();

            var index = Count;
            var count = itemsArray.Length;

            using (SuppressEvents())
            {
                foreach (var item in itemsArray)
                {
                    Add(item);
                }

                var reset = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
                base.OnCollectionChanged(reset);
            }

            var args = new CollectionChangedEventArgs(CollectionChangedAction.AddRange, index, count);
            OnExtendedCollectionChanged?.Invoke(this, args);
        }

        public void ReplaceRange(IEnumerable<T> items, int firstIndex, int oldSize)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var itemsArray = items.ToArray();

            using (SuppressEvents())
            {
                var lastIndex = firstIndex + oldSize - 1;

                // If there are more items in the previous list, remove them.
                while (firstIndex + itemsArray.Length <= lastIndex)
                {
                    RemoveAt(lastIndex--);
                }

                foreach (var item in itemsArray)
                {
                    if (firstIndex <= lastIndex)
                    {
                        SetItem(firstIndex++, item);
                    }
                    else
                    {
                        Insert(firstIndex++, item);
                    }
                }
            }

            var reset = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(reset);
        }

        public void SwitchTo(IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var itemIndex = 0;
            var count = Count;

            foreach (var item in items)
            {
                if (itemIndex >= count)
                {
                    Add(item);
                }
                else if (!Equals(this[itemIndex], item))
                {
                    this[itemIndex] = item;
                }

                itemIndex++;
            }

            while (count > itemIndex)
            {
                RemoveAt(--count);
            }
        }

        public void RemoveItems(IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            using (SuppressEvents())
            {
                foreach (var item in items)
                {
                    Remove(item);
                }
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void RemoveRange(int start, int count)
        {
            if (start < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            var end = start + count - 1;

            if (end > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            var removedItems = new List<T>(count);
            for (var i = start; i < count; i++)
            {
                removedItems.Add(this[i]);
            }

            using (SuppressEvents())
            {
                for (var i = end; i >= start; i--)
                {
                    RemoveAt(i);
                }
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItems, start));
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (EventsAreSuppressed)
            {
                return;
            }

            base.OnCollectionChanged(e);
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (EventsAreSuppressed)
            {
                return;
            }

            var oldStartingIndex = e.OldStartingIndex;
            var newStartingIndex = e.NewStartingIndex;

            var action = CollectionChangedAction.Reset;
            var index = 0;
            var count = 0;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    index = newStartingIndex;
                    action = CollectionChangedAction.Add;
                    break;
                case NotifyCollectionChangedAction.Move:
                    // TODO not supported
                    break;
                case NotifyCollectionChangedAction.Remove:
                    index = oldStartingIndex;
                    action = CollectionChangedAction.Remove;
                    break;
                case NotifyCollectionChangedAction.Replace:
                    // TODO not supported
                    break;
                case NotifyCollectionChangedAction.Reset:
                    action = CollectionChangedAction.Reset;
                    count = cachedTotalCount;
                    break;
            }

            cachedTotalCount = Count;

            var args = new CollectionChangedEventArgs(action, index, count);
            OnExtendedCollectionChanged?.Invoke(this, args);
        }
    }
}
