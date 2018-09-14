using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ViewModels.Abstract;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.TransferSuplyProduct
{
    public class SelectSuplyProductsViewModel : BaseAdminSearchViewModel<ListItemViewModelBase>, IMvxViewModel<Tuple<int, int>>
    {
        readonly ISuplyProductsManager manager;
        int fromCoffeeRoom, toCoffeeRoom;
        public SelectSuplyProductsViewModel(ISuplyProductsManager manager)
        {
            this.manager = manager;
            TransferSuplyProductsCommand = new MvxCommand(DoTransferSuplyProducts);
        }

        private void DoTransferSuplyProducts()
        {
            string confirmMessage = "Вы переводите продукты:\n";
            var items = ItemsCollection.OfType<SelectSuplyProductItemViewModel>().Where(i => i.IsSelected);
            if(!items.Any())
            {
                Alert("Выберите продукты для перевода");
                return;
            }
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

        public override async Task<List<ListItemViewModelBase>> LoadData()
        {
            var items = await manager.GetSuplyProducts(fromCoffeeRoom);
            var result = new List<ListItemViewModelBase>();


            var vms = items.Select(s => new SelectSuplyProductItemViewModel(s)).GroupBy(g => g.ExpenseTypeName).OrderByDescending(o => o.Key);
            foreach (var item in vms)
            {
                result.Add(new ExpenseTypeHeaderViewModel(item.Key));
                result.AddRange(item);
            }
            return result;
        }

        public void Prepare(Tuple<int, int> parameter)
        {
            fromCoffeeRoom = parameter.Item1;
            toCoffeeRoom = parameter.Item2;
        }
    }
}
