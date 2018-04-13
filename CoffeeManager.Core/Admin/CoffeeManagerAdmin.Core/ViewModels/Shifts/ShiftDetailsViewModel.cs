using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;
using System.Threading.Tasks;
using CoffeeManagerAdmin.Core.ViewModels.Users;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class ShiftDetailsViewModel : ViewModelBase
    {
        private readonly MvxSubscriptionToken updateToken;
        
        private readonly IShiftManager shiftManager;
        private readonly IPaymentManager paymentManager;

        private int userId;
        private int _shiftId;
        private string _date;
        private string _name;
        private int? _usedCoffee;
        private int? _counter;
        private int _rejectedSales;
        private int _utilizeSales;
        private bool isFinished;
        private List<ExpenseItemViewModel> _expenseItems = new List<ExpenseItemViewModel>();

        private float _copSalePercentage;


        public ShiftDetailsViewModel(IShiftManager shiftManager, IPaymentManager paymentManager)
        {
            this.paymentManager = paymentManager;
            this.shiftManager = shiftManager;
            ShowSalesCommand = new MvxCommand(DoShowSales);
            AddExpenseCommand = new MvxCommand(() => ShowViewModel<AddShiftExpenseViewModel>(new { id = _shiftId }));
            ShowUserDetailsCommand = new MvxCommand(() => ShowViewModel<UserDetailsViewModel>(new { id = userId }));

            updateToken = Subscribe<UpdateShiftMessage>(async obj => await Init(_shiftId));
        }

        private void DoShowSales()
        {
            ShowViewModel<ShiftSalesViewModel>(new { id = _shiftId });
        }

        public async Task Init(int id)
        {
            _shiftId = id;
            await ExecuteSafe(async () =>
           {
               var shiftInfo = await shiftManager.GetShiftInfo(id);
               IsFinished = shiftInfo.IsFinished;
               Date = shiftInfo.Date.ToString("g");
               Name = shiftInfo.UserName;
               userId = shiftInfo.UserId;
               if (shiftInfo.StartCounter.HasValue && shiftInfo.EndCounter.HasValue)
               {
                   Counter = shiftInfo.EndCounter - shiftInfo.StartCounter;
               }

               var items = await paymentManager.GetShiftExpenses(_shiftId);
               ExpenseItems = items.Select(s => new ExpenseItemViewModel(s, !isFinished)).ToList();

               var saleItems = await shiftManager.GetShiftSales(_shiftId);
               if (saleItems.Any())
               {
                   CalculateCopSalePercentage(saleItems.ToList());
               }

               RejectedSales = saleItems.Count(i => i.IsRejected);
               UtilizedSales = saleItems.Count(i => i.IsUtilized);
               UsedCoffee = (int)shiftInfo.UsedPortions;

           });
            BaseManager.ShiftNo = id;
        }


        private void CalculateCopSalePercentage(List<Sale> saleItems)
        {
            int allSalesCount = saleItems.Count;

            int copSaleCount = saleItems.Count(s => s.IsPoliceSale);

            CopSalePercentage = copSaleCount * 100 / allSalesCount;
        }

        public ICommand ShowSalesCommand { get; set; }

        public ICommand AddExpenseCommand { get; set; }

        public ICommand ShowUserDetailsCommand { get; set; }

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

        public bool IsFinished
        {
            get { return isFinished; }
            set
            {
                isFinished = value;
                RaisePropertyChanged(nameof(IsFinished));
            }
        }

        protected override void DoUnsubscribe()
        {
            base.DoUnsubscribe();
            Unsubscribe<UpdateShiftMessage>(updateToken);
        }
    }
}
