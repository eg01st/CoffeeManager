using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Utilize
{
    public class UtilizeViewModel : ViewModelBase
    {
        public List<UtilizeItemViewModel> Items { get; set; }

        readonly ISuplyProductsManager manager;

        public UtilizeViewModel(ISuplyProductsManager manager)
        {
            this.manager = manager;
        }

        public override async Task Initialize()
        {
            var items = await manager.GetUtilizedProducts();
            Items = items.Select(s => new UtilizeItemViewModel(s)).OrderByDescending(o => o.Id).ToList();
            RaisePropertyChanged(nameof(Items));
        }
    }
}
