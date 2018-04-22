using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Inventory.Create
{
    public class CreateInventoryViewModel : BaseSearchViewModel<CreateInventoryItemViewModel>
    {
      //  private readonly MvxSubscriptionToken token;

        readonly IInventoryManager manager;

        public ICommand SendReportCommand { get; }

        public CreateInventoryViewModel(IInventoryManager manager)
        {
            this.manager = manager;
            SendReportCommand = new MvxCommand(DoSendReport);

            //token = Subscribe<InventoryItemChangedMessage>(OnItemChanged);
        }

        //private void OnItemChanged(InventoryItemChangedMessage inventoryItemChangedMessage)
        //{
        //    CreateInventoryItemViewModel vm = (CreateInventoryItemViewModel)inventoryItemChangedMessage.Sender;
        //    var item = Map(vm);
        //    manager.SaveReportItem(item);
        //}

        private async void DoSendReport()
        {
            if (Items.Any(i => !i.IsProceeded))
            {
                Alert("Не все товары прошли переучет");
                return;
            }
            await ExecuteSafe(async () =>
            {
                var items = Items.Select(MapForServerSend);
                await manager.SentInventoryInfo(items);
               // manager.RemoveSavedItems();
                CloseCommand.Execute(null);
            });
        }

        public override async Task<List<CreateInventoryItemViewModel>> LoadData()
        {
            var items = await manager.GetInventoryItems();
            //var savedItems = manager.GetSavedItems();

            var vms = items.Select(s => new CreateInventoryItemViewModel(s)).ToList();

            //foreach (var savedItem in savedItems)
            //{
            //    var vm = vms.FirstOrDefault(v => v.SuplyProductId == savedItem.SuplyProductId);
            //    if (vm != null)
            //    {
            //        vm.QuantityAfter = savedItem.QuantityAfer;
            //        vm.IsProceeded = true;
            //    }
            //}
            //vms.Insert(0, new CreateInventoryItemViewModel(new SupliedProduct()));
            return vms;
        }

        private InventoryItem Map(CreateInventoryItemViewModel vm)
        {
            return new InventoryItem()
            {
                SuplyProductId = vm.SuplyProductId,
                QuantityBefore = vm.QuantityBefore,
                QuantityAfer = vm.QuantityAfter ?? 0,
                CoffeeRoomNo = vm.CoffeeRoomNo,
                SuplyProductName = vm.Name,
            };
        }

        private InventoryItem MapForServerSend(CreateInventoryItemViewModel vm)
        {
            var item = new InventoryItem()
            {
                SuplyProductId = vm.SuplyProductId,
                QuantityBefore = vm.QuantityBefore,
                QuantityAfer = vm.QuantityAfter ?? 0,
                CoffeeRoomNo = vm.CoffeeRoomNo,
                SuplyProductName = vm.Name,
            };
            if (vm.InventoryNumerationMultyplier.HasValue)
            {
                item.QuantityAfer = vm.QuantityAfter * vm.InventoryNumerationMultyplier ?? 0;
            }
            return item;
        }

        //protected override void DoUnsubscribe()
        //{
        //    base.DoUnsubscribe();
        //    //Unsubscribe<InventoryItemChangedMessage>(token);
        //}
    }
}
