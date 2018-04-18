using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Messages;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using MobileCore.Collections;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels.Categories
{
    public class CategoriesViewModel : FeedViewModel<CategoryItemViewModel>
    {
        private Entity currentCoffeeRoom;
        private List<Entity> coffeeRooms;

        private MvxSubscriptionToken refreshToken;
        private readonly ICategoryManager categoryManager;

        public ICommand AddCategoryCommand { get; }

        public Entity CurrentCoffeeRoom
        {
            get { return currentCoffeeRoom; }
            set
            {
                bool isInitialSelect = currentCoffeeRoom == null;
                currentCoffeeRoom = value;
                Config.CoffeeRoomNo = currentCoffeeRoom.Id;
                if (!isInitialSelect)
                {
                    MvxMessenger.Publish(new CoffeeRoomChangedMessage(this));
                    MvxMessenger.Publish(new CategoriesUpdatedMessage(this));
                }
                RaisePropertyChanged(nameof(CurrentCoffeeRoom));
                RaisePropertyChanged(nameof(CurrentCoffeeRoomName));
              
            }
        }

        public List<Entity> CoffeeRooms
        {
            get { return coffeeRooms; }
            set
            {
                coffeeRooms = value;
                RaisePropertyChanged(nameof(CoffeeRooms));
            }
        }

        public string CurrentCoffeeRoomName
        {
            get { return CurrentCoffeeRoom.Name; }

        }

        private readonly IAdminManager adminManager;

        public CategoriesViewModel(ICategoryManager categoryManager, IAdminManager adminManager)
        {
            this.adminManager = adminManager;
            this.categoryManager = categoryManager;
            AddCategoryCommand = new MvxAsyncCommand(DoAddCategory);
        }

        private async Task InitCoffeeRooms()
        {
            if (CoffeeRooms != null)
            {
                return;
            }
            await ExecuteSafe(async () =>
            {
                var items = await adminManager.GetCoffeeRooms();
                CoffeeRooms = items.ToList();
                CurrentCoffeeRoom = CoffeeRooms.First(c => c.Id == Config.CoffeeRoomNo);
            });
        }

        private async Task DoAddCategory()
        {
            await NavigationService.Navigate<AddCategoryViewModel>();
        }

        protected override async Task<PageContainer<CategoryItemViewModel>> GetPageAsync(int skip)
        {
            await InitCoffeeRooms();
            
            ItemsCollection.Clear();
            var categories = await ExecuteSafe(categoryManager.GetCategoriesPlain);
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