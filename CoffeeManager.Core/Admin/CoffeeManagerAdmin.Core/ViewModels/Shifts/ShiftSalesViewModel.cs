using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Shifts
{
    public class ShiftSalesViewModel : ViewModelBase, IMvxViewModel<int>
    {
        private readonly IShiftManager manager;

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


        public ShiftSalesViewModel(IShiftManager manager)
        {
            this.manager = manager;
        }

        public override async Task Initialize()
        {
            await ExecuteSafe(async () =>
            {
                var saleItems = await manager.GetShiftSales(_shiftId);
                SaleItems = saleItems.Select(s => new SaleItemViewModel(s)).ToList();
                GroupedSaleItems = SaleItems.GroupBy(g => g.Name).Select(s => new Entity() { Name = s.Key, Id = s.Count() }).OrderByDescending(o => o.Id).ToList();
            });
        }

        public void Prepare(int parameter)
        {
            _shiftId = parameter;
        }
    }
}
