using System;
using System.Threading.Tasks;
using CoffeeManager.Models;
using System.Collections.Generic;
namespace CoffeManager.Common
{
    public interface IInventoryProvider
    {
        Task<IEnumerable<SupliedProduct>> GetInventoryItems();
        Task SentInventoryInfo(IEnumerable<InventoryItem> items);
        Task ToggleItemInventoryEnabled(int suplyProductId);
        Task<IEnumerable<InventoryReport>> GetInventoryReports();
        Task<IEnumerable<InventoryItem>> GetInventoryReportDetails(int reportId);
    }
}
