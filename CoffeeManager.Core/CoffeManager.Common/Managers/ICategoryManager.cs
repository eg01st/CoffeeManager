using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.Category;

namespace CoffeManager.Common.Managers
{
    public interface ICategoryManager
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();

        Task<CategoryDTO> GetCategory(int id);

        Task AddCategory(CategoryDTO dto);

        Task UpdateCategory(CategoryDTO dto);

        Task DeleteCategory(int id);
    }
}