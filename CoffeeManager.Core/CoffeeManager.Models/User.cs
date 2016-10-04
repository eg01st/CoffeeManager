namespace CoffeeManager.Models
{
    public class User
    {
        public Shift[] Shifts { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int CoffeeRoomNo { get; set; }
    }
}
