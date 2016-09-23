namespace CoffeeManager.Models
{
    public class Entity
    {
        public int CofferRoomNo { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public Entity()
        {
            
        }

        public Entity(int coffeeRoomNo)
        {
            CofferRoomNo = coffeeRoomNo;
        }
    }
}
