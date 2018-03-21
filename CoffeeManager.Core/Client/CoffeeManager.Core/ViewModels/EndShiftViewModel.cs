using System;
using System.Windows.Input;
using CoffeManager.Common;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeManager.Common.ViewModels;
using MobileCore.Logging;
using MobileCore.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class EndShiftViewModel : PageViewModel
    {
        private readonly IShiftManager shiftManager;

        private string realAmount;
        private string endCounter;
        private string endCounterConfirm;
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

        public string EndCounter
        {
            get { return endCounter; }
            set
            {
                endCounter = value;
                RaisePropertyChanged(nameof(EndCounter));
                RaisePropertyChanged(nameof(EndShiftButtonEnabled));
            }
        }

        public string EndCounterConfirm
        {
            get { return endCounterConfirm; }
            set
            {
                endCounterConfirm = value;
                RaisePropertyChanged(nameof(EndCounterConfirm));
                RaisePropertyChanged(nameof(EndShiftButtonEnabled));
            }
        }

        public bool EndShiftButtonEnabled
        => !string.IsNullOrEmpty(RealAmount) 
           && !string.IsNullOrEmpty(EndCounter) 
           && string.Equals(EndCounter, EndCounterConfirm);
          // && !IsLoading;


        public ICommand FinishShiftCommand {get;}
        
        public ICommand DiscardShiftCommand {get;}

        readonly IPaymentManager paymentManager;

        public EndShiftViewModel(IShiftManager shiftManager, IPaymentManager paymentManager)
        {
            this.paymentManager = paymentManager;
            this.shiftManager = shiftManager;
            FinishShiftCommand  = new MvxAsyncCommand(DoFinishCommand);
            DiscardShiftCommand = new MvxAsyncCommand(DoDiscardShift);
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

        public void Init(int shiftId)
        {
            this.shiftId = shiftId;
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
            await ExecuteSafe(async () =>
            {
                var info = await shiftManager.EndUserShift(shiftId, realAmount, int.Parse(EndCounter));
                Alert($"Касса за смену: {info.RealShiftAmount:F}\nЗаработано за смену: {info.EarnedAmount:F}\nОбщая сумма зп: {info.CurrentUserAmount:F}",
                      () => NavigationService.Navigate<LoginViewModel>(),
                        "Окончание смены");
            });
        }
    }
}
