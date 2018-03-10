using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;

namespace CoffeeManager.Core.ViewModels.UtilizedProducts
{
    public class UtilizeProductsViewModel : BaseSearchViewModel<UtilizeItemViewModel>
    {
        public async override Task<List<UtilizeItemViewModel>> LoadData()
        {
            var items = await manager.GetSuplyProducts();
            return items.Select(s => new UtilizeItemViewModel(s)).ToList();
        }

        readonly ISuplyProductsManager manager;

        public UtilizeProductsViewModel(ISuplyProductsManager manager)
        {
            this.manager = manager;
        }
    }
}
