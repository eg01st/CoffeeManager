using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.Category;

namespace CoffeManager.Common.Providers
{
    public class CategoryProvider : BaseServiceProvider, ICategoryProvider
    {
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            return new[]
            {
                new CategoryDTO() {Id = 1, Name = "Кофе"},
                new CategoryDTO() {Id = 2, Name = "Чай"},
                new CategoryDTO() {Id = 3, Name = "Холодные напитки"},
                new CategoryDTO() {Id = 4, Name = "Мороженое"},
                new CategoryDTO() {Id = 5, Name = "Еда"},
                new CategoryDTO() {Id = 6, Name = "Вода"},
                new CategoryDTO() {Id = 7, Name = "Сладости"},
                new CategoryDTO() {Id = 8, Name = "Добавки"},
            };
        }

        public async Task<CategoryDTO> GetCategory(int id)
        {
            return new CategoryDTO();
        }

        public async Task AddCategory(CategoryDTO dto)
        {

        }

        public async Task UpdateCategory(CategoryDTO dto)
        {

        }

        public async Task DeleteCategory(int id)
        {

        }
    }
}