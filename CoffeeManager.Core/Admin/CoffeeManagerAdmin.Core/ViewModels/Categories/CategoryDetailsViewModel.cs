using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Common;
using CoffeeManager.Models.Data.DTO.Category;
using CoffeeManagerAdmin.Core.Messages;
using CoffeManager.Common.Managers;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.Categories
{
    public class CategoryDetailsViewModel : PageViewModel, IMvxViewModel<int>
    {
        private MvxSubscriptionToken addToken;
        private MvxSubscriptionToken deleteToken;
        
        private readonly ICategoryManager categoryManager;
        private int categoryId;

        private List<CategoryDTO> allCategories;

        private string name;

        public MvxObservableCollection<SubCategoryItemViewModel> AllCategories { get; set; } 
            = new MvxObservableCollection<SubCategoryItemViewModel>();
        public MvxObservableCollection<SubCategoryItemViewModel> SubCategories { get; set; } 
            = new MvxObservableCollection<SubCategoryItemViewModel>();
        
        public ICommand AddSubCategoryCommand { get; }
        public ICommand SaveChangesCommand { get; }
        
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public CategoryDetailsViewModel(ICategoryManager categoryManager)
        {
            this.categoryManager = categoryManager;
            
            AddSubCategoryCommand = new MvxCommand(DoAddSubCategory);
            SaveChangesCommand = new MvxAsyncCommand(DoSaveChanges);
        }

        private async Task DoSaveChanges()
        {
            var dto = new CategoryDTO();
            dto.Name = Name;
            dto.Id = categoryId;
            
            var subCategories = new List<CategoryDTO>();
            foreach (var subCategory in SubCategories)
            {
                subCategories.Add(new CategoryDTO() {Id = subCategory.Id, ParentId = categoryId} );
            }

            dto.SubCategories = subCategories.ToArray();
            dto.CoffeeRoomNo = Config.CoffeeRoomNo;
            await categoryManager.UpdateCategory(dto);
            MvxMessenger.Publish(new CategoriesUpdatedMessage(this));
            CloseCommand.Execute(null);
        }

        private void DoAddSubCategory()
        {
            var subs = new List<ActionSheetOption>();
            foreach (var cat in AllCategories)
            {
                subs.Add(new ActionSheetOption(cat.Name, () =>
                {
                    SubCategories.Add(cat);
                    RefreshCategories();
                }));
            }

            UserDialogs.ActionSheet(new ActionSheetConfig()
            {
                Title = "Добавить подкатегорию",
                Options = subs,
                Cancel = new ActionSheetOption("Отмена")
            });
        }

        protected override async Task DoLoadDataImplAsync()
        {
            var category = await categoryManager.GetCategory(categoryId);

            Name = category.Name;
            if (category.SubCategories != null)
            {
                SubCategories.AddRange(category.SubCategories?
                    .Select(s => new SubCategoryItemViewModel(s)));
            }
   
            var categories = await categoryManager.GetCategories();
            allCategories = categories.ToList();
            
            RefreshCategories();
        }
        
        private void DeleteCategory(SubCategoryDeleteMessage obj)
        {
            var item = obj.Sender as SubCategoryItemViewModel;
            SubCategories.Remove(item);
            RefreshCategories();
        }

        private void AddCategory(SubCategoryAddMessage subCategoryAddMessage)
        {
            var item = subCategoryAddMessage.Sender as SubCategoryItemViewModel;
            SubCategories.Add(item);
        }

        public void Prepare(int categoryId)
        {
            this.categoryId = categoryId;
        }

        private void RefreshCategories()
        {
            AllCategories.Clear();
            AllCategories.AddRange(allCategories
                .Where(c => c.Id != categoryId && !SubCategories.Any(s => s.Id == c.Id))
                .Select(s => new SubCategoryItemViewModel(s))
                .ToList());
        }
        
        protected override void DoSubscribe()
        {
            base.DoSubscribe();
            addToken = MvxMessenger.Subscribe<SubCategoryAddMessage>(AddCategory);
            deleteToken = MvxMessenger.Subscribe<SubCategoryDeleteMessage>(DeleteCategory);
        }

        protected override void DoUnsubscribe()
        {
            base.DoUnsubscribe();
            MvxMessenger.Unsubscribe<SubCategoryAddMessage>(addToken);
            MvxMessenger.Unsubscribe<SubCategoryDeleteMessage>(deleteToken);
        }

    }
}