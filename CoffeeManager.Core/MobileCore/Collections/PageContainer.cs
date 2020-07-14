using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using MobileCore.Extensions;

namespace MobileCore.Collections
{
    public class PageContainer<T>
    {
        public PageContainer()
            : this(new T[0], 0)
        {
        }

        public PageContainer(IEnumerable<T> items)
            : this(items?.ToImmutableList())
        {
        }
        
        public PageContainer(IEnumerable<T> items, int totalCount)
            : this(items?.ToImmutableList(), totalCount)
        {
        }
        
        public PageContainer(IImmutableList<T> items)
            : this(items ?? ImmutableList.Create<T>(), items?.Count ?? 0)
        {
        }

        public PageContainer(IImmutableList<T> items, int totalCount)
        {
            Items = items ?? ImmutableList.Create<T>();
            TotalCount = totalCount;
        }

        public IImmutableList<T> Items { get; set; }

        public int ItemsCount => Items?.Count ?? 0;

        public int TotalCount { get; set; }

        public bool IsEmpty => Items.IsNullOrEmpty();

        public PageContainer<V> Transform<V>(Func<T, V> transformFunction)
        {
            var items = Items.Select(transformFunction);
            return new PageContainer<V>(items, TotalCount);
        }
	    
        public PageContainer<V> Transform<V>(Func<T, int, V> transformFunction)
        {
            var items = Items.Select(transformFunction);
            return new PageContainer<V>(items, TotalCount);
        }
    }
}