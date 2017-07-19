using System.Windows.Input;
using CoffeeManager.Core.Managers;
using CoffeeManager.Core.ServiceProviders;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class EndShiftViewModel :ViewModelBase
    {
        private ShiftManager _shiftManager = new ShiftManager();
        private string _realAmount;
        private string _endCounter;
        private ICommand __finishShiftCommand;

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


        public ICommand FinishShiftCommand => __finishShiftCommand;

        public EndShiftViewModel()
        {
            __finishShiftCommand  = new MvxCommand(DoFinishCommand);
        }

        public async void Init(int shiftId)
        {
            _shiftId = shiftId;
            var sales = ProductManager.GetSalesStorage();
            await _shiftManager.AssertShiftSales(sales);
            ProductManager.ClearStorage();
        }

        private async void DoFinishCommand()
        {
            var info = await _shiftManager.EndUserShift(_shiftId, decimal.Parse(RealAmount), int.Parse(EndCounter));
            UserDialogs.Alert("Окончание смены", $"Касса за смену: {info.RealShitAmount} \n Заработано за смену: {info.EarnedAmount}");
            Close(this);
        }
    }
}
