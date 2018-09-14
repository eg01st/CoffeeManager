namespace CoffeeManager.Models.Data.DTO.StaffMotivation
{
    public class UserMotivationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MotivationId { get; set; }
        public string UserName { get; set; }
        public decimal ShiftScore { get; set; }
        public decimal MoneyScore { get; set; }
        public decimal OtherScore { get; set; }
        public decimal EntireScore => ShiftScore + MoneyScore + OtherScore;
    }
}