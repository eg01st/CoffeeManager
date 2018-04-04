using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManagerAdmin.Core.Messages;
using CoffeManager.Common.Managers;
using MobileCore.Collections;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.Categories
{
    public class CategoriesViewModel : FeedViewModel<CategoryItemViewModel>
    {
        private MvxSubscriptionToken refreshToken;
        private readonly ICategoryManager categoryManager;

        public ICommand AddCategoryCommand { get; }

        public CategoriesViewModel(ICategoryManager categoryManager)
        {
            this.categoryManager = categoryManager;
            AddCategoryCommand = new MvxAsyncCommand(DoAddCategory);
        }

        private async Task DoAddCategory()
        {
            await NavigationService.Navigate<AddCategoryViewModel>();
        }

        protected override async Task<PageContainer<CategoryItemViewModel>> GetPageAsync(int skip)
        {
            var categories = await ExecuteSafe(categoryManager.GetCategories);
            return categories.Select(s => new CategoryItemViewModel(s)).ToPageContainer();
        }

        public async Task Init()
        {
            await DoLoadDataImplAsync();
        }

        protected override void DoSubscribe()
        {
            base.DoSubscribe();
            refreshToken = MvxMessenger.Subscribe<CategoriesUpdatedMessage>(async (sender) => await RefreshDataAsync());
        }

        protected override void DoUnsubscribe()
        {
            base.DoUnsubscribe();
            MvxMessenger.Unsubscribe<CategoriesUpdatedMessage>(refreshToken);
        }
    }
}