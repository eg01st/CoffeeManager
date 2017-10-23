using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeManager.Common;
namespace CoffeeManagerAdmin.Core
{
    public class TransferSuplyProductsViewModel : BaseSearchViewModel<ListItemViewModelBase>
    {
        readonly ISuplyProductsManager manager;

        public TransferSuplyProductsViewModel(ISuplyProductsManager manager)
        {
            this.manager = manager;
        }

        public async override Task<List<ListItemViewModelBase>> LoadData()
        {
            var items = await manager.GetSuplyProducts();
            var result = new List<ListItemViewModelBase>();


            var vms = items.Select(s => new TransferSuplyProductItemViewModel(s)).GroupBy(g => g.ExpenseTypeName).OrderByDescending(o => o.Key);
            foreach (var item in vms)
            {
                result.Add(new ExpenseTypeHeaderViewModel(item.Key));
                result.AddRange(item);
            }
            return result;
        }
    }
}
