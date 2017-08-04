using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Core.Messages;
using CoffeManager.Common;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManager.Core.ViewModels
{
    public class CurrentShiftSalesViewModel : ViewModelBase
    {
        private readonly IShiftManager shiftManager;
        private readonly IProductManager productManager;

        private MvxSubscriptionToken token;
        protected List<SaleItemViewModel> _items;

        public List<SaleItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }



        public CurrentShiftSalesViewModel(IShiftManager shiftManager, IProductManager productManager)
        {
            this.productManager = productManager;
            this.shiftManager = shiftManager;
            token = Subscribe<SaleRemovedMessage>((async message => { await LoadSales(); }));
        }

        public async void Init()
        {
            await ExecuteSafe(LoadSales);
        }

        private async Task LoadSales()
        {
            try
            {
                var items = await shiftManager.GetCurrentShiftSales();
                Items = items.Select(s => new SaleItemViewModel(productManager, s)).ToList();
            }
            catch (ArgumentNullException ex)
            {
                Close(this);
            }
        }
    }
}
