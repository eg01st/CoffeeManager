using System;
using System.Windows.Input;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public class AddExpenseTypeViewModel : ViewModelBase
    {
        private string expenseName;

        public string ExpenseName 
        {
            get { return expenseName; }
            set
            {
                expenseName = value;
                RaisePropertyChanged(nameof(ExpenseName));
            }
        }

        public ICommand AddExpenseTypeCommand { get; set; }

        private readonly IPaymentManager paymentManager;

        public AddExpenseTypeViewModel(IPaymentManager paymentManager)
        {
            this.paymentManager = paymentManager;
            AddExpenseTypeCommand = new MvxCommand(DoAddExpenseType);
        }

        private async void DoAddExpenseType()
        {
            if(!string.IsNullOrEmpty(ExpenseName))
            {
                await paymentManager.AddNewExpenseType(ExpenseName);
                Publish(new ExpenseListChangedMessage(this));
                Close(this);
            }
        }
    }
}
