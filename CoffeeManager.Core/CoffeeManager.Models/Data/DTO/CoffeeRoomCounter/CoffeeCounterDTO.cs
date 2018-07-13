namespace CoffeeManager.Models.Data.DTO.CoffeeRoomCounter
{
    public class CoffeeCounterDTO
    {
        public int Id { get; set; }
        public int SuplyProductId { get; set; }
        public int CoffeeRoomId { get; set; }
        public string SuplyProductName { get; set; }
        public int StartCounter { get; set; }
        public int EndCounter { get; set; }
    }
}
