using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Messages;
using CoffeeManagerAdmin.Core.ViewModels.Abstract;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using MobileCore.Collections;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.Categories
{
    public class CategoriesViewModel : AdminCoffeeRoomFeedViewModel<CategoryItemViewModel>
    {
        private MvxSubscriptionToken refreshToken;
        private readonly ICategoryManager categoryManager;

        public override bool ShouldReloadOnCoffeeRoomChange => true;
        
        public ICommand AddCategoryCommand { get; }

        public override Entity CurrentCoffeeRoom
        {
            get => base.CurrentCoffeeRoom;
            set
            {
                base.CurrentCoffeeRoom = value;
                MvxMessenger.Publish(new CategoriesUpdatedMessage(this));
            }
        }

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
            var categories = await ExecuteSafe(categoryManager.GetCategoriesPlain);
            return categories.Select(s => new CategoryItemViewModel(s)).ToPageContainer();
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