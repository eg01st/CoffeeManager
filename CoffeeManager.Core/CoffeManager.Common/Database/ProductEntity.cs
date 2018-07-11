using CoffeeManager.Models;
using CoffeeManager.Models.Data.Product;
using SQLite;

namespace CoffeManager.Common.Database
{
    public class ProductEntity : ProductDTO
    {
        [PrimaryKey, AutoIncrement]
        public int DatabaseId { get; set; }
    }
}
