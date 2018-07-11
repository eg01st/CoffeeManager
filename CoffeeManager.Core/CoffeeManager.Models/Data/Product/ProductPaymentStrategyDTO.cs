namespace CoffeeManager.Models.Data.Product
{
    public class ProductPaymentStrategyDTO
    {
        public int Id { get; set; }
        public int CoffeeRoomId { get; set; }
        public decimal DayShiftPersent { get; set; }
        public decimal NightShiftPercent { get; set; }
    }
}