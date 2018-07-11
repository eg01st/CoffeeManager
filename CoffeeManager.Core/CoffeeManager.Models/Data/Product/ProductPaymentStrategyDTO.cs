namespace CoffeeManager.Models.Data.Product
{
    public class ProductPaymentStrategyDTO
    {
        public int Id { get; set; }
        public int CoffeeRoomId { get; set; }
        public int ProductId { get; set; }
        public decimal DayShiftPercent { get; set; }
        public decimal NightShiftPercent { get; set; }
    }
}