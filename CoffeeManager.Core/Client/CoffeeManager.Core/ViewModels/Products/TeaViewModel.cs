﻿using System.Threading.Tasks;
using CoffeeManager.Core.ViewModels.Products;
using CoffeeManager.Models;
using CoffeManager.Common;

namespace CoffeeManager.Core.ViewModels
{
    public class TeaViewModel : ProductBaseViewModel
    {
        public TeaViewModel(IProductManager productManager) : base(productManager)
        {
        }

        protected override async Task<Product[]> GetProducts()
        {
            return await ProductManager.GetTeaProducts();
        }
    }
}
