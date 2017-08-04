﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.Messages;
using CoffeManager.Common;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManager.Core.ViewModels
{
    public class CurrentShiftExpensesViewModel : ViewModelBase
    {
        private readonly IPaymentManager manager;
        private List<ExpenseItemViewModel> _items = new List<ExpenseItemViewModel>();
        private MvxSubscriptionToken _token;
        public List<ExpenseItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

   

        public CurrentShiftExpensesViewModel(IPaymentManager manager)
        {
            this.manager = manager;
            _token = Subscribe<ExpenseDeletedMessage>(async (obj) => await LoadData());
        }

        public async void Init()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            var items = await manager.GetShiftExpenses();
            Items = items.Select(s => new ExpenseItemViewModel(manager, s)).ToList();
        }
    }
}
