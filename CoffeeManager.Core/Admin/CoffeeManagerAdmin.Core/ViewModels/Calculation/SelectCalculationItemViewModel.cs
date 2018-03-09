using Acr.UserDialogs;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.Messages;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class SelectCalculationItemViewModel : ListItemViewModelBase
    {
        private int _productId;
        private SupliedProduct _prod;

        public override string Name => _prod.Name;

        readonly ISuplyProductsManager manager;

        public SelectCalculationItemViewModel(ISuplyProductsManager manager, int productId, SupliedProduct prod)
        {
            this.manager = manager;
            _prod = prod;
            _productId = productId;
        }

        protected override void DoGoToDetails()
        {
            DoAddCalculationItem();
        }

        private void DoAddCalculationItem()
        {
            UserDialogs.Prompt(new PromptConfig()
            {
                InputType = InputType.DecimalNumber,
                Message = "Укажите сколько тратится на одну порцию продукта (в килограммах, литрах, штуках)",
                OnAction = AddItem,

            });
        }

        private async void AddItem(PromptResult obj)
        {
            if (obj.Ok)
            {
                await manager.AddProductCalculationItem(_productId, _prod.Id, decimal.Parse(obj.Text));
                Publish(new CalculationListChangedMessage(this));
            }
        }

    }
}
