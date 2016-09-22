using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManager.Core.Managers
{
    public class ProductManager : BaseManager
    {
        private Product[] mock = new Product[]
            {
                new Product {Id = 1, Name = "coffee one", Price = 3.33f},
                new Product {Id = 2, Name = "coffee two", Price = 4},
                new Product {Id = 3, Name = "coffee tree", Price = 65},
                new Product {Id = 4, Name = "coffee four", Price = 22},
                                new Product {Id = 5, Name = "coffee one", Price = 3.33f},
                new Product {Id = 6, Name = "coffee two", Price = 4},
                new Product {Id = 7, Name = "coffee tree", Price = 65},
                new Product {Id = 8, Name = "coffee four", Price = 22},
                                new Product {Id = 9, Name = "coffee one", Price = 3.33f},
                new Product {Id = 10, Name = "coffee two", Price = 4},
                new Product {Id = 11, Name = "coffee tree", Price = 65},
                new Product {Id = 12, Name = "coffee four", Price = 22},
                 new Product {Id = 1, Name = "coffee one", Price = 3.33f},
                new Product {Id = 2, Name = "coffee two", Price = 4},
                new Product {Id = 3, Name = "coffee tree", Price = 65},
                new Product {Id = 4, Name = "coffee four", Price = 22},
                                new Product {Id = 5, Name = "coffee one", Price = 3.33f},
                new Product {Id = 6, Name = "coffee two", Price = 4},
                new Product {Id = 7, Name = "coffee tree", Price = 65},
                new Product {Id = 8, Name = "coffee four", Price = 22},
                                new Product {Id = 9, Name = "coffee one", Price = 3.33f},
                new Product {Id = 10, Name = "coffee two", Price = 4},
                new Product {Id = 11, Name = "coffee tree", Price = 65},
                new Product {Id = 12, Name = "coffee four", Price = 22},
                 new Product {Id = 1, Name = "coffee one", Price = 3.33f},
                new Product {Id = 2, Name = "coffee two", Price = 4},
                new Product {Id = 3, Name = "coffee tree", Price = 65},
                new Product {Id = 4, Name = "coffee four", Price = 22},
                                new Product {Id = 5, Name = "coffee one", Price = 3.33f},
                new Product {Id = 6, Name = "coffee two", Price = 4},
                new Product {Id = 7, Name = "coffee tree", Price = 65},
                new Product {Id = 8, Name = "coffee four", Price = 22},
                                new Product {Id = 9, Name = "coffee one", Price = 3.33f},
                new Product {Id = 10, Name = "coffee two", Price = 4},
                new Product {Id = 11, Name = "coffee tree", Price = 65},
                new Product {Id = 12, Name = "coffee four", Price = 22},
            };

        public Product[] GetCoffeeProducts(bool isPoliceSale)
        {
            if (isPoliceSale)
            {
                return new Product[]
                {
                    new Product {Id = 1, Name = "coffee one cop", Price = 3.33f, IsPoliceSale = true},
                    new Product {Id = 2, Name = "coffee two cop", Price = 4, IsPoliceSale = true},
                    new Product {Id = 3, Name = "coffee tree cop", Price = 65, IsPoliceSale = true},
                    new Product {Id = 4, Name = "coffee four cop", Price = 22, IsPoliceSale = true},
                    new Product {Id = 5, Name = "coffee one cop", Price = 3.33f, IsPoliceSale = true},
                    new Product {Id = 6, Name = "coffee two cop", Price = 4, IsPoliceSale = true},
                    new Product {Id = 7, Name = "coffee tree cop", Price = 65, IsPoliceSale = true},
                    new Product {Id = 8, Name = "coffee four cop", Price = 22, IsPoliceSale = true},
                    new Product {Id = 9, Name = "coffee one cop", Price = 3.33f, IsPoliceSale = true},
                };
            }
            else
            {
                return mock;
            }

        }

        public Product[] GetTeaProducts(bool isPoliceSale)
        {
            return mock;
        }

        public Product[] GetColdDrinksProducts(bool isPoliceSale)
        {
            return mock;

        }

        public Product[] GetIceCreamProducts(bool isPoliceSale)
        {
            return mock;
        }

        public Product[] GetMealsProducts(bool isPoliceSale)
        {
            return mock;
        }

        public Product[] GetWaterProducts(bool isPoliceSale)
        {
            return mock;
        }


        public void SaleProduct(int id)
        {
            
        }


        public void DismisSaleProduct(int id)
        {

        }
    }
}
