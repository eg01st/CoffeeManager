using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CoffeManager.Common;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Core.ViewModels.CoffeeCounter;
using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MobileCore.Collections;
using MobileCore.Logging;
using MobileCore.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class EndShiftViewModel : FeedViewModel<CoffeeCounterItemViewModel>, IMvxViewModel<int>
    {
        private readonly IShiftManager shiftManager;

        private string realAmount;
        private int shiftId;
        
        public string RealAmount
        {
            get { return realAmount; }
            set
            {
                realAmount = value; 
                RaisePropertyChanged(nameof(RealAmount));
                RaisePropertyChanged(nameof(EndShiftButtonEnabled));
            }
        }

        public bool EndShiftButtonEnabled
            => !string.IsNullOrEmpty(RealAmount);
        
        public ICommand FinishShiftCommand {get;}
        
        public ICommand DiscardShiftCommand {get;}

        readonly IPaymentManager paymentManager;
        private readonly ICoffeeCounterManager coffeeCounterManager;

        public EndShiftViewModel(IShiftManager shiftManager,
            IPaymentManager paymentManager,
            ICoffeeCounterManager coffeeCounterManager)
        {
            this.paymentManager = paymentManager;
            this.coffeeCounterManager = coffeeCounterManager;
            this.shiftManager = shiftManager;
            FinishShiftCommand  = new MvxAsyncCommand(DoFinishCommand);
            DiscardShiftCommand = new MvxAsyncCommand(DoDiscardShift);
        }

        protected override async Task<PageContainer<CoffeeCounterItemViewModel>> GetPageAsync(int skip)
        {
            var items = await coffeeCounterManager.GetCountersForClient();
            return items.Select(s => new CoffeeCounterItemViewModel(s)).ToPageContainer();
        }

        private async Task DoDiscardShift()
        {
            if (await UserDialogs.ConfirmAsync(
                "Отменить текущую смену? Отмена возможна только в случае всех отмененных продаж и трат"))
            {
                string promt = await PromtStringAsync("Напишите слово \"Отмена\" что бы подтвердить отмену смены");
                if(!string.Equals(promt, "Отмена", StringComparison.OrdinalIgnoreCase))
                {
                    Alert("Слово введено не правильно");
                    return;
                }
                try
                {
                    await shiftManager.DiscardShift(shiftId);
                }
                catch (Exception e)
                {
                    ConsoleLogger.Exception(e);
                    if (e.Message.Contains("Sales exist"))
                    {
                        await UserDialogs.AlertAsync("Отмените все продажи чтобы закрыть смену");
                        return;
                    }
                    else if (e.Message.Contains("Expenses exist"))
                    {
                        await UserDialogs.AlertAsync("Отмените все расходы что бы закрыть смену");
                        return;
                    }
                    else
                    {
                        await EmailService?.SendErrorEmail($"CoffeeRoomId: {Config.CoffeeRoomNo}",e.ToDiagnosticString());
                        await UserDialogs.AlertAsync("Произошла ошибка сервера");   
                        return;
                    }
                }

                await NavigationService.Navigate<LoginViewModel>();
            }
        }

        private async Task DoFinishCommand()
        {
            var currentAmount = await ExecuteSafe(paymentManager.GetEntireMoney);
            var currentMin = currentAmount - 100;
            var currentMax = currentAmount + 100;

            var realAmount = decimal.Parse(RealAmount);
            if (realAmount > currentMax || realAmount < currentMin)
            {
                if (await UserDialogs.ConfirmAsync("Текущая касса сильно отличается от реальной. Вы уверены что эта точная сумма?"))
                {
                    await FinishShift(realAmount);
                }
            }
            else
            {
                await FinishShift(realAmount);
            }
           
        }

        private async Task FinishShift(decimal realAmount)
        {
            if (ItemsCollection.Any(i => i.Counter != i.Confirm || !i.Counter.HasValue))
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
                    EndCounter = counter.Counter.Value,
                    SuplyProductId = counter.SuplyProductId
                });
            }
            
            await ExecuteSafe(async () =>
            {
                var info = await shiftManager.EndUserShift(shiftId, realAmount, counters);
                Alert($"Касса за смену: {info.RealShiftAmount:F}\nЗаработано за смену: {info.EarnedAmount:F}\nОбщая сумма зп: {info.CurrentUserAmount:F}",
                      () => NavigationService.Navigate<LoginViewModel>(),
                        "Окончание смены");
            });
        }

        public void Prepare(int parameter)
        {
            shiftId = parameter;
        }
    }
}
