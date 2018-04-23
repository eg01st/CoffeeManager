using System;
using CoffeeManager.Models;
using CoffeManager.Common;
using MvvmCross.Platform;
using CoffeeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;

namespace CoffeeManager.Core
{
    public class UtilizeItemViewModel : ListItemViewModelBase
    {
        private readonly ISuplyProductsManager manager;
        private SupliedProduct s;

        public override string Name => s.Name;

        public UtilizeItemViewModel()
        {
        }

        public UtilizeItemViewModel(SupliedProduct s)
        {
            this.s = s;
            manager = Mvx.Resolve<ISuplyProductsManager>();
        }

        protected async override void DoGoToDetails()
        {
            var quantity = await PromtDecimalAsync("Введите количетсво для списания");
            if(!quantity.HasValue)
            {
                return;
            }
            var reason = await PromtStringAsync("Введите причину");
            if(string.IsNullOrEmpty(reason))
            {
                return;
            }

            var item = new UtilizedSuplyProduct();
            item.SuplyProductId = s.Id;
            item.Quantity = quantity.Value;
            item.Date = DateTime.Now;
            item.Reason = reason;
            item.ShiftId = BaseManager.ShiftNo;
            item.CoffeeRoomNo = Config.CoffeeRoomNo;

            await ExecuteSafe(async () =>
            {
                await manager.UtilizeSuplyProduct(item);
                Alert("Списано!");
            });
        }
    }
}
