using System;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ViewModels.SuplyProducts;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using MvvmCross.Platform;

namespace CoffeeManagerAdmin.Core
{
    public class SelectMappedSuplyProductItemViewModel : SuplyProductItemViewModel
    {
        readonly IPaymentManager paymentManager;
        readonly int expenseTypeId;

        public SelectMappedSuplyProductItemViewModel(SupliedProduct product, int expenseTypeId) : base(product)
        {
            this.expenseTypeId = expenseTypeId;
            paymentManager = Mvx.Resolve<IPaymentManager>();
        }

        protected override void DoGoToDetails()
        {
            Confirm("Связать товар с тратой?", async () =>
            {
                await paymentManager.MapExpenseToSuplyProduct(expenseTypeId, Id);
                Publish(new MappedSuplyProductChangedMessage(this));
            });

        }
    }
}
