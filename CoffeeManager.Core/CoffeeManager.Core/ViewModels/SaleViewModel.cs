using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Models;

namespace CoffeeManager.Core.ViewModels
{
    public class SaleViewModel : ViewModelBase
    {
        private Sale _sale;
        public SaleViewModel(Sale sale)
        {
            _sale = sale;
        }

        public string Name => _sale.Product1.Name;

        public string Amount => _sale.Amount.ToString();

        public bool IsPoliceSale => _sale.IsPoliceSale;

        public string Time => _sale.Time.TimeOfDay.ToString();
    }
}
