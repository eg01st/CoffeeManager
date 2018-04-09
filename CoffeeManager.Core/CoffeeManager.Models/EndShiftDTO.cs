using System.Collections.Generic;
using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;

namespace CoffeeManager.Models
{
    public class EndShiftDTO : Entity
    {
        public int ShiftId { get; set; }
        public decimal RealAmount { get; set; }

        public int Counter { get; set; }
        public int CoffeePacks { get; set; }
        public int MilkPacks { get; set; }

        public List<CoffeeCounterDTO> CoffeeCounters { get; set; }
    }
}
