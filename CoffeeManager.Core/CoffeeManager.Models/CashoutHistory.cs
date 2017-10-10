namespace CoffeeManager.Models
{
    public class CashoutHistory
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime Date { get; set; }
        public int ShiftId { get; set; }
        public int CoffeeRoomNo { get; set; }
    }
}
