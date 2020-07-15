using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeManagerAdmin.Core.ViewModels.Abstract;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MobileCore.Collections;

namespace CoffeeManagerAdmin.Core.ViewModels.Utilize
{
    public class UtilizeViewModel : AdminCoffeeRoomFeedViewModel<UtilizeItemViewModel>
    {
        public List<UtilizeItemViewModel> Items { get; set; }

        readonly ISuplyProductsManager manager;

        public UtilizeViewModel(ISuplyProductsManager manager)
        {
            this.manager = manager;
        }

        protected override async Task<PageContainer<UtilizeItemViewModel>> GetPageAsync(int skip)
        {
            var items = await manager.GetUtilizedProducts();
            return items.Select(s => new UtilizeItemViewModel(s)).OrderByDescending(o => o.Id).ToPageContainer();
        }

        //public override async Task Initialize()
        //{
        //    var items = await manager.GetUtilizedProducts();
        //    Items = items.Select(s => new UtilizeItemViewModel(s)).OrderByDescending(o => o.Id).ToList();
        //    RaisePropertyChanged(nameof(Items));
        //}
    }
}
