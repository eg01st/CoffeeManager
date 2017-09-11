using CoffeeManager.Models;
using SQLite;

namespace CoffeManager.Common.Database
{
    public class InventoryItemEntity : InventoryItem
    {
        [PrimaryKey, AutoIncrement]
        public int DatabaseId { get; set; }
    }
}
