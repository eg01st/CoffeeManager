using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.Managers;
using CoffeeManager.Core.Messages;
using CoffeManager.Common;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManager.Core.ViewModels
{
    public class CurrentShiftSalesViewModel : ViewModelBase
    {
        private MvxSubscriptionToken token;
        private ShiftManager _manager = new ShiftManager();
        protected List<SaleViewModel> _items;

        public List<SaleViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        public async void Init()
        {
            token = Subscribe<SaleRemovedMessage>((async message => { await LoadSales(); }));
            await LoadSales();
        }

        private async Task LoadSales()
        {
            try
            {
                var items = await _manager.GetCurrentShiftSales();
                Items = items.Select(s => new SaleViewModel(s)).ToList();
            }
            catch (ArgumentNullException ex)
            {
                Close(this);
            }
        }
    }
}
