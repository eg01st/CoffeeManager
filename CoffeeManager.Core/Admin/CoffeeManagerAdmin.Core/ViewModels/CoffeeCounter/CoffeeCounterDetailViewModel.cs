using System.Linq;
using System.Threading.Tasks;
using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;
using CoffeeManagerAdmin.Core.Messages;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.CoffeeCounter
{
    public class CoffeeCounterDetailViewModel : AddCoffeeCounterViewModel, IMvxViewModel<int>
    {
        private int counterId;
        private int coffeeRoomId;
        
        public CoffeeCounterDetailViewModel(ICoffeeCounterManager counterManager, ISuplyProductsManager suplyProductsManager, ICategoryManager categoryManager) : base(counterManager, suplyProductsManager, categoryManager)
        {
        }
        
        protected override async Task DoLoadDataImplAsync()
        {
            await base.DoLoadDataImplAsync();
            await ExecuteSafe(async () =>
            {
                var countedDto = await counterManager.GetCounter(counterId);
                CounterName = countedDto.Name;
                SuplyProductName = suplyProducts.First(s => s.Id == countedDto.SuplyProductId).Name;
                suplyProductId = countedDto.SuplyProductId;
                CategoryName = categories.First(s => s.Id == countedDto.CategoryId).Name;
                categoryId = countedDto.CategoryId;

                coffeeRoomId = countedDto.CoffeeRoomNo;
            });
        }

        protected override async Task DoAddCounter()
        {
            var dto = new CoffeeCounterForCoffeeRoomDTO();
            dto.Id = counterId;
            dto.Name = CounterName;
            dto.CoffeeRoomNo = coffeeRoomId;
            dto.CategoryId = categoryId;
            dto.SuplyProductId = suplyProductId;
            await ExecuteSafe(async () =>
            {
                await counterManager.UpdateCounter(dto);
                MvxMessenger.Publish(new CoffeeCountersUpdateMessage(this));
                CloseCommand.Execute(null);
            });
        }

        public void Prepare(int parameter)
        {
            counterId = parameter;
        }
    }
}