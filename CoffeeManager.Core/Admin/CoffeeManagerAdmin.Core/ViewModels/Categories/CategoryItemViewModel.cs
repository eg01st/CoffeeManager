using System.Collections.Generic;
using System.Linq;
using CoffeeManager.Models.Data.DTO.Category;
using MobileCore.ViewModels;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using System;
using System.Threading.Tasks;
using MvvmCross.Platform;
using CoffeManager.Common.Managers;
using CoffeeManagerAdmin.Core.Messages;
using CoffeeManager.Common;

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
            IsActive = categoryDto.IsActive.FirstOrDefault(a => a.CoffeeRoomNo == Config.CoffeeRoomNo)?.IsActive ?? false;
            DeleteCategoryCommand = new MvxAsyncCommand(DoDeletecategory);
            ToggleIsActiveCommand = new MvxAsyncCommand(DoToggleIsActive);
        }

        private async Task DoDeletecategory()
        {
            if (await UserDialogs.ConfirmAsync($"Полностью удалить категорию {Name} из системы?"))
            {
                await ExecuteSafe(async () =>
                {
                    var manager = Mvx.Resolve<ICategoryManager>();
                    await manager.DeleteCategory(Id);
                    MvxMessenger.Publish(new CategoriesUpdatedMessage(this));
                });
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public List<CategoryItemViewModel> SubCategories { get; set; }

        public bool IsActive { get; set; }

        public ICommand DeleteCategoryCommand { get; }
        public ICommand ToggleIsActiveCommand { get; }

        protected override async void Select()
        {
            await NavigationService.Navigate<CategoryDetailsViewModel, int>(Id);
        }

        private async Task DoToggleIsActive()
        {
            var manager = Mvx.Resolve<ICategoryManager>();
            await manager.ToggleIsActiveCategory(Id);
            MvxMessenger.Publish(new CoffeeCountersUpdateMessage(this));
        }

        public override string ToString()
        {
            return Name;
        }

    }
}