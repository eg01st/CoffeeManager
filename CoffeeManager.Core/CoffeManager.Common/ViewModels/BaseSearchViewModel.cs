using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MobileCore.ViewModels;

namespace CoffeManager.Common.ViewModels
{
    public abstract class BaseSearchViewModel<TItem> : PageViewModel where TItem : ListItemViewModelBase
    {
        private List<TItem> _orginalItems;
        private List<TItem> _items;

        private string _searchString;

        public async override Task Initialize()
        {
            var loadedItems = await ExecuteSafe(LoadData);
            _orginalItems = Items = loadedItems;
        }

        public abstract Task<List<TItem>> LoadData();

        public List<TItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
                RaisePropertyChanged(nameof(SearchString));
                if (!string.IsNullOrWhiteSpace(SearchString) && Items != null)
                {
                    Items = Items.Where(i => i.Name != null && i.Name.StartsWith(SearchString, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                else
                {
                    Items = _orginalItems;
                }
            }
        }
    }
}
