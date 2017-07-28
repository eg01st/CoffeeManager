using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Managers;

namespace CoffeeManagerAdmin.Core
{
    public class ShiftSalesViewModel : ViewModelBase
    {
        private ShiftManager shiftManager = new ShiftManager();
        private List<SaleItemViewModel> _saleItems = new List<SaleItemViewModel>();

        private List<Entity> _groupedSaleItems = new List<Entity>();
        private int _shiftId;

        public List<SaleItemViewModel> SaleItems
        {
            get { return _saleItems; }
            set
            {
                _saleItems = value;
                RaisePropertyChanged(nameof(SaleItems));
            }
        }

        public List<Entity> GroupedSaleItems
        {
            get { return _groupedSaleItems; }
            set
            {
                _groupedSaleItems = value;
                RaisePropertyChanged(nameof(GroupedSaleItems));
            }
        }

        public ShiftSalesViewModel()
        {
        }

        public async void Init(int id)
        {
            _shiftId = id;

            var saleItems = await shiftManager.GetShiftSales(_shiftId);
            SaleItems = saleItems.Select(s => new SaleItemViewModel(s)).ToList();
            GroupedSaleItems = SaleItems.GroupBy(g => g.Name).Select(s => new Entity() { Name = s.Key, Id = s.Count() }).OrderByDescending(o => o.Id).ToList();
        }
    }
}
