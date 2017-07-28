using System;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Managers;

namespace CoffeeManagerAdmin.Core
{
    public class ProductManager : BaseManager
    {
        ProductProvider provider = new ProductProvider();
        public ProductManager()
        {
        }

        public async Task AddProduct(string name, string price, string policePrice, int cupType, int productTypeId)
        {
            await provider.AddProduct(new Product { Name = name, Price = decimal.Parse(price), PolicePrice = decimal.Parse(policePrice), ProductType = productTypeId, CupType = cupType, CoffeeRoomNo = Config.CoffeeRoomNo });
        }

        public async Task DeleteProduct(int id)
        {
            await provider.DeleteProduct(id);
        }

        public async Task EditProduct(int id, string name, string price, string policePrice, int cupType, int productTypeId)
        {
            await provider.EditProduct(new Product { Id = id, Name = name, Price = decimal.Parse(price), PolicePrice = decimal.Parse(policePrice), ProductType = productTypeId, CupType = cupType, CoffeeRoomNo = Config.CoffeeRoomNo });
        }

        public async Task<Product[]> GetProducts()
        {
            return await provider.GetProducts();
        }

        public async Task ToggleIsActiveProduct(int id)
        {
            await provider.ToggleIsActiveProduct(id);
        }
    }
}
