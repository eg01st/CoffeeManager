using System.Threading.Tasks;
using System.Windows.Input;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.SuplyProducts
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
            AddSuplyProductCommand = new MvxAsyncCommand(DoAddSuplyProduct);
        }

        private async Task DoAddSuplyProduct()
        {
            if(!string.IsNullOrWhiteSpace(Name))
            {
                await ExecuteSafe(manager.AddSuplyProduct(Name));
                Publish(new SuplyListChangedMessage(this));
                Close(this);
            }
        }
    }
}
