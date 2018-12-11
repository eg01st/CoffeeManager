using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.AutoOrder;

namespace CoffeManager.Common.Managers
{
    public interface IInventoryManager
    {
        Task<IEnumerable<SupliedProduct>> GetInventoryItems();
        Task SentInventoryInfo(IEnumerable<InventoryItem> items);
        Task ToggleItemInventoryEnabled(int suplyProductId);
        Task<IEnumerable<InventoryReport>> GetInventoryReports();
        Task<IEnumerable<InventoryItem>> GetInventoryReportDetails(int reportId);
        void SaveReportItem(InventoryItem item);
        IEnumerable<InventoryItem> GetSavedItems();
        void RemoveSavedItems();
        Task<IEnumerable<InventoryItemsInfoForShiftDTO>> GetInventoryItemsForShiftToUpdate();
        Task SendInventoryItemsForShiftToUpdate(List<SupliedProduct> dto);
    }
}
