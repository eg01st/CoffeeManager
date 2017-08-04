using System;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public class ProductManager : BaseManager, IProductManager
    {
        readonly IProductProvider productProvider;

        public ProductManager(IProductProvider productProvider)
        {
            this.productProvider = productProvider;
        }

        public async Task<Product[]> GetCoffeeProducts()
        {
            return await productProvider.GetProduct(ProductType.Coffee);
        }

        public async Task<Product[]> GetTeaProducts()
        {
            return await productProvider.GetProduct(ProductType.Tea);
        }

        public async Task<Product[]> GetColdDrinksProducts()
        {
            return await productProvider.GetProduct(ProductType.ColdDrinks);
        }

        public async Task<Product[]> GetIceCreamProducts()
        {
            return await productProvider.GetProduct(ProductType.IceCream);
        }

        public async Task<Product[]> GetMealsProducts()
        {
            return await productProvider.GetProduct(ProductType.Meals);
        }

        public async Task<Product[]> GetWaterProducts()
        {
            return await productProvider.GetProduct(ProductType.Water);
        }

        public async Task<Product[]> GetSweetsProducts()
        {
            return await productProvider.GetProduct(ProductType.Sweets);
        }

        public async Task<Product[]> GetAddsProducts()
        {
            return await productProvider.GetProduct(ProductType.Adds);
        }


        public async Task SaleProduct(int id, decimal price, bool isPoliceSale, bool isCreditCardSale, bool isSaleByWeight, decimal? weight)
        {
            await productProvider.SaleProduct(ShiftNo, id, price, isPoliceSale, isCreditCardSale, isSaleByWeight, weight);
        }

        public async Task DismisSaleProduct(int id)
        {
            await productProvider.DeleteSale(ShiftNo, id);
        }
        public async Task UtilizeSaleProduct(int id)
        {
            await productProvider.UtilizeSaleProduct(ShiftNo, id);
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

    }
}
