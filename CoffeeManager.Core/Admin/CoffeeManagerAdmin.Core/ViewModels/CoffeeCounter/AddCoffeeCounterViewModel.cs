using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.Category;
using CoffeeManager.Models.Data.DTO.CoffeeRoomCounter;
using CoffeeManagerAdmin.Core.Messages;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using MobileCore.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.CoffeeCounter
{
    public class AddCoffeeCounterViewModel : PageViewModel
    {
        protected string counterName;
        protected string suplyProductName;
        protected int suplyProductId;

        protected string categoryName;
        protected int categoryId;

        protected SupliedProduct[] suplyProducts;
        protected IEnumerable<CategoryDTO> categories;
        
        protected readonly ICoffeeCounterManager counterManager;
        protected readonly ISuplyProductsManager suplyProductsManager;
        protected readonly ICategoryManager categoryManager;

        public string CounterName
        {
            get => counterName;
            set
            {
                counterName = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(AddCounterCommand));
            }
        }
        
        public string SuplyProductName
        {
            get => suplyProductName;
            set
            {
                suplyProductName = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(AddCounterCommand));
            }
        }
        
        public string CategoryName
        {
            get => categoryName;
            set
            {
                categoryName = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(AddCounterCommand));
            }
        }
        
        public ICommand AddCounterCommand { get; }
        public ICommand SelectSuplyProductCommand { get; }
        public ICommand SelectCategoryCommand { get; }
        
        public AddCoffeeCounterViewModel(ICoffeeCounterManager counterManager,
            ISuplyProductsManager suplyProductsManager,
            ICategoryManager categoryManager)
        {
            this.counterManager = counterManager;
            this.suplyProductsManager = suplyProductsManager;
            this.categoryManager = categoryManager;
            AddCounterCommand = new MvxAsyncCommand(DoAddCounter, CanAddCounter);
            SelectSuplyProductCommand = new MvxCommand(DoSelectSuplyProduct);
            SelectCategoryCommand = new MvxCommand(DoSelectCategory);

        }

        protected override async Task DoLoadDataImplAsync()
        {
            suplyProducts = await suplyProductsManager.GetSuplyProducts();
            categories = await categoryManager.GetCategoriesPlain();
        }

        protected void DoSelectCategory()
        {
            var optionList = new List<ActionSheetOption>();
            foreach (var cr in categories)
            {
                optionList.Add(new ActionSheetOption(cr.Name, () =>
                {
                    CategoryName = cr.Name;
                    categoryId = cr.Id;
                }));
            }

            UserDialogs.ActionSheet(new ActionSheetConfig
            {
                Options = optionList,
                Title = "Выбор категории",
            });
        }

        protected void DoSelectSuplyProduct()
        {
            var optionList = new List<ActionSheetOption>();
            foreach (var cr in suplyProducts)
            {
                optionList.Add(new ActionSheetOption(cr.Name, () =>
                {
                    SuplyProductName = cr.Name;
                    suplyProductId = cr.Id;
                }));
            }

            UserDialogs.ActionSheet(new ActionSheetConfig
            {
                Options = optionList,
                Title = "Выбор продукта",
            });
        }

        protected bool CanAddCounter()
        {
            return !string.IsNullOrWhiteSpace(CounterName) && categoryId > 0 && suplyProductId > 0;
        }

        protected virtual async Task DoAddCounter()
        {
            var dto = new CoffeeCounterForCoffeeRoomDTO();
            dto.Name = CounterName;
            dto.CoffeeRoomNo = Config.CoffeeRoomNo;
            dto.CategoryId = categoryId;
            dto.SuplyProductId = suplyProductId;
            await ExecuteSafe(async () =>
            {
                var counterId = await counterManager.AddCounter(dto);
                MvxMessenger.Publish(new CoffeeCountersUpdateMessage(this));
                CloseCommand.Execute(null);
            });
        }
    }
}