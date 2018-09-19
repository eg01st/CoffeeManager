using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManager.Models.Data.Product
{
    public class ProductPriceDTO
    {
        public int Id { get; set; }
        public int CoffeeRoomNo { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
    }
}
