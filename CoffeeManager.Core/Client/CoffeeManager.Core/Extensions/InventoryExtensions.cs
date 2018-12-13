using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using CoffeeManager.Core.ViewModels.Inventory;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.AutoOrder;
using CoffeManager.Common;
using CoffeManager.Common.Common;
using CoffeManager.Common.Managers;
using MobileCore.Email;
using MobileCore.Logging;
using MvvmCross.Core.Navigation;
using MvvmCross.Platform;

namespace CoffeeManager.Core.Extensions
{
    public static class InventoryExtensions
    {
        public static async Task<bool> CheckInventory(bool force = false)
        {
            var inventoryManager = Mvx.Resolve<IInventoryManager>();
            var userDialogs = Mvx.Resolve<IUserDialogs>();
            
            var itemsToUpdate = (await inventoryManager.GetInventoryItemsForShiftToUpdate()).ToList();
            if (!itemsToUpdate.Any())
            {
                return true;
            }

            if (force)
            {
                await userDialogs.AlertAsync(new AlertConfig()
                {
                    Message = Strings.InventoryQuantityShouldBeUpdatedBeforeEndShiftMessage,
                    OkText = Strings.SetInventoryQuantityOkTitle,
                    OnAction = async () => await ProceedInventory(itemsToUpdate, userDialogs, inventoryManager)
                });
                return true;
            }
            else
            {
                var confirm = await userDialogs.ConfirmAsync(new ConfirmConfig()
                {
                    Title = Strings.SetInventoryQuantityTitle,
                    Message = Strings.SetInventoryQuantitySubTitle,
                    OkText = Strings.SetInventoryQuantityOkTitle,
                    CancelText = Strings.SetInventoryQuantityLaterTitle
                });
                if (confirm)
                {
                    return await ProceedInventory(itemsToUpdate, userDialogs, inventoryManager);
                }
                else
                {
                    return false;
                }
            }
        }

        private static async Task<bool> ProceedInventory(IEnumerable<InventoryItemsInfoForShiftDTO> itemsToUpdate, IUserDialogs userDialogs, IInventoryManager inventoryManager)
        {
            var navigationService = Mvx.Resolve<IMvxNavigationService>();
            
            var suplyItems = itemsToUpdate.SelectMany(s => s.Items).ToList();
            var updatedItems = await navigationService.Navigate<PartialInventoryViewModel, List<SupliedProduct>, List<SupliedProduct>>(suplyItems);
            try
            {
                userDialogs.ShowLoading();
                await inventoryManager.SendInventoryItemsForShiftToUpdate(updatedItems);
            }
            catch (Exception e)
            {
                await Mvx.Resolve<IEmailService>().SendErrorEmail("CheckInventory", e.ToDiagnosticString());
                await userDialogs.AlertAsync(Strings.DefaultErrorMessage);
                ConsoleLogger.Exception(e);
                return false;
            }
            finally
            {
                userDialogs.HideLoading();
            }

            return true;
        }
    }
}