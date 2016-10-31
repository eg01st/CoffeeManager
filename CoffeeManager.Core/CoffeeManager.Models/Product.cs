namespace CoffeeManager.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductType { get; set; }
        public decimal Price { get; set; }
        public decimal PolicePrice { get; set; }
        public int CupType { get; set; }
        public int CoffeeRoomNo { get; set; }
        public int? SuplyId { get; set; }
    }
}
