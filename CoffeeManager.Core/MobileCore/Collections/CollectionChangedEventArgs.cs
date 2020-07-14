using System;

namespace MobileCore.Collections
{
    public class CollectionChangedEventArgs : EventArgs
    {
        public CollectionChangedEventArgs(CollectionChangedAction collectionChangedAction, int index, int count)
        {
            CollectionChangedAction = collectionChangedAction;
            Index = index;
            Count = count;
        }

        public CollectionChangedAction CollectionChangedAction { get; }
        public int Index { get; }
        public int Count { get; }
    }
}
