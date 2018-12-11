﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ViewModels.Users;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Shifts
{
    public class ShiftDetailsViewModel : FeedViewModel<ExpenseItemViewModel>, IMvxViewModel<int>
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
            ShowSalesCommand = new MvxAsyncCommand(DoShowSales);
            ShowCountersCommand = new MvxAsyncCommand(DoShowCounters);
            AddExpenseCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AddShiftExpenseViewModel>());
            ShowUserDetailsCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<UserDetailsViewModel, int>(userId));

            updateToken = MvxMessenger.Subscribe<UpdateShiftMessage>(async obj => await Initialize());
        }

        private async Task DoShowCounters()
        {
            await NavigationService.Navigate<ShiftCountersViewModel, int>(_shiftId);
        }

        public ICommand ShowSalesCommand { get; set; }
        
        public ICommand ShowCountersCommand { get; set; }

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

        //public List<ExpenseItemViewModel> ExpenseItems
        //{
        //    get { return _expenseItems; }
        //    set
        //    {
        //        _expenseItems = value;
        //        RaisePropertyChanged(nameof(ExpenseItems));
        //    }
        //}


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
            MvxMessenger.Unsubscribe<UpdateShiftMessage>(updateToken);
        }

        public void Prepare(int parameter)
        {
            _shiftId = parameter;
        }
        
        public override async Task Initialize()
        {
            await ExecuteSafe(async () =>
            {
                var shiftInfo = await shiftManager.GetShiftInfo(_shiftId);
                IsFinished = shiftInfo.IsFinished;
                Date = shiftInfo.Date.ToString("g");
                Name = shiftInfo.UserName;
                userId = shiftInfo.UserId;
                if (shiftInfo.StartCounter.HasValue && shiftInfo.EndCounter.HasValue)
                {
                    Counter = shiftInfo.EndCounter - shiftInfo.StartCounter;
                }

                var items = await paymentManager.GetShiftExpenses(_shiftId);
                ItemsCollection.ReplaceWith(items.Select(s => new ExpenseItemViewModel(s, !isFinished)));

                var saleItems = await shiftManager.GetShiftSales(_shiftId);
                if (saleItems.Any())
                {
                    CalculateCopSalePercentage(saleItems.ToList());
                }

                RejectedSales = saleItems.Count(i => i.IsRejected);
                UtilizedSales = saleItems.Count(i => i.IsUtilized);
                UsedCoffee = (int)shiftInfo.UsedPortions;

            });
            BaseManager.ShiftNo = _shiftId;
        }
        
        private async Task DoShowSales()
        {
            await NavigationService.Navigate<ShiftSalesViewModel, int>(_shiftId);
        }

        private void CalculateCopSalePercentage(List<Sale> saleItems)
        {
            int allSalesCount = saleItems.Count;

            int copSaleCount = saleItems.Count(s => s.IsPoliceSale);

            CopSalePercentage = copSaleCount * 100 / allSalesCount;
        }

    }
}
