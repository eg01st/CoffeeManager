using System;
using CoffeManager.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace CoffeeManagerAdmin.Core
{
    public class UtilizeViewModel : ViewModelBase
    {
        public List<UtilizeItemViewModel> Items { get; set; }

        readonly ISuplyProductsManager manager;

        public UtilizeViewModel(ISuplyProductsManager manager)
        {
            this.manager = manager;
        }

        public async Task Init()
        {
            var items = await manager.GetUtilizedProducts();
            Items = items.Select(s => new UtilizeItemViewModel(s)).ToList();
            RaisePropertyChanged(nameof(Items));
        }
    }
}
