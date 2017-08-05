using CoffeeManager.Models;
using SQLite;

namespace CoffeManager.Common
{
    public class ShiftEntity : Shift
    {
        [PrimaryKey, AutoIncrement]
        public int DatabaseId { get; set; }
    }
}
