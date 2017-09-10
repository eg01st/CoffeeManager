using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeManager.Common;
using System.Linq;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CoffeeManager.Models;
namespace CoffeeManager.Core
{
    public class InventoryViewModel : BaseSearchViewModel<InventoryItemViewModel>
    {
        readonly IInventoryManager manager;

        public ICommand SendReportCommand { get; }

        public InventoryViewModel(IInventoryManager manager)
        {
            this.manager = manager;
            SendReportCommand = new MvxCommand(DoSendReport);
        }

        private async void DoSendReport()
        {
            if(Items.Any(i => !i.IsProceeded))
            {
                Alert("Не все товары прошли переучет");
                return;
            }
            var items = Items.Select(s => new InventoryItem()
            { 
                SuplyProductId = s.SuplyProductId,
                QuantityBefore = s.QuantityBefore,
                QuantityAfer = s.QuantityAfter.Value
            });
            await manager.SentInventoryInfo(items);
            CloseCommand.Execute(null);
        }

        public async override Task<List<InventoryItemViewModel>> LoadData()
        {
            var items = await manager.GetInventoryItems();
            return items.Select(s => new InventoryItemViewModel(s)).ToList();
        }
    }
}
