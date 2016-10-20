namespace CoffeeManager.Models
{
    public class UsedCupPerShift
    {
        public int Id { get; set; }
        public int ShiftId { get; set; }
        public int C110 { get; set; }
        public int C170 { get; set; }
        public int C250 { get; set; }
        public int C400 { get; set; }
        public int Plastic { get; set; }
        public int CoffeeRoomNo { get; set; }
    }
}
