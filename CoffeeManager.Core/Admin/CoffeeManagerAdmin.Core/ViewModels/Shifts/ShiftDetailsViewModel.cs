using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Managers;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class ShiftDetailsViewModel : ViewModelBase
    {
        private int _shiftId;
        private ShiftManager shiftManager = new ShiftManager();
        private PaymentManager paymentManager = new PaymentManager();
        private string _date;
        private string _name;
        private int? _usedCoffee;
        private int? _counter;
        private int _rejectedSales;
        private int _utilizeSales;
        private List<ExpenseItemViewModel> _expenseItems = new List<ExpenseItemViewModel>();

        private float _copSalePercentage;

        public ShiftDetailsViewModel()
        {
            ShowSalesCommand = new MvxCommand(DoShowSales);
        }

        private void DoShowSales()
        {
            ShowViewModel<ShiftSalesViewModel>(new { id = _shiftId });
        }

        public async void Init(int id)
        {
            _shiftId = id;

            var items = await paymentManager.GetShiftExpenses(_shiftId);
            ExpenseItems = items.Select(s => new ExpenseItemViewModel(s)).ToList();

            var saleItems = await shiftManager.GetShiftSales(_shiftId);
            CalculateCopSalePercentage(saleItems.ToList());

            var shiftInfo = await shiftManager.GetShiftInfo(id);
            Date = shiftInfo.Date.ToString("g");
            Name = shiftInfo.UserName;
            if (shiftInfo.StartCounter.HasValue && shiftInfo.EndCounter.HasValue)
            {
                Counter = shiftInfo.EndCounter - shiftInfo.StartCounter;
            }
            RejectedSales = saleItems.Count(i => i.IsRejected);
            UtilizedSales = saleItems.Count(i => i.IsUtilized);
            UsedCoffee = (int)shiftInfo.UsedPortions;
        }


        private void CalculateCopSalePercentage(List<Sale> saleItems)
        {
            int allSalesCount = saleItems.Count;

            int copSaleCount = saleItems.Count(s => s.IsPoliceSale);

            CopSalePercentage = copSaleCount * 100 / allSalesCount;
        }

        public ICommand ShowSalesCommand { get; set; }

        public float CopSalePercentage
        {
            get { return _copSalePercentage; }
            set
            {
                _copSalePercentage = value;
                RaisePropertyChanged(nameof(CopSalePercentage));
            }
        }

        public List<ExpenseItemViewModel> ExpenseItems
        {
            get { return _expenseItems; }
            set
            {
                _expenseItems = value;
                RaisePropertyChanged(nameof(ExpenseItems));
            }
        }


        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged(nameof(Date));
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public int? UsedCoffee
        {
            get { return _usedCoffee; }
            set
            {
                _usedCoffee = value;
                RaisePropertyChanged(nameof(UsedCoffee));
            }
        }

        public int? Counter
        {
            get { return _counter; }
            set
            {
                _counter = value;
                RaisePropertyChanged(nameof(Counter));
            }
        }

        public int RejectedSales
        {
            get { return _rejectedSales; }
            set
            {
                _rejectedSales = value;
                RaisePropertyChanged(nameof(RejectedSales));
            }
        }

        public int UtilizedSales
        {
            get { return _utilizeSales; }
            set
            {
                _utilizeSales = value;
                RaisePropertyChanged(nameof(UtilizedSales));
            }
        }
    }
}
