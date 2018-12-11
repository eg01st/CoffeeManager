using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models.Data.DTO.AutoOrder;
using CoffeManager.Common.Managers;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace CoffeeManagerAdmin.Core.ViewModels.AutoOrder
{
    public class AutoOrderItemViewModel : FeedItemElementViewModel
    {
        private readonly AutoOrderDTO autoOrderDTO;
        private bool isActive;
        
        public AutoOrderItemViewModel(AutoOrderDTO autoOrderDTO)
        {
            this.autoOrderDTO = autoOrderDTO;
            IsActive = autoOrderDTO.IsActive;
            ToggleOrderEnabledCommand = new MvxAsyncCommand(DoToggleOrderEnabled);
        }

        private async Task DoToggleOrderEnabled()
        {
            var manager = Mvx.Resolve<IAutoOrderManager>();
            await ExecuteSafe(manager.ToggleOrderEnabled(autoOrderDTO.Id));
        }

        public ICommand ToggleOrderEnabledCommand { get; }

        public int Id => autoOrderDTO.Id;
        public DayOfWeek DayOfWeek => autoOrderDTO.DayOfWeek;
        public TimeSpan OrderTime => autoOrderDTO.OrderTime;

        public bool IsActive
        {
            get => isActive;
            set => SetProperty(ref isActive, value);
        }
    }
}