using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using CoffeManager.Common;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels
{
    public class SuplyProductsViewModel : BaseSearchViewModel<SuplyProductItemViewModel>
    {
        private MvxSubscriptionToken _listChanged;

        readonly ISuplyProductsManager manager;

        public ICommand AddNewSuplyProductCommand { get; }


        public SuplyProductsViewModel(ISuplyProductsManager manager)
        {
            this.manager = manager;
            _listChanged = Subscribe<SuplyListChangedMessage>((obj) => base.Init());
            AddNewSuplyProductCommand = new MvxCommand(() => ShowViewModel<AddSuplyProductViewModel>());
        }

        public async override Task<List<SuplyProductItemViewModel>> LoadData()
        {
            var items = await manager.GetSuplyProducts();
            return items.Select(s => new SuplyProductItemViewModel(s)).ToList();
        }
    }
}
