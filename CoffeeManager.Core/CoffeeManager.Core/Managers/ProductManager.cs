using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.ServiceProviders;
using CoffeeManager.Models;

namespace CoffeeManager.Core.Managers
{
    public class ProductManager : BaseManager
    {
        private ProductProvider provider = new ProductProvider();

        public async Task<Product[]> GetCoffeeProducts(bool isPoliceSale)
        {
            return await provider.GetProduct(ProductType.Coffee, isPoliceSale);
        }

        public async Task<Product[]> GetTeaProducts(bool isPoliceSale)
        {
            return await provider.GetProduct(ProductType.Tea, isPoliceSale);
        }

        public async Task<Product[]> GetColdDrinksProducts(bool isPoliceSale)
        {
            return await provider.GetProduct(ProductType.ColdDrinks, isPoliceSale);
        }

        public async Task<Product[]> GetIceCreamProducts(bool isPoliceSale)
        {
            return await provider.GetProduct(ProductType.IceCream, isPoliceSale);
        }

        public async Task<Product[]> GetMealsProducts(bool isPoliceSale)
        {
            return await provider.GetProduct(ProductType.Meals, isPoliceSale);
        }

        public async Task<Product[]> GetWaterProducts(bool isPoliceSale)
        {
            return await provider.GetProduct(ProductType.Water, isPoliceSale);
        }


        public async Task SaleProduct(int id, bool isPoliceSale)
        {
            await provider.SaleProduct(new Product() {CofferRoomNo = CoffeeRoomNo, Id = id, IsPoliceSale = isPoliceSale});
        }


        public async Task DismisSaleProduct(int id)
        {
            await provider.DeleteSale(new Product() { CofferRoomNo = CoffeeRoomNo, Id = id });
        }
    }
}
