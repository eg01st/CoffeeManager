using System.Linq;
using System.Threading.Tasks;
using CoffeManager.Common.Managers;
using MobileCore.Collections;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Shifts
{
    public class ShiftCountersViewModel : FeedViewModel<ShiftCounterItemViewModel>, IMvxViewModel<int>
    {
        private readonly ICoffeeCounterManager coffeeCounterManager;
        private int shiftId;

        public ShiftCountersViewModel(ICoffeeCounterManager coffeeCounterManager)
        {
            this.coffeeCounterManager = coffeeCounterManager;
        }
        
        public void Prepare(int parameter)
        {
            shiftId = parameter;
        }

        protected override async Task<PageContainer<ShiftCounterItemViewModel>> GetPageAsync(int skip)
        {
            return await ExecuteSafe(async () =>
            {
                var counters = await coffeeCounterManager.GetCountersForShift(shiftId);
                return counters.Select(s => new ShiftCounterItemViewModel(s)).ToPageContainer();
            });
        }
    }
}