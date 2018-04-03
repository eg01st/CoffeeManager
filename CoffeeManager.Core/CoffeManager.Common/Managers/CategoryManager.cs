using System.Collections.Generic;
using System.Linq;
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
            var result = await categoryProvider.GetCategories();
            var categories = result.ToList();
            var subCategories = categories.Where(c => c.ParentId.HasValue).GroupBy(g => g.ParentId);
            foreach (var subCategory in subCategories)
            {
                var parentCategory = categories.First(c => c.Id == subCategory.Key);
                parentCategory.SubCategories = subCategory.ToArray();
                foreach (var s in subCategory)
                {
                    categories.Remove(s);
                }
            }

            return categories;
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