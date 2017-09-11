using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public class InventoryProvider : BaseServiceProvider, IInventoryProvider
    {
        public async Task<IEnumerable<SupliedProduct>> GetInventoryItems()
        {
            return await Get<SupliedProduct[]>(RoutesConstants.GetInventoryItems);
        }

        public async Task<IEnumerable<InventoryItem>> GetInventoryReportDetails(int reportId)
        {
            return await Get<IEnumerable<InventoryItem>>(RoutesConstants.GetInventoryReportDetails,
                new Dictionary<string, string>()
                {
                    {nameof(reportId), reportId.ToString()},
                });
        }

        public async Task<IEnumerable<InventoryReport>> GetInventoryReports()
        {
            return await Get<IEnumerable<InventoryReport>>(RoutesConstants.GetInventoryReports);
        }

        public async Task SentInventoryInfo(IEnumerable<InventoryItem> items)
        {
            await Post(RoutesConstants.SentInventoryInfo, items);
        }

        public async Task ToggleItemInventoryEnabled(int suplyProductId)
        {
            await Post<object>(RoutesConstants.ToggleItemInventoryEnabled, null,
                new Dictionary<string, string>()
                {
                    {nameof(suplyProductId), suplyProductId.ToString()},
                });
        }
    }
}
