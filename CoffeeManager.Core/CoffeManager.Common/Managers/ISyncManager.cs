using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public interface ISyncManager
    {
        void AddSaleToSync(SaleEntity item, SaleAction action);
        Task<bool> SyncSales();

        Task AddProductsToSync(IEnumerable<ProductEntity> products, ProductType type);
        void AddCurrentShift(ShiftEntity shift);
        ShiftEntity GetCurrentShift();
        void ClearCurrentShift();
        IEnumerable<ProductEntity> GetProducts(ProductType type);

        void CreateSyncTables();
        void InitDataBaseConnection();
    }
}
