using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.ServiceProviders;
using CoffeeManager.Models;
using MvvmCross.Platform;
using MvvmCross.Plugins.File;
using Newtonsoft.Json;

namespace CoffeeManager.Core.Managers
{
    public class ProductManager : BaseManager
    {
        private ProductProvider provider = new ProductProvider();
        private static IMvxFileStore storage = Mvx.Resolve<IMvxFileStore>();
        private static readonly object sync = new object();
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

        public async Task<Product[]> GetSweetsProducts()
        {
            return await provider.GetProduct(ProductType.Sweets);
        }

        public async Task<Product[]> GetAddsProducts()
        {
            return await provider.GetProduct(ProductType.Adds);
        }


        public async Task SaleProduct(int id, decimal price, bool isPoliceSale, bool isCreditCardSale)
        {
            await Task.Run(() =>
            {
                lock (sync)
                {
                    var st = GetSalesStorage();
                    st.Sales.Add(new Sale() {Product = id, Amount = price, IsPoliceSale = isPoliceSale, IsCreditCardSale = isCreditCardSale});
                    SaveStorage(st);
                }
            });
            await provider.SaleProduct(ShiftNo, id, price, isPoliceSale, isCreditCardSale);
        }

        public async Task DismisSaleProduct(int id)
        {
            await Task.Run(() =>
            {
                lock (sync)
                {
                    var st = GetSalesStorage();
                    st.DismissedSales.Add(new Sale() {Id = id,});
                    SaveStorage(st);
                }
            });
            await provider.DeleteSale(ShiftNo, id);
        }
        public async Task UtilizeSaleProduct(int id)
        {
            await Task.Run(() =>
            {
                lock (sync)
                {
                    var st = GetSalesStorage();
                    st.UtilizedSales.Add(new Sale() {Id = id,});
                    SaveStorage(st);
                }
            });
            await provider.UtilizeSaleProduct(ShiftNo, id);
        }

        private const string Sales = "Sales";

        public static SaleStorage GetSalesStorage()
        {
            string storageJson;
            if (storage.TryReadTextFile(Sales, out storageJson))
            {
                return JsonConvert.DeserializeObject<SaleStorage>(storageJson);
            }
            else
            {
                return new SaleStorage();
            }
        }

        public static void ClearStorage()
        {
            SaveStorage(new SaleStorage());
        }

        private static void SaveStorage(SaleStorage st)
        {
            storage.WriteFile(Sales, JsonConvert.SerializeObject(st));
        }
    }
}
