using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Cheesebaron.MvxPlugins.Connectivity;
using CoffeeManager.Common;
using CoffeeManager.Models;

namespace CoffeManager.Common
{
    public class ProductManager : BaseManager, IProductManager
    {
        readonly IProductProvider productProvider;
        readonly IDataBaseProvider databaseProvider;
        readonly IConnectivity connectivity;

        public ProductManager(IProductProvider productProvider, IDataBaseProvider databaseProvider, IConnectivity connectivity)
        {
            this.connectivity = connectivity;
            this.databaseProvider = databaseProvider;
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


        public async Task SaleProduct(int shiftId, int id, decimal price, bool isPoliceSale, bool isCreditCardSale, bool isSaleByWeight, decimal? weight)
        {
            var sale = new Sale()
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
            if(!connectivity.IsConnected)
            {
                databaseProvider.Add(sale);
                return;
            }
            try
            {
                await productProvider.SaleProduct(sale);
                await SendStoredSalesIfExist();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToDiagnosticString());
                //Email ex
                databaseProvider.Add(sale);
            }
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

        private async Task SendStoredSalesIfExist()
        {
            var storedItems = databaseProvider.Get<Sale>();
            foreach (var item in storedItems)
            {
                try
                {
                    await productProvider.SaleProduct(item);
                    databaseProvider.Remove(item);
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}
