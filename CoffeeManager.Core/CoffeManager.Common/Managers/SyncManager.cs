﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeManager.Common.Database;

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
                    EmailService?.SendErrorEmail(hrex.ToDiagnosticString());
                    return false;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToDiagnosticString());
                    EmailService?.SendErrorEmail(ex.ToDiagnosticString());
                    return false;
                }
            }
            return true;
        }
    }
}
