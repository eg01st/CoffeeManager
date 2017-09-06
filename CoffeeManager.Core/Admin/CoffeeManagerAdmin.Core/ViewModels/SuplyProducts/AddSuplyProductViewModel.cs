using System;
using CoffeManager.Common;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
namespace CoffeeManagerAdmin.Core
{
    public class AddSuplyProductViewModel : ViewModelBase
    {
        private string name;

        public string Name 
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public ICommand AddSuplyProductCommand { get; }

        readonly ISuplyProductsManager manager;

        public AddSuplyProductViewModel(ISuplyProductsManager manager)
        {
            this.manager = manager;
            AddSuplyProductCommand = new MvxCommand(DoAddSuplyProduct);
        }

        private async void DoAddSuplyProduct()
        {
            if(!string.IsNullOrWhiteSpace(Name))
            {
                await manager.AddSuplyProduct(Name);
                Publish(new SuplyListChangedMessage(this));
                Close(this);
            }
        }
    }
}
