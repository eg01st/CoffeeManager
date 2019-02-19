using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models.Data.DTO.AutoOrder;
using CoffeManager.Common.Managers;
using MobileCore.Collections;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.AutoOrder
{
    public class AutoOrderDetailsViewModel : FeedViewModel<SuplyProductToOrderItemViewModel> , IMvxViewModel<int, bool>
    {
        private DayOfWeek dayOfWeek;
        private int orderTime;
        private string emailToSend;
        private string cCToSend;
        private string senderEmail;
        private string senderEmailPassword;
        private string subject;
        
        private readonly IAutoOrderManager manager;
        private int autoOrderId;
        
        public ICommand UpdateOrderCommand { get; }
        public ICommand DeleteOrderCommand { get; }
        public bool IsActive { get; set; }

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
            
        public AutoOrderDetailsViewModel(IAutoOrderManager manager)
        {
            this.manager = manager;
            DeleteOrderCommand = new MvxAsyncCommand(DoDeleteOrder);
            UpdateOrderCommand = new MvxAsyncCommand(DoUpdateOrder);
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


        private async Task DoUpdateOrder()
        {
             var dto = new AutoOrderDTO();
            dto.CCToSend = CCToSend;
            dto.DayOfWeek = DayOfWeek;
            dto.Subject = Subject;
            dto.EmailToSend = dto.EmailToSend;
            dto.OrderTime = dto.OrderTime;
            dto.SenderEmail = dto.SenderEmail;
            dto.SenderEmailPassword = dto.SenderEmailPassword;

            dto.OrderItems = ItemsCollection.Select(MapDto).ToList();

            await ExecuteSafe(manager.UpdateAutoOrderItem(dto));
            await NavigationService.Close(this, true);
        }

        protected override async Task DoClose()
        {
            await NavigationService.Close(this, false);
        }

        private async Task DoDeleteOrder()
        {
            Confirm("Удалить автозаказ?", async () =>
             {
                 await ExecuteSafe(manager.DeleteAutoOrderItem(autoOrderId));
                 await NavigationService.Close(this, true);
             });
        }

        public void Prepare(int parameter)
        {
            autoOrderId = parameter;
        }

        protected override async Task<PageContainer<SuplyProductToOrderItemViewModel>> GetPageAsync(int skip)
        {
            var order = await ExecuteSafe(async () => await manager.GetAutoOrderDetails(autoOrderId));
            DayOfWeek = order.DayOfWeek;
            OrderTime = order.OrderTime.Hours;
            IsActive = order.IsActive;
            
            return order.OrderItems.Select(MapItem).ToPageContainer();
        }

        private SuplyProductToOrderItemViewModel MapItem(SuplyProductToOrderItemDTO dto)
        {
            return new SuplyProductToOrderItemViewModel(dto.SuplyProductId, dto.SuplyProductName, true)
            {
                Id = dto.Id,
                QuantityShouldBeAfterOrder = dto.QuantityShouldBeAfterOrder,
                ShouldUpdateQuantityBeforeOrder = dto.ShouldUpdateQuantityBeforeOrder
            };
        }
        
        private SuplyProductToOrderItemDTO MapDto(SuplyProductToOrderItemViewModel vm)
        {
            return new SuplyProductToOrderItemDTO
            {
                Id = vm.Id,
                OrderId = autoOrderId,
                SuplyProductId = vm.SuplyProductId,
                QuantityShouldBeAfterOrder = vm.QuantityShouldBeAfterOrder,
                ShouldUpdateQuantityBeforeOrder = vm.ShouldUpdateQuantityBeforeOrder
            };
        }

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }
    }
}