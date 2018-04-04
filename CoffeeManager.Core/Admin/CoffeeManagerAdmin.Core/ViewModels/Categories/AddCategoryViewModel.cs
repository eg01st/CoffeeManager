using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Common;
using CoffeeManager.Models.Data.DTO.Category;
using CoffeeManagerAdmin.Core.Messages;
using CoffeManager.Common.Managers;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Categories
{
    public class AddCategoryViewModel : PageViewModel
    {
        private string categoryName;
        private readonly ICategoryManager categoryManager;

        public string CategoryName
        {
            get => categoryName;
            set
            {
                categoryName = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(AddCategoryCommand));
            }
        }
        
        public ICommand AddCategoryCommand { get; }
        
        public AddCategoryViewModel(ICategoryManager categoryManager)
        {
            this.categoryManager = categoryManager;
            AddCategoryCommand = new MvxAsyncCommand(DoAddCategory, CanAddCategory);
        }

        private bool CanAddCategory()
        {
            return !string.IsNullOrWhiteSpace(CategoryName);
        }

        private async Task DoAddCategory()
        {
            var dto = new CategoryDTO();
            dto.Name = CategoryName;
            dto.CoffeeRoomNo = Config.CoffeeRoomNo;
            var categoryId = await categoryManager.AddCategory(dto);
            MvxMessenger.Publish(new CategoriesUpdatedMessage(this));
            CloseCommand.Execute(null);
            await NavigationService.Navigate<CategoryDetailsViewModel, int>(categoryId);
        }
    }
}