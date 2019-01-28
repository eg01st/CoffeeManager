using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using CoffeeManager.Core.ViewModels.Inventory;
using CoffeeManager.Models;
using CoffeeManager.Models.Data.DTO.AutoOrder;
using CoffeManager.Common.Common;
using CoffeManager.Common.Managers;
using MobileCore;
using MobileCore.Email;
using MobileCore.Logging;
using MvvmCross.Core.Navigation;
using MvvmCross.Platform;

namespace CoffeeManager.Core.Extensions
{
    public static class InventoryExtensions
    {
        public static async Task<bool> HasAutoOrders()
        {
            var autoOrderManager = Mvx.Resolve<IAutoOrderManager>();
            var orders = await autoOrderManager.GetAutoOrders();
            return orders.Any();
        }

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
               var tcs = new TaskCompletionSource<bool>();

                userDialogs.Alert(new AlertConfig()
                {
                    Message = Strings.InventoryQuantityShouldBeUpdatedBeforeEndShiftMessage,
                    OkText = Strings.SetInventoryQuantityOkTitle,
                    OnAction = async () => await ProceedInventory(itemsToUpdate, userDialogs, inventoryManager, tcs)
                });
                return await tcs.Task;
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

        private static async Task<bool> ProceedInventory(IEnumerable<InventoryItemsInfoForShiftDTO> itemsToUpdate, IUserDialogs userDialogs, IInventoryManager inventoryManager, TaskCompletionSource<bool> tcs = null)
        {
            var navigationService = Mvx.Resolve<IMvxNavigationService>();
            
            var suplyItems = itemsToUpdate.SelectMany(s => s.Items).ToList();
            var updatedItems = await navigationService.Navigate<PartialInventoryViewModel, List<SupliedProduct>, List<SupliedProduct>>(suplyItems);
            updatedItems.ForEach(i => i.SetExpenseNumerationQuantity());
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
                tcs?.SetResult(false);
                return false;
            }
            finally
            {
                userDialogs.HideLoading();
            }
            tcs?.SetResult(true);
            return true;
        }
    }
}