using System;
using CoffeeManager.Models;
using CoffeeManagerAdmin.Core.ViewModels.SuplyProducts;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using MvvmCross.Platform;

namespace CoffeeManagerAdmin.Core
{
    public class MappedSuplyProductItemViewModel : SuplyProductItemViewModel
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
            Confirm("Удалить связанный товар с расходом?", async () =>
            {
                await paymentManager.RemoveMappedSuplyProductsToExpense(expenseTypeId, Id);
                Publish(new MappedSuplyProductChangedMessage(this));
            });
        }
    }
}
