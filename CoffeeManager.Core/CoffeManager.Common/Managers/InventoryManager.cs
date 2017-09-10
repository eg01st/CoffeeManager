using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public class InventoryManager : BaseManager, IInventoryManager
    {
        readonly IInventoryProvider provider;

        public InventoryManager(IInventoryProvider provider)
        {
            this.provider = provider;
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
    }
}
