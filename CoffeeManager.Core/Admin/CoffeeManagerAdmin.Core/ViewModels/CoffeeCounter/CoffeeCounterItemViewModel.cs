using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Common;
using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;
using MobileCore.ViewModels;
using CoffeManager.Common.Managers;
using MvvmCross.Platform;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.CoffeeCounter
{
    public class CoffeeCounterItemViewModel : FeedItemElementViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICommand ToggleIsActiveCommand { get; }

        public CoffeeCounterItemViewModel()
        {
            ToggleIsActiveCommand = new MvxAsyncCommand(DoToggleIsActive);
        }

        private async Task DoToggleIsActive()
        {
            var manager = Mvx.Resolve<ICoffeeCounterManager>();
            await manager.ToggleIsActiveCounter(Id);
        }

        public CoffeeCounterItemViewModel(CoffeeCounterForCoffeeRoomDTO dto) : this()
        {
            Id = dto.Id;
            Name = dto.Name;
            IsActive = dto.IsActive.FirstOrDefault(a => a.CoffeeRoomNo == Config.CoffeeRoomNo)?.IsActive ?? false;
            RaiseAllPropertiesChanged();
        }
        
        protected override async void Select()
        {
            await NavigationService.Navigate<CoffeeCounterDetailViewModel, int>(Id);

        }
    }
}