using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeManager.Common;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeeManager.Models;
namespace CoffeeManagerAdmin.Core
{
    public class SelectSuplyProductsViewModel : BaseSearchViewModel<ListItemViewModelBase>
    {
        readonly ISuplyProductsManager manager;
        int fromCoffeeRoom, toCoffeeRoom;
        public SelectSuplyProductsViewModel(ISuplyProductsManager manager)
        {
            this.manager = manager;
            TransferSuplyProductsCommand = new MvxCommand(DoTransferSuplyProducts, () => Items.OfType<SelectSuplyProductItemViewModel>().Any(a => a.IsSelected));
        }

        public void Init(int fromCoffeeRoom, int toCoffeeRoom)
        {
            this.fromCoffeeRoom = fromCoffeeRoom;
            this.toCoffeeRoom = toCoffeeRoom;
        }

        private void DoTransferSuplyProducts()
        {
            string confirmMessage = "Вы переводите продукты:\n";
            var items = Items.OfType<SelectSuplyProductItemViewModel>().Where(i => i.IsSelected);
            foreach (var item in items)
            {
                confirmMessage += $"{item.Name} в количестве {item.QuantityToTransfer}\n";
            }
            confirmMessage += "Продолжить?";
            Confirm(confirmMessage, TransferProducts, items);
        }

        private async Task TransferProducts(IEnumerable<SelectSuplyProductItemViewModel> items)
        {
            var requests = items.Select(s => new TransferSuplyProductRequest() 
            {
                SuplyProductId = s.SuplyProductId,
                Quantity = s.QuantityToTransfer.Value,
                CoffeeRoomIdFrom = fromCoffeeRoom,
                CoffeeRoomIdTo = toCoffeeRoom
            });
            await manager.TransferSuplyProducts(requests);
            CloseCommand.Execute(null);
        }


        public ICommand TransferSuplyProductsCommand { get;}

        public async override Task<List<ListItemViewModelBase>> LoadData()
        {
            var items = await manager.GetSuplyProducts();
            var result = new List<ListItemViewModelBase>();


            var vms = items.Select(s => new SelectSuplyProductItemViewModel(s)).GroupBy(g => g.ExpenseTypeName).OrderByDescending(o => o.Key);
            foreach (var item in vms)
            {
                result.Add(new ExpenseTypeHeaderViewModel(item.Key));
                result.AddRange(item);
            }
            return result;
        }
    }
}
