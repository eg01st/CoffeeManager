using System.Windows.Input;
using CoffeManager.Common;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class EndShiftViewModel : ViewModelBase
    {
        private readonly IShiftManager shiftManager;

        private string _realAmount;
        private string _endCounter;
        private int _shiftId;
        public string RealAmount
        {
            get { return _realAmount; }
            set
            {
                _realAmount = value; 
                RaisePropertyChanged(nameof(RealAmount));
                RaisePropertyChanged(nameof(EndShiftButtonEnabled));
            }
        }

        public string EndCounter
        {
            get { return _endCounter; }
            set
            {
                _endCounter = value;
                RaisePropertyChanged(nameof(EndCounter));
                RaisePropertyChanged(nameof(EndShiftButtonEnabled));
            }
        }

        public bool EndShiftButtonEnabled
            => !string.IsNullOrEmpty(RealAmount) && !string.IsNullOrEmpty(EndCounter);


        public ICommand FinishShiftCommand {get;}

        readonly IPaymentManager paymentManager;

        public EndShiftViewModel(IShiftManager shiftManager, IPaymentManager paymentManager)
        {
            this.paymentManager = paymentManager;
            this.shiftManager = shiftManager;
            FinishShiftCommand  = new MvxCommand(DoFinishCommand);
        }

        public void Init(int shiftId)
        {
            _shiftId = shiftId;
        }

        private async void DoFinishCommand()
        {
            var currentAmount = await ExecuteSafe(paymentManager.GetCurrentShiftMoney);
            var currentMin = currentAmount - 100;
            var currentMax = currentAmount + 100;

            var realAmount = decimal.Parse(RealAmount);
            if(realAmount > currentMax || realAmount < currentMin)
            {
                if(await UserDialogs.ConfirmAsync("Текущая касса сильно отличается от реальной. Вы уверены что эта точная сумма?"))
                {
                    await ExecuteSafe(async () =>
                    {
                        var info = await shiftManager.EndUserShift(_shiftId, realAmount, int.Parse(EndCounter));
                        Alert($"Касса за смену: {info.RealShiftAmount:F}\nЗаработано за смену: {info.EarnedAmount:F}\nОбщая сумма зп: {info.CurrentUserAmount:F}",
                              () => NavigationService.Navigate<LoginViewModel>(),
                                "Окончание смены");
                    });
                }
            }
           
        }
    }
}
