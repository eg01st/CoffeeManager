using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.AutoOrder;

namespace CoffeManager.Common.Providers
{
    public interface IInventoryProvider
    {
        Task<IEnumerable<SupliedProduct>> GetInventoryItems();
        Task SentInventoryInfo(IEnumerable<InventoryItem> items);
        Task ToggleItemInventoryEnabled(int suplyProductId);
        Task<IEnumerable<InventoryReport>> GetInventoryReports();
        Task<IEnumerable<InventoryItem>> GetInventoryReportDetails(int reportId);
        Task<IEnumerable<InventoryItemsInfoForShiftDTO>> GetInventoryItemsForShiftToUpdate();
        Task SendInventoryItemsForShiftToUpdate(List<SupliedProduct> dto);
    }
}
