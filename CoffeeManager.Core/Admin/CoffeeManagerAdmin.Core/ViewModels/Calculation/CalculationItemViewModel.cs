using System.Windows.Input;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Messages;
using MvvmCross.Core.ViewModels;
using CoffeManager.Common;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class CalculationItemViewModel : ListItemViewModelBase
    {
        private ICommand _deleteCommand;

        private CalculationItem _item;
        readonly ISuplyProductsManager manager;

        public CalculationItemViewModel(ISuplyProductsManager manager, CalculationItem item)
        {
            this.manager = manager;
            _item = item;
            _deleteCommand = new MvxCommand(DoDelete);
        }

        private async void DoDelete()
        {
            await manager.DeleteProductCalculationItem(_item.Id);
            Publish(new CalculationListChangedMessage(this));
        }

        public ICommand DeleteCommand => _deleteCommand;
        public int Id => _item.Id;

        public override string Name => _item.Name;

        public decimal Quantity => _item.Quantity;
    }
}
