using System.Collections.Generic;

namespace MobileCore.Collections
{
    public static class PageContainerExtensions
    {
        public static PageContainer<T> ToPageContainer<T>(this IEnumerable<T> items)
        {
            var page = new PageContainer<T>(items);
            return page;
        }
        
        public static PageContainer<T> ToPageContainer<T>(this IEnumerable<T> items, int totalCount)
        {
            var page = new PageContainer<T>(items, totalCount);
            return page;
        }
    }
}