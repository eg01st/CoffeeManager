namespace CoffeeManager.Models
{
    public class Entity
    {
        public int CoffeeRoomNo { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public Entity()
        {

        }

        public Entity(int coffeeRoomNo)
        {
            CoffeeRoomNo = coffeeRoomNo;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
