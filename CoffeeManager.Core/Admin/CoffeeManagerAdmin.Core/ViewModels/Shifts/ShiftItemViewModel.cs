using System;
using System.Windows.Input;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class ShiftItemViewModel : ListItemViewModelBase
    {
        private readonly ShiftInfo _info;
 
        public ShiftItemViewModel(ShiftInfo info)
        {
            _info = info;
        }

        protected override void DoGoToDetails()
        {
            ShowViewModel<ShiftDetailsViewModel>(new { id = _info.Id });
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
