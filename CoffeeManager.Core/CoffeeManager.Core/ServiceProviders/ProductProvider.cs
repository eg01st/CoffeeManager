using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;
using MvvmCross.Platform;
using MvvmCross.Plugins.File;
using Newtonsoft.Json;

namespace CoffeeManager.Core.ServiceProviders
{
    public class ProductProvider : BaseServiceProvider
    {
        private static IMvxFileStore storage = Mvx.Resolve<IMvxFileStore>();

        private const string Products = "Products";
        public async Task<Product[]> GetProduct(ProductType type)
        {
            return await Get<Product[]>(Products,
                new Dictionary<string, string>()
                {
                    {nameof(ProductType), ((int) type).ToString()},
                });
        }

        public async Task SaleProduct(int shiftId, int id, decimal price, bool isPoliceSale)
        {
            var sale = new Sale()
            {
                ShiftId = shiftId,
                Amount = price,
                Product = id,
                IsPoliceSale = isPoliceSale,
                CoffeeRoomNo = CoffeeRoomNo,
                Time = DateTime.Now
            };
            await Task.Run(() =>
            {
                var storage = GetSalesStorage();
                storage.Id++;
                SaveStorage(storage);
            });
  
            await PutInternal($"{Products}/SaleProduct", JsonConvert.SerializeObject(sale));
        }

        public async Task DeleteSale(int shiftId, int id)
        {
            await Task.Run(() =>
            {
                var storage = GetSalesStorage();
                storage.Id--;
                SaveStorage(storage);
            });
            await PostInternal($"{Products}/DeleteSale", JsonConvert.SerializeObject(new Sale() { ShiftId = shiftId, Id = id}));
        }

        private const string Sales = "Sales";

        public static Entity GetSalesStorage()
        {
            string storageJson;
            if (storage.TryReadTextFile(Sales, out storageJson))
            {
                return JsonConvert.DeserializeObject<Entity>(storageJson);
            }
            else
            {
                return new Entity() { Name  = Sales};
            }
        }

        private static void SaveStorage(Entity requestStorage)
        {
            storage.WriteFile(Sales, JsonConvert.SerializeObject(requestStorage));
        }
    }
}
