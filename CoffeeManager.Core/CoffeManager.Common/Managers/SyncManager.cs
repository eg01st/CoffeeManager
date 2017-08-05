using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public class SyncManager : BaseManager, ISyncManager
    {
        readonly IDataBaseProvider provider;
        readonly IProductProvider productProiver;

        public SyncManager(IDataBaseProvider provider, IProductProvider productProiver)
        {
            this.productProiver = productProiver;
            this.provider = provider;
        }

        public void AddCurrentShift(ShiftEntity shift)
        {
            provider.Add(shift);
        }


        public async Task AddProductsToSync(IEnumerable<ProductEntity> products, ProductType type)
        {
            await Task.Run(() =>
            {
                var itemsToRemove = provider.Get<ProductEntity>().Where(p => p.ProductType == (int)type);
                foreach (var item in itemsToRemove)
                {
                    provider.Remove(item);
                }
                foreach (var product in products)
                {
                    provider.Add(product);
                }
            });
        }

        public void AddSaleToSync(SaleEntity item)
        {
            provider.Add(item);
        }

        public void ClearCurrentShift()
        {
            provider.ClearTable<ShiftEntity>();
        }

        public void CreateSyncTables()
        {
            provider.CreateTableIfNotExists<SaleEntity>();
            provider.CreateTableIfNotExists<ShiftEntity>();
            provider.CreateTableIfNotExists<ProductEntity>();
        }

        public ShiftEntity GetCurrentShift()
        {
            return provider.Get<ShiftEntity>().FirstOrDefault();
        }

        public IEnumerable<ProductEntity> GetProducts(ProductType type)
        {
            return provider.Get<ProductEntity>().Where(p => p.ProductType == (int)type);
        }

        public void InitDataBaseConnection()
        {
            provider.InitConnection();
        }

        public async Task<bool> SyncSales()
        {
            var storedItems = provider.Get<SaleEntity>();
            foreach (var item in storedItems)
            {
                try
                {
                    await productProiver.SaleProduct((Sale)item);
                    provider.Remove(item);
                }
                catch (HttpRequestException hrex)
                {
                    Debug.WriteLine(hrex.ToDiagnosticString());
                    return false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToDiagnosticString());
                    //Email instead rethrow
                    throw;
                }
            }
            return true;
        }
    }
}
