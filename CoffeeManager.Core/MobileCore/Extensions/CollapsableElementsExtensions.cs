using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MobileCore.ViewModels;
using MobileCore.ViewModels.Items;

namespace MobileCore.Extensions
{
 public static class CollapsableElementsExtensions
    {
        private static readonly IDictionary<WeakReference, IEnumerable> cache =
            new Dictionary<WeakReference, IEnumerable>();

        public static void ToggleCollapse<T>(this FeedViewModel<T> viewModel, SectionHeaderItemViewModel headerViewModel)
            where T : FeedItemElementViewModel
        {
            var header = headerViewModel as T;
            if (viewModel == null
                || header == null
                || headerViewModel.IsExpandable == false)
            {
                return;
            }

            var key = cache.Keys.FirstOrDefault(r => GetKeyByWeakReference(r, headerViewModel));
            if (key == null)
            {
                headerViewModel.IsExpanded = false;
                var hiddenItems = HideItems(viewModel, header);
                if (hiddenItems.IsNullOrDefault() == false)
                {
                    cache.Add(new WeakReference(headerViewModel), hiddenItems);
                }
            }
            else
            {
                headerViewModel.IsExpanded = true;
                var flag = ShowItems(viewModel, header, cache[key]);
                if (flag == true)
                {
                    cache.Remove(key);
                }
            }

            ClearCacheForDeadLinks();
        }

        private static bool GetKeyByWeakReference(WeakReference r, SectionHeaderItemViewModel vm) 
            => r.IsAlive == false ? false : object.Equals(r.Target, vm);

        private static IEnumerable HideItems<T>(FeedViewModel<T> viewModel, T header)
            where T : FeedItemElementViewModel
        {
            var observable = viewModel.ItemsCollection;
            var startHeaderIndex = observable.IndexOf(header);
            if (startHeaderIndex < 0)
            {
                return null;
            }

            var skipIndex = startHeaderIndex + 1;
            var itemsLeft = observable.Skip(skipIndex).ToList();
            if (itemsLeft.Any() == false)
            {
                return null;
            }

            var nextHeader = itemsLeft.FirstOrDefault(i => i is SectionHeaderItemViewModel);

            int countOfItemsToStore;
            if (nextHeader == null)
            {
                countOfItemsToStore = itemsLeft.Count;
            }
            else
            {
                countOfItemsToStore = itemsLeft.IndexOf(nextHeader);
                if (countOfItemsToStore < 1)
                {
                    return null;
                }
            }

            var removedItems = observable.Skip(skipIndex).Take(countOfItemsToStore).ToList();

            observable.RemoveRange(skipIndex, countOfItemsToStore);

            return removedItems;
        }

        private static bool ShowItems<T>(FeedViewModel<T> viewModel, T header, IEnumerable items)
            where T : FeedItemElementViewModel
        {
            var observable = viewModel.ItemsCollection;
            var startHeaderIndex = observable.IndexOf(header);
            if (startHeaderIndex < 0)
            {
                return false;
            }

            var itemsToAdd = items.Cast<T>();

            var insertIndex = startHeaderIndex + 1;
            if (insertIndex >= observable.Count)
            {
                observable.AddRange(itemsToAdd);
            }
            else
            {
                observable.InsertRange(itemsToAdd, insertIndex);
            }

            return true;
        }

        private static void ClearCacheForDeadLinks()
        {
            var deadLinks = cache.Keys.Where(k => k.IsAlive == false).ToList();
            foreach (var k in deadLinks)
            {
                cache.Remove(k);
            }
        }
    }
}