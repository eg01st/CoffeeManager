using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MobileCore.Collections;
using MobileCore.ViewModels;

namespace CoffeManager.Common.ViewModels
{
    public abstract class BaseSearchViewModel<TItem> : PageViewModel where TItem : ListItemViewModelBase
    {
        private ExtendedObservableCollection<TItem> orginalItems;
        private ExtendedObservableCollection<TItem> items;

        private string searchString;

        public override async Task Initialize()
        {
            var loadedItems = new ExtendedObservableCollection<TItem>(await ExecuteSafe(LoadData));
            orginalItems = Items = loadedItems;
        }

        public abstract Task<List<TItem>> LoadData();

        public ExtendedObservableCollection<TItem> Items
        {
            get { return items; }
            set
            {
                items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        public string SearchString
        {
            get { return searchString; }
            set
            {
                searchString = value;
                RaisePropertyChanged(nameof(SearchString));
                if (!string.IsNullOrWhiteSpace(SearchString) && Items != null)
                {
                    Items.ReplaceWith(Items.Where(i => i.Name != null && i.Name.StartsWith(SearchString, StringComparison.OrdinalIgnoreCase)));
                }
                else
                {
                    Items = orginalItems;
                }
            }
        }
    }
}
