﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.Category;

namespace CoffeManager.Common.Providers
{
    public interface ICategoryProvider
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();

        Task<CategoryDTO> GetCategory(int id);

        Task<int> AddCategory(CategoryDTO dto);

        Task UpdateCategory(CategoryDTO dto);

        Task DeleteCategory(int id);
        
        Task ToggleIsActiveCategory(int id);
    }
}