using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public interface IInventoryManager
    {
        Task<IEnumerable<SupliedProduct>> GetInventoryItems();
        Task SentInventoryInfo(IEnumerable<InventoryItem> items);
        Task ToggleItemInventoryEnabled(int suplyProductId);
        Task<IEnumerable<InventoryReport>> GetInventoryReports();
        Task<IEnumerable<InventoryItem>> GetInventoryReportDetails(int reportId);
    }
}
