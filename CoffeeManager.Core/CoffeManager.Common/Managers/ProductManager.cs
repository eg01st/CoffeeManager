using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.Product;
using CoffeManager.Common.Database;
using CoffeManager.Common.Providers;
using MobileCore;
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

        public async Task<ProductDTO[]> GetProducts(int categoryId)
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

        public async Task<int> AddProduct(ProductDetaisDTO productDTO)
        {
            return await productProvider.AddProduct(productDTO);
        }

        public async Task DeleteProduct(int id)
        {
            await productProvider.DeleteProduct(id);
        }

        public async Task EditProduct(ProductDetaisDTO productDTO)
        {
            await productProvider.EditProduct(productDTO);
        }

        public async Task<ProductDetaisDTO> GetProduct(int productId)
        {
            return await productProvider.GetProduct(productId);
        }
        
        public async Task<ProductDTO[]> GetProducts()
        {
            return await productProvider.GetProducts();
        }

        public async Task ToggleIsActiveProduct(int id)
        {
            await productProvider.ToggleIsActiveProduct(id);
        }

        public async Task<string[]> GetAvaivalbeProductColors()
        {
            return await productProvider.GetAvaivalbeProductColors();
        }

        private async Task<ProductDTO[]> GetAndSyncProduct(int categoryId)
        {
            IEnumerable<ProductEntity> products;
            if(! await connectivity.HasInternetConnectionAsync)
            {
                products = syncManager.GetProducts(categoryId);
                return products.ToArray();

            }
            try
            {
                products = await productProvider.GetProducts(categoryId);
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
