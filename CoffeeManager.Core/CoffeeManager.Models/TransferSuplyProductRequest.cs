
namespace CoffeeManager.Models
{
    public class TransferSuplyProductRequest
    {
        public int CoffeeRoomIdFrom { get; set; }
        public int CoffeeRoomIdTo { get; set; }
        public int SuplyProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}
