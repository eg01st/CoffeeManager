using System.Windows.Input;
using CoffeeManager.Core.Managers;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class EndShiftViewModel :ViewModelBase
    {
        private ShiftManager _shiftManager = new ShiftManager();
        private string _realAmount;
        private string _milkPack;
        private string _coffeePack;
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

        public bool EndShiftButtonEnabled
            => !string.IsNullOrEmpty(RealAmount) && !string.IsNullOrEmpty(MilkPack) && !string.IsNullOrEmpty(CoffeePack)
            ;
        public string MilkPack
        {
            get { return _milkPack; }
            set
            {
                _milkPack = value;
                RaisePropertyChanged(nameof(MilkPack));
                RaisePropertyChanged(nameof(EndShiftButtonEnabled));
            }
        }

        public string CoffeePack
        {
            get { return _coffeePack; }
            set
            {
                _coffeePack = value;
                RaisePropertyChanged(nameof(CoffeePack));
                RaisePropertyChanged(nameof(EndShiftButtonEnabled));
            }
        }

        public ICommand FinishShiftCommand => __finishShiftCommand;

        public EndShiftViewModel()
        {
            __finishShiftCommand  = new MvxCommand(DoFinishCommand);
        }

        public void Init(int shiftId)
        {
            _shiftId = shiftId;
        }

        private async void DoFinishCommand()
        {
            await _shiftManager.EndUserShift(_shiftId, decimal.Parse(RealAmount), int.Parse(CoffeePack), int.Parse(MilkPack));
            Close(this);
        }
    }
}
