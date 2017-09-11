using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeManager.Common.Database;

namespace CoffeManager.Common
{
    public class InventoryManager : BaseManager, IInventoryManager
    {
        readonly IInventoryProvider provider;
        private readonly IDataBaseProvider _dataBaseProvider;

        public InventoryManager(IInventoryProvider provider, IDataBaseProvider dataBaseProvider)
        {
            this.provider = provider;
            _dataBaseProvider = dataBaseProvider;
        }

        public async Task<IEnumerable<SupliedProduct>> GetInventoryItems()
        {
            return await provider.GetInventoryItems();
        }

        public async Task<IEnumerable<InventoryItem>> GetInventoryReportDetails(int reportId)
        {
            return await provider.GetInventoryReportDetails(reportId);
        }

        public async Task<IEnumerable<InventoryReport>> GetInventoryReports()
        {
            return await provider.GetInventoryReports();
        }

        public async Task SentInventoryInfo(IEnumerable<InventoryItem> items)
        {
            await provider.SentInventoryInfo(items);
        }

        public async Task ToggleItemInventoryEnabled(int suplyProductId)
        {
            await provider.ToggleItemInventoryEnabled(suplyProductId);
        }

        public void SaveReportItem(InventoryItem item)
        {
            InventoryItemEntity entity = new InventoryItemEntity
            {
                Id = item.Id,
                SuplyProductId = item.SuplyProductId,
                CoffeeRoomNo = item.CoffeeRoomNo,
                QuantityAfer = item.QuantityAfer,
                QuantityBefore = item.QuantityBefore,
                SuplyProductName = item.SuplyProductName
            };
            _dataBaseProvider.Add(entity);
        }

        public IEnumerable<InventoryItem> GetSavedItems()
        {
            return _dataBaseProvider.Get<InventoryItemEntity>();
        }

        public void RemoveSavedItems()
        {
            _dataBaseProvider.ClearTable<InventoryItemEntity>();
        }
    }
}
