using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeManager.Common.Providers;
using MobileCore.Connection;

namespace CoffeManager.Common.Managers
{
    public class ProductManager : BaseManager, IProductManager
    {
        private readonly IProductProvider productProvider;
        private readonly ISyncManager syncManager;

        readonly IConnectivity connectivity;

        public ProductManager(IProductProvider productProvider, ISyncManager syncManager, IConnectivity connectivity)
        {
            this.connectivity = connectivity;
            this.syncManager = syncManager;
            this.productProvider = productProvider;
        }

        public async Task<Product[]> GetProducts(int categoryId)
        {
            return await GetAndSyncProduct(categoryId).ConfigureAwait(false);
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
                Time = DateTime.Now.ToLocalTime(),
                IsCreditCardSale = isCreditCardSale,
                IsSaleByWeight = isSaleByWeight,
                Weight = weight
            };

            if (!await connectivity.HasInternetConnectionAsync)
            {
                syncManager.AddSaleToSync(sale, SaleAction.Add);
                return;
            }
        
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
                //await EmailService?.SendErrorEmail($"CoffeeRoomId: {Config.CoffeeRoomNo}",ex.ToDiagnosticString());
               // syncManager.AddSaleToSync(sale, SaleAction.Add);
                throw;
            }

           
        }

        public async Task DismisSaleProduct(int id)
        {
            if (!await connectivity.HasInternetConnectionAsync)
            {
                syncManager.AddSaleToSync(new SaleEntity() { Id = id, ShiftId = ShiftNo }, SaleAction.Dismiss);
                return;
            }
            try
            {
                await productProvider.DeleteSale(ShiftNo, id);
            }
            catch (HttpRequestException hrex)
            {
                Debug.WriteLine(hrex.ToDiagnosticString());

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
            if (!await connectivity.HasInternetConnectionAsync)
            {
                syncManager.AddSaleToSync(new SaleEntity() { Id = id, ShiftId = ShiftNo }, SaleAction.Utilize);
                return;
            }
            try
            {
                await productProvider.UtilizeSaleProduct(ShiftNo, id);
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

        public async Task AddProduct(string name, string price, string policePrice, int cupType, int productTypeId, bool isSaleByWeight, int categoryId)
        {
            await productProvider.AddProduct(new Product { Name = name, Price = decimal.Parse(price), PolicePrice = decimal.Parse(policePrice), ProductType = productTypeId, CupType = cupType, CoffeeRoomNo = Config.CoffeeRoomNo, IsSaleByWeight = isSaleByWeight, CategoryId = categoryId });
        }

        public async Task DeleteProduct(int id)
        {
            await productProvider.DeleteProduct(id);
        }

        public async Task EditProduct(int id, string name, string price, string policePrice, int cupType, int productTypeId, bool isSaleByWeight, int categoryId)
        {
            await productProvider.EditProduct(new Product { Id = id, Name = name, Price = decimal.Parse(price), PolicePrice = decimal.Parse(policePrice), ProductType = productTypeId, CupType = cupType, CoffeeRoomNo = Config.CoffeeRoomNo, IsSaleByWeight = isSaleByWeight, CategoryId = categoryId });
        }

        public async Task<Product[]> GetProducts()
        {
            return await productProvider.GetProducts();
        }

        public async Task ToggleIsActiveProduct(int id)
        {
            await productProvider.ToggleIsActiveProduct(id);
        }

        private async Task<Product[]> GetAndSyncProduct(int categoryId)
        {
            IEnumerable<ProductEntity> products;
            if(! await connectivity.HasInternetConnectionAsync)
            {
                products = syncManager.GetProducts(categoryId);
                return products.ToArray();

            }
            try
            {
                products = await productProvider.GetProduct(categoryId);
                syncManager.AddProductsToSync(products, categoryId);
            }
            catch (HttpRequestException hrex)
            {
                Debug.WriteLine(hrex.ToDiagnosticString());
                products = syncManager.GetProducts(categoryId);
            }
            catch (TaskCanceledException tcex)
            {
                Debug.WriteLine(tcex.ToDiagnosticString());
                products = syncManager.GetProducts(categoryId);
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
