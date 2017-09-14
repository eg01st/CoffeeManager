namespace CoffeeManager.Models
{
    public class UserEarningsHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShiftId { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime Date { get; set; }
        public bool IsDayShift { get; set; }
    }
}
