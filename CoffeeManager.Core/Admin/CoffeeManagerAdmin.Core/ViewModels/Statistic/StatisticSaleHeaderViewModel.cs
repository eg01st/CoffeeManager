using System;
namespace CoffeeManagerAdmin.Core
{
    public class StatisticSaleHeaderViewModel : BaseStatisticSaleItemViewModel
    {
        public decimal Amount {get;set;}

        public StatisticSaleHeaderViewModel(string name, decimal amount)
        {
            Name = name;
            Amount = amount;
            RaiseAllPropertiesChanged();
        }

    }
}
