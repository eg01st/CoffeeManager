using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.Category;
using CoffeManager.Common.Providers;

namespace CoffeManager.Common.Managers
{
    public class CategoryManager : BaseManager, ICategoryManager
    {
        private readonly ICategoryProvider categoryProvider;

        public CategoryManager(ICategoryProvider categoryProvider)
        {
            this.categoryProvider = categoryProvider;
        }
        
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            return await categoryProvider.GetCategories();
        }

        public async Task<CategoryDTO> GetCategory(int id)
        {
            return await categoryProvider.GetCategory(id);
        }

        public async Task AddCategory(CategoryDTO dto)
        {
            await categoryProvider.AddCategory(dto);
        }

        public async Task UpdateCategory(CategoryDTO dto)
        {
            await categoryProvider.UpdateCategory(dto);
        }

        public async Task DeleteCategory(int id)
        {
            await categoryProvider.DeleteCategory(id);
        }
    }
}