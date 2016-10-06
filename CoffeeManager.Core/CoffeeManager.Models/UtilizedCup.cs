using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManager.Models
{
    public class UtilizedCup
    {
        public int Id { get; set; }
        public int ShiftId { get; set; }
        public int CupTypeId { get; set; }
        public DateTime DateTime { get; set; }
        public int CoffeeRoomNo { get; set; }
    }
}
