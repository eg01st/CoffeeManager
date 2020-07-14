using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MobileCore.Extensions;

namespace MobileCore
{
    public static class EnumerableExtensions
    {
        public static async Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> action)
        {
            if (source.IsNullOrEmpty() == true)
            {
                return;
            }

            action.ThrowIfNull(nameof(action));

            var tasks = new List<Task>(source.Count());
            foreach (var element in source)
            {
                tasks.Add(action(element));
            }

            await Task.WhenAll(tasks);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source.IsNullOrEmpty() == true)
            {
                return;
            }

            action.ThrowIfNull(nameof(action));

            foreach (var element in source)
            {
                action(element);
            }
        }
    }
}
