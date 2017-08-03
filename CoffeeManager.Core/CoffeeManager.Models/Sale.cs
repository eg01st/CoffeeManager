using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManager.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int ShiftId { get; set; }
        public Product Product1 { get; set; }

        public int CoffeeRoomNo { get; set; }

        public int Product { get; set; }
        public bool IsPoliceSale { get; set; }
        public DateTime Time { get; set; }

        public bool IsRejected { get; set; }

        public bool IsUtilized { get; set; }

        public bool IsCreditCardSale { get; set; }

        public bool IsSaleByWeight { get; set; }
        public decimal? Weight { get; set; }
    }
}
