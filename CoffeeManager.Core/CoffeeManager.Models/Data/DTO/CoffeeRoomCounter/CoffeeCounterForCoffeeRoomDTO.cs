namespace CoffeeManager.Models.Data.DTO.CoffeeRoomCounter
{
    public class CoffeeCounterForCoffeeRoomDTO
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int CoffeeRoomNo { get; set; }
        public int SuplyProductId { get; set; }
        public string Name { get; set; }
        public IsActiveDTO[] IsActive { get; set; }
    }
}
