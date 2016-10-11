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

        public async Task<Product[]> GetCoffeeProducts()
        {
            return await provider.GetProduct(ProductType.Coffee);
        }

        public async Task<Product[]> GetTeaProducts()
        {
            return await provider.GetProduct(ProductType.Tea);
        }

        public async Task<Product[]> GetColdDrinksProducts()
        {
            return await provider.GetProduct(ProductType.ColdDrinks);
        }

        public async Task<Product[]> GetIceCreamProducts()
        {
            return await provider.GetProduct(ProductType.IceCream);
        }

        public async Task<Product[]> GetMealsProducts()
        {
            return await provider.GetProduct(ProductType.Meals);
        }

        public async Task<Product[]> GetWaterProducts()
        {
            return await provider.GetProduct(ProductType.Water);
        }


        public async Task SaleProduct(int id, decimal price, bool isPoliceSale)
        {
            await provider.SaleProduct(ShiftNo, id, price, isPoliceSale);
        }


        public async Task DismisSaleProduct(int id)
        {
            await provider.DeleteSale(ShiftNo, id);
        }

        public async Task<Product[]> GetSweetsProducts()
        {
            return await provider.GetProduct(ProductType.Sweets);
        }

        public async Task<Product[]> GetAddsProducts()
        {
            return await provider.GetProduct(ProductType.Adds);
        }
    }
}
