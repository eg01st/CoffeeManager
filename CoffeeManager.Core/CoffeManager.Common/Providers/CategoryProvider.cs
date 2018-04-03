using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.Category;

namespace CoffeManager.Common.Providers
{
    public class CategoryProvider : BaseServiceProvider, ICategoryProvider
    {
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            return new[]
            {
                new CategoryDTO()
                {
                    Id = 1,
                    Name = "Кофе",
                },
                new CategoryDTO() {Id = 2, Name = "Чай"},
                new CategoryDTO() {Id = 3, Name = "Холодные напитки"},
                new CategoryDTO() {Id = 4, Name = "Мороженое"},
                new CategoryDTO() {Id = 5, Name = "Еда"},
                new CategoryDTO() {Id = 6, Name = "Вода"},
                new CategoryDTO() {Id = 7, Name = "Сладости"},
                new CategoryDTO() {Id = 8, Name = "Добавки"},
                new CategoryDTO() {Id = 1, Name = "Lavaza", ParentId = 1} , 
                new CategoryDTO() {Id = 2, Name = "Арабика", ParentId = 1} , 
            };
        }

        public async Task<CategoryDTO> GetCategory(int categoryId)
        {
            return await Get<CategoryDTO>(RoutesConstants.GetCategory, new Dictionary<string, string>()
            {
                {nameof(categoryId), categoryId.ToString()}
            });
        }

        public async Task<int> AddCategory(CategoryDTO category)
        {
            return await Put<int, CategoryDTO>(RoutesConstants.AddCategory, category);
        }

        public async Task UpdateCategory(CategoryDTO category)
        {
            await Post(RoutesConstants.UpdateCategory, category);
        }

        public async Task DeleteCategory(int categoryId)
        {
            await Delete(RoutesConstants.DeleteCategory, new Dictionary<string, string>()
            {
                {nameof(categoryId), categoryId.ToString()}
            });
        }
    }
}