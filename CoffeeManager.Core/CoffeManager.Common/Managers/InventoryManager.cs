using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.AutoOrder;
using CoffeManager.Common.Common;
using CoffeManager.Common.Database;
using CoffeManager.Common.Providers;
using MvvmCross.Core.Navigation;
using MvvmCross.Platform;

namespace CoffeManager.Common.Managers
{
    public class InventoryManager : BaseManager, IInventoryManager
    {
        readonly IInventoryProvider provider;
        private readonly IDataBaseProvider _dataBaseProvider;
        private IUserDialogs userDialogs => Mvx.Resolve<IUserDialogs>();
        private readonly IMvxNavigationService navigationService;

        public InventoryManager(IInventoryProvider provider, 
            IDataBaseProvider dataBaseProvider,
            IMvxNavigationService navigationService)
        {
            this.provider = provider;
            _dataBaseProvider = dataBaseProvider;
            this.navigationService = navigationService;
        }

        public async Task<IEnumerable<SupliedProduct>> GetInventoryItems()
        {
            return await provider.GetInventoryItems();
        }

        public async Task<IEnumerable<InventoryItem>> GetInventoryReportDetails(int reportId)
        {
            return await provider.GetInventoryReportDetails(reportId);
        }

        public async Task<IEnumerable<InventoryReport>> GetInventoryReports()
        {
            return await provider.GetInventoryReports();
        }

        public async Task SentInventoryInfo(IEnumerable<InventoryItem> items)
        {
            await provider.SentInventoryInfo(items);
        }

        public async Task ToggleItemInventoryEnabled(int suplyProductId)
        {
            await provider.ToggleItemInventoryEnabled(suplyProductId);
        }

        public void SaveReportItem(InventoryItem item)
        {
            InventoryItemEntity entity = new InventoryItemEntity
            {
                Id = item.Id,
                SuplyProductId = item.SuplyProductId,
                CoffeeRoomNo = item.CoffeeRoomNo,
                QuantityAfer = item.QuantityAfer,
                QuantityBefore = item.QuantityBefore,
                SuplyProductName = item.SuplyProductName
            };
            _dataBaseProvider.Add(entity);
        }

        public IEnumerable<InventoryItem> GetSavedItems()
        {
            return _dataBaseProvider.Get<InventoryItemEntity>();
        }

        public void RemoveSavedItems()
        {
            _dataBaseProvider.ClearTable<InventoryItemEntity>();
        }

        public async Task<IEnumerable<InventoryItemsInfoForShiftDTO>> GetInventoryItemsForShiftToUpdate()
        {
            return await provider.GetInventoryItemsForShiftToUpdate();
        }

        public async Task SendInventoryItemsForShiftToUpdate(List<SupliedProduct> dto)
        {
            await provider.SendInventoryItemsForShiftToUpdate(dto);
        }
    }
}
