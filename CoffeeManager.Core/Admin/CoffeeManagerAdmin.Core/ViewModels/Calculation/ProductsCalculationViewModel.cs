using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class ProductsCalculationViewModel : BaseSearchViewModel<ItemViewModel>
    {
        private ProductManager manager = new ProductManager();

        public async override Task<List<ItemViewModel>> LoadData()
        {
            var items = await manager.GetProducts();
            return items.Select(s => new ItemViewModel(s)).ToList();
        }
    }
}
