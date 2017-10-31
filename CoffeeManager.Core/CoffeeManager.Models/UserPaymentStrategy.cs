
namespace CoffeeManager.Models
{
    public class UserPaymentStrategy
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CoffeeRoomId { get; set; }
        public decimal DayShiftPersent { get; set; }
        public decimal NightShiftPercent { get; set; }
        public decimal SimplePayment { get; set; }
        public decimal MinimumPayment { get; set; }
    }
}
