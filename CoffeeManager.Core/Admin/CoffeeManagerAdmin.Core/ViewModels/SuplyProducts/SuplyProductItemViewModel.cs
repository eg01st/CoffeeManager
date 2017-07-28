using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;
using System;
using CoffeeManagerAdmin.Core.Managers;
using System.Linq;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class SuplyProductItemViewModel : ViewModelBase
    {
        private SupliedProduct _item;
        private ICommand _selectCommand;

        public ICommand SelectCommand => _selectCommand;


        public SuplyProductItemViewModel(SupliedProduct product)
        {
            _item = product;
            _selectCommand = new MvxCommand(DoShowDetails);
        }

        private void DoShowDetails()
        {
            ShowViewModel<SuplyProductDetailsViewModel>(new { id = _item.Id });
        }

        public string Name => _item.Name;

        public decimal Price => _item.Price;

        public string Quatity => _item.Quatity?.ToString("F");
    }
}
