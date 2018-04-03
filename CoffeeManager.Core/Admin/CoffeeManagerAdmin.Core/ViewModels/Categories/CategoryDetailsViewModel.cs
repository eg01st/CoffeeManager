using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
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

        private string name;

        public MvxObservableCollection<SubCategoryItemViewModel> AllCategories { get; set; }
        public MvxObservableCollection<SubCategoryItemViewModel> SubCategories { get; set; }
        
        public ICommand AddSubCategoryCommand { get; }
        
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public CategoryDetailsViewModel(ICategoryManager categoryManager)
        {
            this.categoryManager = categoryManager;
            
            AddSubCategoryCommand = new MvxAsyncCommand(DoAddSubCategory);
        }

        private async Task DoAddSubCategory()
        {
            var subs = new List<ActionSheetOption>();
            foreach (var cat in AllCategories)
            {
                subs.Add(new ActionSheetOption(cat.Name, () => SubCategories.Add(cat)));
            }

            UserDialogs.ActionSheet(new ActionSheetConfig()
            {
                Title = "Добавить подкатегорию",
                Options = subs
            });
        }

        protected override async Task DoLoadDataImplAsync()
        {
            var category = await categoryManager.GetCategory(categoryId);
            var allCategories = await categoryManager.GetCategories();
            AllCategories = new MvxObservableCollection<SubCategoryItemViewModel>(allCategories
                .Select(s => new SubCategoryItemViewModel(s)).ToList());
            SubCategories = new MvxObservableCollection<SubCategoryItemViewModel>(category.SubCategories
                .Select(s => new SubCategoryItemViewModel(s)));
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

        private void DeleteCategory(SubCategoryDeleteMessage obj)
        {
            var item = obj.Sender as SubCategoryItemViewModel;
            SubCategories.Remove(item);
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
    }
}