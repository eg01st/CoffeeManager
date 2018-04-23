using CoffeeManager.Models;

namespace CoffeeManagerAdmin.Core.ViewModels.Shifts
{
    public class ShiftItemViewModel : MobileCore.ViewModels.FeedItemElementViewModel
    {
        private readonly ShiftInfo _info;
 
        public ShiftItemViewModel(ShiftInfo info)
        {
            _info = info;
        }

        protected override async void Select()
        {
            await NavigationService.Navigate<ShiftDetailsViewModel, int>(_info.Id);
        } 

        public int Id => _info.Id;

        public string Date => _info.Date.Date.ToString("dd-MM");

        public string UserName => _info.UserName;

        public bool IsPositive => _info.RealAmount - _info.TotalAmount >= 0;

        public string StartAmount => ((int)_info.StartMoney).ToString();

        public string EarnedAmount => ((int)_info.ShiftEarnedMoney).ToString();

        public string TotalAmount => ((int)_info.TotalAmount).ToString();

        public string RealAmount => ((int)_info.RealAmount).ToString();

        public string ExpenseAmount => ((int)_info.ExpenseAmount).ToString();

        public string CreditCardAmount => _info.IsFinished ? (realShiftAmount + _info.CreditCardAmount).ToString() : "0";

        public string RealShiftAmount => _info.IsFinished ? ((int)realShiftAmount).ToString() : "0";

        private decimal realShiftAmount => _info.RealAmount - _info.TotalAmount + _info.ShiftEarnedMoney;
    }
}
