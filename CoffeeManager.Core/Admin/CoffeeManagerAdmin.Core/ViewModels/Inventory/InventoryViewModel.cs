using System;
using CoffeManager.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core
{
    public class InventoryViewModel : ViewModelBase
    {
        readonly IInventoryManager manager;

        public List<InventoryItemViewModel> Items { get; set; }

        public ICommand CreateReportCommand { get; }

        public InventoryViewModel(IInventoryManager manager)
        {
            this.manager = manager;
            CreateReportCommand = new MvxCommand(() => ShowViewModel<CreateInventoryViewModel>());
        }

        public async Task Init()
        {
            await ExecuteSafe(async () =>
            {
                var items = await manager.GetInventoryReports();
                Items = items.Select(s => new InventoryItemViewModel(s)).OrderByDescending(o => o.Id).ToList();
                RaisePropertyChanged(nameof(Items));
            });
        }
    }
}
