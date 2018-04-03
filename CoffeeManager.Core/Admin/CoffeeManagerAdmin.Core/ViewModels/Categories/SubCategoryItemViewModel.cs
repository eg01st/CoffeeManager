using System.Windows.Input;
using CoffeeManager.Models.Data.DTO.Category;
using CoffeeManagerAdmin.Core.Messages;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Categories
{
    public class SubCategoryItemViewModel : CategoryItemViewModel
    {
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        
        public SubCategoryItemViewModel(CategoryDTO categoryDto) : base(categoryDto)
        {
            AddCommand = new MvxCommand(DoAdd);
            DeleteCommand = new MvxCommand(DoDelete);
        }

        protected override async void Select()
        {
            if (await UserDialogs.ConfirmAsync($"Не использовать {Name} как подкатегорию?"))
            {
                DeleteCommand.Execute(null);
            }
        }

        private void DoDelete()
        {
            MvxMessenger.Publish(new SubCategoryDeleteMessage(this));
        }

        private void DoAdd()
        {
            MvxMessenger.Publish(new SubCategoryAddMessage(this));
        }
    }
}