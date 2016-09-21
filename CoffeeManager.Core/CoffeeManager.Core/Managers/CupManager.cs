using CoffeeManager.Models;

namespace CoffeeManager.Core.Managers
{
    public class CupManager : BaseManager
    {
        public Cup[] GetSupportedCups()
        {
            return new[]
            {
                new Cup() {Capacity = 110, Name = "110 ml", Id = 1},
                new Cup() {Capacity = 110, Name = "170 ml", Id = 2},
                new Cup() {Capacity = 110, Name = "250 ml", Id = 3},
                new Cup() {Capacity = 110, Name = "400 ml", Id = 4},
                new Cup() {Capacity = 110, Name = "Пластиковый", Id = 5},
            };
        }

        public void UtilizeCup(int id)
        {
            
        }
    }
}
