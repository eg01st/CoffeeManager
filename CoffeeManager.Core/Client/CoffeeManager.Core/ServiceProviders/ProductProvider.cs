﻿using System;
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

        private const string Products = "Products";
        public async Task<Product[]> GetProduct(ProductType type)
        {
            var prods = await Get<Product[]>(Products,
                new Dictionary<string, string>()
                {
                    {nameof(ProductType), ((int) type).ToString()},
                });
            return prods.Where(p => p.IsActive).ToArray();
        }

        public async Task SaleProduct(int shiftId, int id, decimal price, bool isPoliceSale, bool isCreditCardSale)
        {
            var sale = new Sale()
            {
                ShiftId = shiftId,
                Amount = price,
                Product = id,
                IsPoliceSale = isPoliceSale,
                CoffeeRoomNo = CoffeeRoomNo,
                Time = DateTime.Now,
                IsCreditCardSale = isCreditCardSale
            };
            await PutInternal($"{Products}/SaleProduct", JsonConvert.SerializeObject(sale));
        }

        public async Task DeleteSale(int shiftId, int id)
        {
            await PostInternal($"{Products}/DeleteSale", JsonConvert.SerializeObject(new Sale() { ShiftId = shiftId, Id = id}));
        } 

        public async Task UtilizeSaleProduct(int shiftId, int id)
        {
            await PostInternal($"{Products}/UtilizeSale", JsonConvert.SerializeObject(new Sale() { ShiftId = shiftId, Id = id }));
        }
    }
}