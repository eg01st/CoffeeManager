using System.Windows.Input;
using CoffeManager.Common;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class EndShiftViewModel :ViewModelBase
    {
        private readonly IShiftManager shiftManager;

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



        public EndShiftViewModel(IShiftManager shiftManager)
        {
            this.shiftManager = shiftManager;
            __finishShiftCommand  = new MvxCommand(DoFinishCommand);
        }

        public void Init(int shiftId)
        {
            _shiftId = shiftId;
        }

        private async void DoFinishCommand()
        {
            await ExecuteSafe(async () =>
           {
               var info = await shiftManager.EndUserShift(_shiftId, decimal.Parse(RealAmount), int.Parse(EndCounter));
               Alert($"Касса за смену: {info.RealShiftAmount:F}\nЗаработано за смену: {info.EarnedAmount:F}\nОбщая сумма зп: {info.CurrentUserAmount:F}",
                       () => Close(this),
                       "Окончание смены");
           });
        }
    }
}
