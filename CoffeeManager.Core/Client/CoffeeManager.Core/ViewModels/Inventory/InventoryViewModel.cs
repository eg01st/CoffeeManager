using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeManager.Common;
using System.Linq;
using System.Windows.Input;
using CoffeeManager.Core.Messages;
using MvvmCross.Core.ViewModels;
using CoffeeManager.Models;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManager.Core
{
    public class InventoryViewModel : BaseSearchViewModel<InventoryItemViewModel>
    {
        private readonly MvxSubscriptionToken token;

        readonly IInventoryManager manager;

        public ICommand SendReportCommand { get; }

        public InventoryViewModel(IInventoryManager manager)
        {
            this.manager = manager;
            SendReportCommand = new MvxCommand(DoSendReport);

            token = Subscribe<InventoryItemChangedMessage>(OnItemChanged);
        }

        private void OnItemChanged(InventoryItemChangedMessage inventoryItemChangedMessage)
        {
            InventoryItemViewModel vm = (InventoryItemViewModel)inventoryItemChangedMessage.Sender;
            var item = Map(vm);
            manager.SaveReportItem(item);
        }

        private async void DoSendReport()
        {
            if(Items.Any(i => !i.IsProceeded))
            {
                Alert("Не все товары прошли переучет");
                return;
            }
            await ExecuteSafe(async () =>
            {
                var items = Items.Select(Map);
                await manager.SentInventoryInfo(items);
                manager.RemoveSavedItems();
                CloseCommand.Execute(null);
            });
        }

        public async override Task<List<InventoryItemViewModel>> LoadData()
        {
            var items = await manager.GetInventoryItems();
            var savedItems = manager.GetSavedItems();

            var vms = items.Select(s => new InventoryItemViewModel(s)).ToList();

            foreach (var savedItem in savedItems)
            {
                var vm = vms.FirstOrDefault(v => v.SuplyProductId == savedItem.SuplyProductId);
                if (vm != null)
                {
                    vm.QuantityAfter = savedItem.QuantityAfer;
                    vm.IsProceeded = true;
                }
            }
            return vms;
        }

        private InventoryItem Map(InventoryItemViewModel vm)
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

        protected override void DoUnsubscribe()
        {
            base.DoUnsubscribe();
            Unsubscribe<InventoryItemChangedMessage>(token);
        }
    }
}
