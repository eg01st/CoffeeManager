namespace CoffeeManager.Models.Data.DTO.Category
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int? ParentId { get; set; }
        
        public CategoryDTO[] SubCategories { get; set; }
    }
}