using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class ProductsCalculationViewModel : BaseSearchViewModel<ItemViewModel>
    {
        private readonly IProductManager manager;

        public ProductsCalculationViewModel(IProductManager manager)
        {
            this.manager = manager;
        }

        public async override Task<List<ItemViewModel>> LoadData()
        {
            var items = await manager.GetProducts();
            return items.Select(s => new ItemViewModel(s)).ToList();
        }
    }
}
