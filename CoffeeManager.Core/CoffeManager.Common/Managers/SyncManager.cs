using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeManager.Common.Database;
using CoffeManager.Common.Providers;
using MobileCore.Connection;

namespace CoffeManager.Common.Managers
{
    public class SyncManager : BaseManager, ISyncManager
    {
        readonly IDataBaseProvider provider;
        readonly IProductProvider productProiver;
        private readonly IConnectivity connectivity;

        public SyncManager(IDataBaseProvider provider, IProductProvider productProiver, IConnectivity connectivity)
        {
            this.productProiver = productProiver;
            this.connectivity = connectivity;
            this.provider = provider;
        }

        public void AddCurrentShift(ShiftEntity shift)
        {
            provider.Add(shift);
        }


        public async Task AddProductsToSync(IEnumerable<ProductEntity> products, int categoryId)
        {
            await Task.Run(() =>
            {
                var itemsToRemove = provider.Get<ProductEntity>().Where(p => p.CategoryId == categoryId);
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

        public void AddSaleToSync(SaleEntity item, SaleAction action)
        {
            item.Action = action;
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
            provider.CreateTableIfNotExists<InventoryItemEntity>();
        }

        public void DeleteTable<T>() where T : new()
        {
            provider.DeleteTable<T>();
        }

        public ShiftEntity GetCurrentShift()
        {
            return provider.Get<ShiftEntity>().FirstOrDefault();
        }

        public IEnumerable<ProductEntity> GetProducts(int categoryId)
        {
            return provider.Get<ProductEntity>().Where(p => p.CategoryId == categoryId);
        }

        public void InitDataBaseConnection()
        {
            provider.InitConnection();
        }

        public async Task<bool> SyncSales()
        {
            if (!await connectivity.HasInternetConnectionAsync)
            {
                return true;
            }
            
            var storedItems = provider.Get<SaleEntity>();
            foreach (var item in storedItems)
            {
                try
                {
                    Debug.WriteLine($"SyncSales {item.Amount}");
                    switch (item.Action)
                    {
                        case SaleAction.Add:
                            await productProiver.SaleProduct((Sale)item);
                            break;
                        case SaleAction.Dismiss:
                            await productProiver.DeleteSale(item.ShiftId, item.Id);
                            break;
                        case SaleAction.Utilize:
                            await productProiver.UtilizeSaleProduct(item.ShiftId, item.Id);
                            break;
                    }

                    provider.Remove(item);
                }
                catch (HttpRequestException hrex)
                {
                    Debug.WriteLine(hrex.ToDiagnosticString());
                    return false;
                }
                catch (TaskCanceledException tcex)
                {
                    Debug.WriteLine(tcex.ToDiagnosticString());
                    return false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToDiagnosticString());
                    await EmailService?.SendErrorEmail($"CoffeeRoomId: {Config.CoffeeRoomNo}",ex.ToDiagnosticString());
                    return false;
                }
            }
            return true;
        }
    }
}
