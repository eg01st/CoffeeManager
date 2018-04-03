using System.Collections.Generic;
using System.Linq;
using CoffeeManager.Models.Data.DTO.Category;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Categories
{
    public class CategoryItemViewModel : FeedItemElementViewModel
    {
        public CategoryItemViewModel(CategoryDTO categoryDto)
        {
            Id = categoryDto.Id;
            Name = categoryDto.Name;
            ParentId = categoryDto.ParentId;
            SubCategories = categoryDto?.SubCategories?.Select(s => new CategoryItemViewModel(s)).ToList();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public List<CategoryItemViewModel> SubCategories { get; set; }

        protected override async void Select()
        {
            await NavigationService.Navigate<CategoryDetailsViewModel, int>(Id);
        }
    }
}