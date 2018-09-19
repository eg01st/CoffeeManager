using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models.Data.Product;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Products
{
    public class AddProductViewModel : PageViewModel
    {
        private readonly IProductManager productManager;
        private string productName;
        
        public AddProductViewModel(IProductManager productManager)
        {
            this.productManager = productManager;
            
            AddProductCommad = new MvxAsyncCommand(DoAddProduct, () => !string.IsNullOrEmpty(productName));
        }

        private async Task DoAddProduct()
        {
            UserDialogs.Confirm(new Acr.UserDialogs.ConfirmConfig()
            {
                Message = $"Добавить продукт \"{productName}\"?",
                OnAction = async (obj) =>
                {
                    if (obj)
                    {
                        await ExecuteSafe(async () =>
                        {
                            int prodId = await productManager.AddProduct(new ProductDetaisDTO() {Name = ProductName});
                            MvxMessenger.Publish(new ProductListChangedMessage(this));
                            await NavigationService.Navigate<ProductDetailsViewModel, int>(prodId);
                            CloseCommand.Execute(null);
                        });
                    }
                }
            });
        }

        public string ProductName
        {
            get => productName;
            set
            {
                SetProperty(ref productName, value);
                RaisePropertyChanged(nameof(AddProductCommad));
            }
        }

        public ICommand AddProductCommad { get; }
    }
}