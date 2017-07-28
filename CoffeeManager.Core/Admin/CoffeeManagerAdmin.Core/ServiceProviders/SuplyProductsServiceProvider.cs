using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ServiceProviders;
namespace CoffeeManagerAdmin.Core
{
    public class SuplyProductsServiceProvider : BaseServiceProvider
    {
        private const string SuplyProducts = "suplyproducts";
        public SuplyProductsServiceProvider()
        {
        }

        public async Task<SupliedProduct[]> GetSupliedProducts()
        {
            return await Get<SupliedProduct[]>($"{SuplyProducts}/getproducts");
        }

        public async Task AddNewProduct(string newProduct)
        {
            await
                Put($"{SuplyProducts}/addproduct",
                    new SupliedProduct() { CoffeeRoomNo = CoffeeRoomNo, Quatity = 0, Price = 0, Name = newProduct });
        }

        public async Task DeleteSuplyRequest(int id)
        {
            await Delete($"{SuplyProducts}/deleteSuplyRequest", new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task AddNewSuplyRequest(IEnumerable<SupliedProduct> items)
        {
            await Put($"{SuplyProducts}/addsuplyrequest", items);
        }

        public async Task<SupliedProduct[]> GetSuplyRequests()
        {
            return await Get<SupliedProduct[]>($"{SuplyProducts}/getsuplyrequest");
        }

        public async Task<SupliedProduct> GetSupliedProduct(int id)
        {
            return await Get<SupliedProduct>($"{SuplyProducts}/getProduct", new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task SaveSuplyProduct(SupliedProduct supliedProduct)
        {
            await Post($"{SuplyProducts}/editSuplyProduct", supliedProduct);
        }

        public async Task DeleteSuplyProduct(int id)
        {
            await Delete($"{SuplyProducts}/deleteSuplyProduct", new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task ProcessSuplyRequests(IEnumerable<SupliedProduct> items)
        {
            await Post($"{SuplyProducts}/processsuplyrequest", items);
        }

        public async Task<ProductCalculationEntity> GetProductCalculationItems(int productId)
        {
            return await
                Get<ProductCalculationEntity>($"{SuplyProducts}/getproductcalculationitems",
                    new Dictionary<string, string>() { { nameof(productId), productId.ToString() } });
        }

        public async Task DeleteProductCalculationItem(int id)
        {
            await
                Delete($"{SuplyProducts}/deleteproductcalculationitem",
                    new Dictionary<string, string>() { { nameof(id), id.ToString() } });
        }

        public async Task AddProductCalculationItem(ProductCalculationEntity productCalculationEntity)
        {
            await Put($"{SuplyProducts}/addproductcalculationitem", productCalculationEntity);
        }
    }
}
