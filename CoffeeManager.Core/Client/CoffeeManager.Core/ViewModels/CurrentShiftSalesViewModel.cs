using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Core.Messages;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MobileCore.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManager.Core.ViewModels
{
    public class CurrentShiftSalesViewModel : PageViewModel
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
            token = MvxMessenger.Subscribe<SaleRemovedMessage>((async message => { await Initialize(); }));
        }

        protected override async Task DoLoadDataImplAsync()
        {
            await ExecuteSafe(async () =>
            {
                var items = await shiftManager.GetCurrentShiftSales();
                Items = items.Select(s => new SaleItemViewModel(productManager, s)).ToList();
            });
        }

        protected override void DoUnsubscribe()
        {
            MvxMessenger.Unsubscribe<SaleRemovedMessage>(token);
        }
    }
}
