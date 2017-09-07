using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeManager.Common;
using MvvmCross.Plugins.Messenger;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public class MapExpenseToSuplyProductViewModel : BaseSearchViewModel<SelectMappedSuplyProductItemViewModel>
    {
        private MvxSubscriptionToken _listChanged;
        private int expenseTypeId;
        readonly ISuplyProductsManager suplyProductManager;

        public ICommand AddNewSuplyProductCommand { get; }

        public MapExpenseToSuplyProductViewModel(ISuplyProductsManager suplyProductManager)
        {
            this.suplyProductManager = suplyProductManager;
            _listChanged = Subscribe<SuplyListChangedMessage>((obj) => base.Init());
            AddNewSuplyProductCommand = new MvxCommand(() => ShowViewModel<AddSuplyProductViewModel>());
        }

        public void Init(int id)
        {
            expenseTypeId = id;
        }

        public async override Task<List<SelectMappedSuplyProductItemViewModel>> LoadData()
        {
		    var items = await suplyProductManager.GetSuplyProducts();
            return items.Where(i => i.ExpenseTypeId != expenseTypeId).Select(s => new SelectMappedSuplyProductItemViewModel(s, expenseTypeId)).ToList();
        }
    }
}
