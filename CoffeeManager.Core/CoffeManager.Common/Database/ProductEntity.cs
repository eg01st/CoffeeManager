using CoffeeManager.Models;
using SQLite;

namespace CoffeManager.Common.Database
{
    public class ProductEntity : Product
    {
        [PrimaryKey, AutoIncrement]
        public int DatabaseId { get; set; }
    }
}
