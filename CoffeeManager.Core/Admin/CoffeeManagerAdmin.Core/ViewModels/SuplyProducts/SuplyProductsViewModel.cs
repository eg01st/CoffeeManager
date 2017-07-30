using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class SuplyProductsViewModel : BaseSearchViewModel<SuplyProductItemViewModel>
    {
        private MvxSubscriptionToken _listChanged;

        private SuplyProductsManager manager = new SuplyProductsManager();

        public SuplyProductsViewModel()
        {
            _listChanged = Subscribe<SuplyListChangedMessage>((obj) => Init());
        }

        public async override Task<List<SuplyProductItemViewModel>> LoadData()
        {
            var items = await manager.GetSupliedProducts();
            return items.Select(s => new SuplyProductItemViewModel(s)).ToList();
        }
    }
}
