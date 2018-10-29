﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CoffeeManager.Models;
using CoffeManager.Common.Managers;
using CoffeManager.Common.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.AutoOrder
{
    public class SelectSuplyProductsViewModel : BaseSearchViewModel<SelectSuplyProductItemViewModel>, IMvxViewModelResult<IEnumerable<SupliedProduct>>
    {
        private readonly ISuplyProductsManager suplyProductsManager;
        public TaskCompletionSource<object> CloseCompletionSource { get; set; }
        
        public ICommand DoneCommand { get; }

        public SelectSuplyProductsViewModel(ISuplyProductsManager suplyProductsManager)
        {
            this.suplyProductsManager = suplyProductsManager;
            DoneCommand = new MvxAsyncCommand(DoDone);
        }

        private async Task DoDone()
        {
            var selectedItems = Items.Where(i => i.IsSelected).Select(s => s.SupliedProduct);
            await NavigationService.Close(this, selectedItems);
        }

        public override async Task<List<SelectSuplyProductItemViewModel>> LoadData()
        {
            var items = await ExecuteSafe(async () => await suplyProductsManager.GetSuplyProducts());
            return items.Select(s => new SelectSuplyProductItemViewModel(s)).ToList();
        }
    }
}