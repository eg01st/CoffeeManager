using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class SelectCalculationListViewModel : ViewModelBase
    {
        private SuplyProductsManager manager = new SuplyProductsManager();
        public async void Init(int productId)
        {
            var items = await manager.GetSupliedProducts();
            Items = items.Select(s => new SelectCalculationItemViewModel(productId, s)).ToList();
        }
        private List<SelectCalculationItemViewModel> _items;

        public List<SelectCalculationItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }
    }
}
