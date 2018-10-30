using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.AutoOrder;
using CoffeManager.Common.Managers;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.AutoOrder
{
    public class AddAutoOrderViewModel : FeedViewModel<SuplyProductToOrderItemViewModel>, IMvxViewModelResult<bool>
    {
        private DayOfWeek dayOfWeek;
        private int orderTime;
        
        private readonly IAutoOrderManager manager;
        public ICommand AddSuplyProductsCommand { get; }
        
        public ICommand SaveAutoOrderCommand { get; }

        public List<DayOfWeek> DaysOfWeek { get; set; }

        public DayOfWeek DayOfWeek
        {
            get => dayOfWeek;
            set => SetProperty(ref dayOfWeek, value);
        }

        public List<int> Hours { get; set; }

        public int OrderTime
        {
            get => orderTime;
            set => SetProperty(ref orderTime, value);
        }

        public AddAutoOrderViewModel(IAutoOrderManager manager)
        {
            this.manager = manager;
            AddSuplyProductsCommand = new MvxAsyncCommand(DoAddSuplyProducts);
            SaveAutoOrderCommand = new MvxAsyncCommand(DoSaveAutoOrder, () => ItemsCollection.Count > 0);
        }

        public override Task Initialize()
        {
            var values = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();
            DaysOfWeek = values.ToList();
            RaisePropertyChanged(nameof(DaysOfWeek));

            Hours = Enumerable.Range(0, 23).ToList();
            RaisePropertyChanged(nameof(Hours));

            return base.Initialize();
        }

        private async Task DoSaveAutoOrder()
        {
            var order = new AutoOrderDTO();
            order.DayOfWeek = dayOfWeek;
            order.OrderTime = TimeSpan.FromHours(orderTime);
            order.IsActive = true;
            order.OrderItems = ItemsCollection.Select(MapItem).ToList();

            await ExecuteSafe(manager.AddAutoOrderItem(order));
            await NavigationService.Close(this, true);
        }

        private SuplyProductToOrderItemDTO MapItem(SuplyProductToOrderItemViewModel vm)
        {
            return new SuplyProductToOrderItemDTO()
            {
                SuplyProductId = vm.SuplyProductId,
                QuantityShouldBeAfterOrder = vm.QuantityShouldBeAfterOrder
            };
        }

        private async Task DoAddSuplyProducts()
        {
            var suplyProducts = await NavigationService.Navigate<SelectSuplyProductsForAutoOrderViewModel, IEnumerable<SupliedProduct>>();
            ItemsCollection.AddRange(suplyProducts.Select(s => new SuplyProductToOrderItemViewModel(s.Id, s.Name)));
        }

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }
    }
}