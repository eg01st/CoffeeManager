using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Core.Messages;
using CoffeeManager.Models;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private ICommand _selectItemCommand;
        private Product product;

        public int Id => product.Id;

        public float Price => product.Price;

        public string Name => product.Name;

        public ICommand SelectItemCommand => _selectItemCommand;

        public ProductViewModel(Product product)
        {
            this.product = product;
            _selectItemCommand = new MvxAsyncCommand(DoSelectItem);
        }

        private Task DoSelectItem()
        {
            ProductManager.SaleProduct(Id);
            MvxMessenger.Publish(new AmoutChangedMessage(new Tuple<float, bool>(Price, true), this));
            ToastMessage("test");
            return Task.Delay(2000);
        }
    }
}
