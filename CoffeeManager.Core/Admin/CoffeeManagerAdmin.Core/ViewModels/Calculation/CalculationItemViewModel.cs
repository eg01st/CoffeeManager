using System.Windows.Input;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Messages;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class CalculationItemViewModel : ListItemViewModelBase
    {
        private ICommand _deleteCommand;

        private CalculationItem _item;
        public CalculationItemViewModel(CalculationItem item)
        {
            _item = item;
            _deleteCommand = new MvxCommand(DoDelete);
        }

        private async void DoDelete()
        {
            var manager= new SuplyProductsManager();
            await manager.DeleteProductCalculationItem(_item.Id);
            Publish(new CalculationListChangedMessage(this));
        }

        public ICommand DeleteCommand => _deleteCommand;
        public int Id => _item.Id;

        public string Name => _item.Name;

        public decimal Quantity => _item.Quantity;
    }
}
