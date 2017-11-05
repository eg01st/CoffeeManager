using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;
using MvvmCross.Platform;

namespace CoffeManager.Common
{
    public class ProductManager : BaseManager, IProductManager
    {
        private readonly IProductProvider productProvider;
        private readonly ISyncManager syncManager;

        private static readonly object salesSyncLock = new object();

        public ProductManager(IProductProvider productProvider, ISyncManager syncManager)
        {
            this.syncManager = syncManager;
            this.productProvider = productProvider;
        }

        public async Task<Product[]> GetCoffeeProducts()
        {
            return await GetAndSyncProduct(ProductType.Coffee);
        }

        public async Task<Product[]> GetTeaProducts()
        {
            return await GetAndSyncProduct(ProductType.Tea);
        }

        public async Task<Product[]> GetColdDrinksProducts()
        {
            return await GetAndSyncProduct(ProductType.ColdDrinks);
        }

        public async Task<Product[]> GetIceCreamProducts()
        {
            return await GetAndSyncProduct(ProductType.IceCream);
        }

        public async Task<Product[]> GetMealsProducts()
        {
            return await GetAndSyncProduct(ProductType.Meals);
        }

        public async Task<Product[]> GetWaterProducts()
        {
            return await GetAndSyncProduct(ProductType.Water);
        }

        public async Task<Product[]> GetSweetsProducts()
        {
            return await GetAndSyncProduct(ProductType.Sweets);
        }

        public async Task<Product[]> GetAddsProducts()
        {
            return await GetAndSyncProduct(ProductType.Adds);
        }


        public async Task SaleProduct(int shiftId, int id, decimal price, bool isPoliceSale, bool isCreditCardSale, bool isSaleByWeight, decimal? weight)
        {
            var sale = new SaleEntity()
            {
                ShiftId = shiftId,
                Amount = price,
                ProductId = id,
                IsPoliceSale = isPoliceSale,
                CoffeeRoomNo = Config.CoffeeRoomNo,
                Time = DateTime.Now,
                IsCreditCardSale = isCreditCardSale,
                IsSaleByWeight = isSaleByWeight,
                Weight = weight
            };
            try
            {
                await productProvider.SaleProduct((Sale)sale);
            }
            catch (HttpRequestException hrex)
            {
                Debug.WriteLine(hrex.ToDiagnosticString());
                syncManager.AddSaleToSync(sale, SaleAction.Add);
                return;
            }
            catch (TaskCanceledException tcex)
            {
                Debug.WriteLine(tcex.ToDiagnosticString());
                syncManager.AddSaleToSync(sale, SaleAction.Add);
                return;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToDiagnosticString());
                await EmailService?.SendErrorEmail($"CoffeeRoomId: {Config.CoffeeRoomNo}",ex.ToDiagnosticString());
                syncManager.AddSaleToSync(sale, SaleAction.Add);
                return;
            }

            lock(salesSyncLock)
            {
                syncManager.SyncSales();
            }
        }

        public async Task DismisSaleProduct(int id)
        {
            try
            {
                await productProvider.DeleteSale(ShiftNo, id);
                await syncManager.SyncSales();
            }
            catch (HttpRequestException hrex)
            {
                Debug.WriteLine(hrex.ToDiagnosticString());
                syncManager.AddSaleToSync(new SaleEntity() { Id = id, ShiftId = ShiftNo }, SaleAction.Dismiss);
            }
            catch (TaskCanceledException tcex)
            {
                Debug.WriteLine(tcex.ToDiagnosticString());
                syncManager.AddSaleToSync(new SaleEntity() { Id = id, ShiftId = ShiftNo }, SaleAction.Dismiss);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToDiagnosticString());
                await EmailService?.SendErrorEmail($"CoffeeRoomId: {Config.CoffeeRoomNo}",ex.ToDiagnosticString());
                syncManager.AddSaleToSync(new SaleEntity() { Id = id, ShiftId = ShiftNo}, SaleAction.Dismiss);
            }

        }
        public async Task UtilizeSaleProduct(int id)
        {
            try
            {
                await productProvider.UtilizeSaleProduct(ShiftNo, id);
                await syncManager.SyncSales();
            }
            catch (HttpRequestException hrex)
            {
                Debug.WriteLine(hrex.ToDiagnosticString());
                syncManager.AddSaleToSync(new SaleEntity() { Id = id, ShiftId = ShiftNo }, SaleAction.Utilize);
            }
            catch (TaskCanceledException tcex)
            {
                Debug.WriteLine(tcex.ToDiagnosticString());
                syncManager.AddSaleToSync(new SaleEntity() { Id = id, ShiftId = ShiftNo }, SaleAction.Utilize);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToDiagnosticString());
                await EmailService?.SendErrorEmail($"CoffeeRoomId: {Config.CoffeeRoomNo}",ex.ToDiagnosticString());
                syncManager.AddSaleToSync(new SaleEntity() { Id = id, ShiftId = ShiftNo }, SaleAction.Utilize);
            }
        }

        public async Task AddProduct(string name, string price, string policePrice, int cupType, int productTypeId, bool isSaleByWeight)
        {
            await productProvider.AddProduct(new Product { Name = name, Price = decimal.Parse(price), PolicePrice = decimal.Parse(policePrice), ProductType = productTypeId, CupType = cupType, CoffeeRoomNo = Config.CoffeeRoomNo, IsSaleByWeight = isSaleByWeight });
        }

        public async Task DeleteProduct(int id)
        {
            await productProvider.DeleteProduct(id);
        }

        public async Task EditProduct(int id, string name, string price, string policePrice, int cupType, int productTypeId, bool isSaleByWeight)
        {
            await productProvider.EditProduct(new Product { Id = id, Name = name, Price = decimal.Parse(price), PolicePrice = decimal.Parse(policePrice), ProductType = productTypeId, CupType = cupType, CoffeeRoomNo = Config.CoffeeRoomNo, IsSaleByWeight = isSaleByWeight });
        }

        public async Task<Product[]> GetProducts()
        {
            return await productProvider.GetProducts();
        }

        public async Task ToggleIsActiveProduct(int id)
        {
            await productProvider.ToggleIsActiveProduct(id);
        }

        private async Task<Product[]> GetAndSyncProduct(ProductType type)
        {
            IEnumerable<ProductEntity> products;
            try
            {
                products = await productProvider.GetProduct(type);
                syncManager.AddProductsToSync(products, type);
            }
            catch (HttpRequestException hrex)
            {
                Debug.WriteLine(hrex.ToDiagnosticString());
                products = syncManager.GetProducts(type);
            }
            catch (TaskCanceledException tcex)
            {
                Debug.WriteLine(tcex.ToDiagnosticString());
                products = syncManager.GetProducts(type);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToDiagnosticString());
                await EmailService?.SendErrorEmail($"CoffeeRoomId: {Config.CoffeeRoomNo}",ex.ToDiagnosticString());
                throw;
            }
            return products.ToArray();
        }
    }
}
