using System;
using CoffeeManager.Models;
using CoffeManager.Common;
using MvvmCross.Platform;

namespace CoffeeManagerAdmin.Core
{
    public class MappedSuplyProductItemViewModel : ViewModels.SuplyProductItemViewModel
    {
        readonly int expenseTypeId;
        readonly IPaymentManager paymentManager;

        public MappedSuplyProductItemViewModel(SupliedProduct product, int expenseTypeId) : base(product)
        {
            this.expenseTypeId = expenseTypeId;
            paymentManager = Mvx.Resolve<IPaymentManager>();
        }

        protected override void DoGoToDetails()
        {
            Confirm("Удалить связанный товар с тратой?", async () =>
            {
                await paymentManager.RemoveMappedSuplyProductsToExpense(expenseTypeId, Id);
                Publish(new MappedSuplyProductChangedMessage(this));
            });
        }
    }
}
