using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;
using CoffeManager.Common.Managers;
using MobileCore.Collections;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels.CoffeeCounter
{
    public class CoffeeCounterViewModel : FeedViewModel<CoffeeCounterItemViewModel>, IMvxViewModel<int>
    {
        private int userId;
        
        private readonly ICoffeeCounterManager coffeeCounterManager;
        private readonly IShiftManager shiftManager;

        public ICommand OpenShiftCommand { get; }
        
        public CoffeeCounterViewModel(ICoffeeCounterManager coffeeCounterManager,
            IShiftManager shiftManager)
        {
            this.coffeeCounterManager = coffeeCounterManager;
            this.shiftManager = shiftManager;

            OpenShiftCommand = new MvxAsyncCommand(DoOpenShift);
        }

        private async Task DoOpenShift()
        {
            if (ItemsCollection.Any(i => i.Counter != i.Confirm))
            {
                UserDialogs.Alert("Показания счетчиков не сходятся! Перепроверьте показания");
                return;
            }

            var counters = new List<CoffeeCounterDTO>();
            foreach (var counter in ItemsCollection)
            {
                counters.Add(new CoffeeCounterDTO()
                {
                    Id = counter.Id,
                    StartCounter = counter.Counter,
                    SuplyProductId = counter.SuplyProductId
                });
            }

            await ExecuteSafe(async () =>
            {
                var shiftId = await shiftManager.StartUserShift(userId, counters);
                await NavigationService.Navigate<MainViewModel, Shift>(new Shift {  UserId = userId, Id = shiftId });
            });
        }

        protected override async Task<PageContainer<CoffeeCounterItemViewModel>> GetPageAsync(int skip)
        {
            var items = await coffeeCounterManager.GetCounters();
            return items.Select(s => new CoffeeCounterItemViewModel(s)).ToPageContainer();
        }

        public void Prepare(int parameter)
        {
            userId = parameter;
        }
    }
}