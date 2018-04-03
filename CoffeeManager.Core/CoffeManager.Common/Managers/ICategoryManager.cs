using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.Category;

namespace CoffeManager.Common.Managers
{
    public interface ICategoryManager
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
        
        Task<IEnumerable<CategoryDTO>> GetCategoriesPlain();

        Task<CategoryDTO> GetCategory(int id);

        Task<int> AddCategory(CategoryDTO dto);

        Task UpdateCategory(CategoryDTO dto);

        Task DeleteCategory(int id);
    }
}