using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.AutoOrder;
using CoffeeManagerAdmin.Core.ViewModels.Abstract;
using CoffeManager.Common.Managers;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;
using System.Runtime.InteropServices;

namespace CoffeeManagerAdmin.Core.ViewModels.AutoOrder
{
    public class AddAutoOrderViewModel : AdminCoffeeRoomFeedViewModel<SuplyProductToOrderItemViewModel>, IMvxViewModelResult<bool>
    {
        private DayOfWeek dayOfWeek;
        private int orderTime;
        private string emailToSend;
        private string cCToSend;
        private string senderEmail;
        private string senderEmailPassword;
        private string subject;

        private readonly IAutoOrderManager manager;
        public ICommand AddSuplyProductsCommand { get; }
        
        public IMvxAsyncCommand SaveAutoOrderCommand { get; }

        public string EmailToSend
        {
            get => emailToSend;
            set => SetProperty(ref emailToSend, value);
        }

        public string CCToSend
        {
            get => cCToSend;
            set => SetProperty(ref cCToSend, value);
        }

        public string SenderEmail
        {
            get => senderEmail;
            set => SetProperty(ref senderEmail, value);
        }

        public string SenderEmailPassword
        {
            get => senderEmailPassword;
            set => SetProperty(ref senderEmailPassword, value);
        }

        public string Subject
        {
            get => subject;
            set => SetProperty(ref subject, value);
        }

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

        protected override async Task DoClose()
        {
            await NavigationService.Close(this, false);
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
            order.CoffeeRoomId = CurrentCoffeeRoom.CoffeeRoomNo;

            order.CCToSend = CCToSend;
            order.Subject = Subject;
            order.EmailToSend = EmailToSend;
            order.SenderEmail = SenderEmail;
            order.SenderEmailPassword = SenderEmailPassword;

            var id = await ExecuteSafe(async () => await manager.AddAutoOrderItem(order));
            await NavigationService.Close(this, true);
        }

        private SuplyProductToOrderItemDTO MapItem(SuplyProductToOrderItemViewModel vm)
        {
            return new SuplyProductToOrderItemDTO()
            {
                SuplyProductId = vm.SuplyProductId,
                QuantityShouldBeAfterOrder = vm.QuantityShouldBeAfterOrder,
                ShouldUpdateQuantityBeforeOrder = vm.ShouldUpdateQuantityBeforeOrder
            };
        }

        private async Task DoAddSuplyProducts()
        {
            var suplyProducts = await NavigationService.Navigate<SelectSuplyProductsForAutoOrderViewModel, IEnumerable<SupliedProduct>>();
            ItemsCollection.AddRange(suplyProducts.Select(s => new SuplyProductToOrderItemViewModel(s.Id, s.Name)));
            SaveAutoOrderCommand.RaiseCanExecuteChanged();
        }

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }
    }
}