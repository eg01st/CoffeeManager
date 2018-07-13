using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Abstract
{
    public abstract class BaseAdminSearchViewModel<TItem> : AdminCoffeeRoomFeedViewModel<TItem> where TItem : ListItemViewModelBase
    {
        private List<TItem> orginalItems;

        private string searchString;

        public override async Task Initialize()
        {
            await base.Initialize();

            var loadedItems = await ExecuteSafe(LoadData);
            orginalItems = loadedItems;
            ItemsCollection.AddRange(loadedItems);
        }

        public abstract Task<List<TItem>> LoadData();

        public string SearchString
        {
            get { return searchString; }
            set
            {
                searchString = value;
                RaisePropertyChanged(nameof(SearchString));
                if (!string.IsNullOrWhiteSpace(SearchString) && ItemsCollection.Any())
                {
                    var searchItems = ItemsCollection.Where(i =>
                        i.Name != null && i.Name.StartsWith(SearchString, StringComparison.OrdinalIgnoreCase));
                    ItemsCollection.ReplaceWith(searchItems);
                }
                else
                {
                    ItemsCollection.ReplaceWith(orginalItems);
                }
            }
        }
    }
}