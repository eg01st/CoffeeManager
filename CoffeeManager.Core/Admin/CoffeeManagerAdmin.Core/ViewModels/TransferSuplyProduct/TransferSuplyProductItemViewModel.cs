using System;
using CoffeeManager.Models;
using CoffeManager.Common;
namespace CoffeeManagerAdmin.Core
{
    public class TransferSuplyProductItemViewModel : ListItemViewModelBase
    {
        private SupliedProduct _item;

        public string ExpenseTypeName => _item.ExpenseTypeName;

        public TransferSuplyProductItemViewModel(SupliedProduct s)
        {
            this._item = s;
            Name = s.Name;
        }
    }
}
