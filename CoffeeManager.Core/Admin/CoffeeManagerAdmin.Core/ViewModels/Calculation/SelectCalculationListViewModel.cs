using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class SelectCalculationListViewModel : BaseSearchViewModel<SelectCalculationItemViewModel>
    {
        private int productId;

        readonly ISuplyProductsManager manager;

        public SelectCalculationListViewModel(ISuplyProductsManager manager)
        {
            this.manager = manager;
        }
        public void Init(int productId)
        {
            this.productId = productId;
        }

        public async override Task<List<SelectCalculationItemViewModel>> LoadData()
        {
            var items = await manager.GetSuplyProducts();
            return items.Select(s => new SelectCalculationItemViewModel(manager, productId, s)).ToList();
        }
    }
}
