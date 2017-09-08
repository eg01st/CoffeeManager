using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeManager.Core.ViewModels;
using CoffeeManager.Models;
namespace CoffeeManager.Core
{
    public class ExpenseItemExtendedViewModel : BaseItemViewModel
    {
        public List<SuplyProductViewModel> ExpenseSuplyProducts { get; set; } = new List<SuplyProductViewModel>();
       
        public ExpenseItemExtendedViewModel(ExpenseType expense) : base(expense)
        {
            ExpenseSuplyProducts = expense.SuplyProducts.Select(s => new SuplyProductViewModel(s)).ToList();
        }
    }
}
