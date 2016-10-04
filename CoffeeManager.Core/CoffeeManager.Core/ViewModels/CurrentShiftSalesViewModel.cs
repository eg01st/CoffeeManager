using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.Managers;

namespace CoffeeManager.Core.ViewModels
{
    public class CurrentShiftSalesViewModel : ViewModelBase
    {
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
            var items = await _manager.GetCurrentShiftSales();
            Items = items.Select(s => new SaleViewModel(s)).ToList();
        }
    }
}
