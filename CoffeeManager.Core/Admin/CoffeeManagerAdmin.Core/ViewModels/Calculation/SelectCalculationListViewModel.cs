using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeManager.Common;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class SelectCalculationListViewModel : BaseSearchViewModel<SelectCalculationItemViewModel>
    {
        private int productId;
        private MvxSubscriptionToken _listChanged;

        readonly ISuplyProductsManager manager;

        public ICommand AddNewSuplyProductCommand { get; }

        public SelectCalculationListViewModel(ISuplyProductsManager manager)
        {
            this.manager = manager;
            _listChanged = MvxMessenger.Subscribe<SuplyListChangedMessage>((obj) => base.Init());
            AddNewSuplyProductCommand = new MvxCommand(() => ShowViewModel<AddSuplyProductViewModel>());
        }
        public void Init(int productId)
        {
            this.productId = productId;
        }

        public async override Task<List<SelectCalculationItemViewModel>> LoadData()
        {
            var items = await manager.GetSuplyProducts();
            return items.Select(s => new SelectCalculationItemViewModel(manager, productId, s)).ToList();
        }
        protected override void DoUnsubscribe()
        {
            MvxMessenger.Unsubscribe<SuplyListChangedMessage>(_listChanged);
        }
    }
}
