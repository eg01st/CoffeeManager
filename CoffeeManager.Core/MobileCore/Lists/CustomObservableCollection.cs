using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using MvvmCross.Core.ViewModels;

namespace MobileCore.Lists
{
public class CustomObservableCollection<T> : MvxObservableCollection<T>
    {
        public CustomObservableCollection()
        { }

        public CustomObservableCollection(IEnumerable<T> items)
            : base(items)
        { }

        public override void AddRange(IEnumerable<T> items)
        {
            var startIndex = Count;

            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var itemsList = items.ToList();
            using (SuppressEvents())
            {
                foreach (var item in itemsList)
                {
                    Add(item);
                }
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, itemsList as IList, startIndex));
        }

        public override void RemoveRange(int start, int count)
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
            for (int i = start; i <= end; i++)
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

        public override void RemoveItems(IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var startIndex = items.Min(x => Items.IndexOf(x));
            using (SuppressEvents())
            {
                foreach (var item in items)
                {
                    Remove(item);
                }
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, items.ToList(), startIndex));
        }

        public void InsertRange(IEnumerable<T> items, int index)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var itemsList = items.ToList();
            using (SuppressEvents())
            {
                var startIndex = index;
                foreach (var item in itemsList)
                {
                    Insert(startIndex++, item);
                }
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, itemsList, index));
        }
    }
}