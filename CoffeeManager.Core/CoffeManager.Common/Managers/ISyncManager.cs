using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeManager.Common.Database;

namespace CoffeManager.Common.Managers
{
    public interface ISyncManager
    {
        void AddSaleToSync(SaleEntity item, SaleAction action);
        Task<bool> SyncSales();

        Task AddProductsToSync(IEnumerable<ProductEntity> products, int categoryId);
        void AddCurrentShift(ShiftEntity shift);
        ShiftEntity GetCurrentShift();
        void ClearCurrentShift();
        IEnumerable<ProductEntity> GetProducts(int categoryId);

        void CreateSyncTables();
        void InitDataBaseConnection();

        void DeleteTable<T>() where T : new();
    }
}
