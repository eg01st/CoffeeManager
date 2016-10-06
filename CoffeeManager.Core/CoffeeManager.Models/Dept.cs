namespace CoffeeManager.Models
{
    public class Dept : Entity
    {
        public decimal Amount { get; set; }
        public int ShiftId { get; set; }
        public bool IsPaid { get; set; }
    }
}
