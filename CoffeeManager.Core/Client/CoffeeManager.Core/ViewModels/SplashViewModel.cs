using System;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Core.ViewModels;
using CoffeeManager.Models;
using CoffeManager.Common;
using CoffeManager.Common.Managers;

namespace CoffeeManager.Core
{
    public class SplashViewModel : ViewModelBase
    {
        readonly IAccountManager accountManager;
        readonly ILocalStorage localStorage;
        readonly IShiftManager shiftManager;

        public SplashViewModel(IAccountManager accountManager, ILocalStorage localStorage, IShiftManager shiftManager)
        {
            this.shiftManager = shiftManager;
            this.localStorage = localStorage;
            this.accountManager = accountManager;
        }

        public async override Task Initialize()
        {
            bool isLoggedIn = false;

            var coffeeRoomNo = localStorage.GetCoffeeRoomId();
            if (coffeeRoomNo == -1)
            {
                await NavigationService.Navigate<InitialLoginViewModel, bool>();
                return;
            }
            else
            {
                Config.CoffeeRoomNo = coffeeRoomNo;
            }

            var userInfo = localStorage.GetUserInfo();
            if(userInfo == null)
            {
                isLoggedIn = await NavigationService.Navigate<InitialLoginViewModel, bool>();
                if(!isLoggedIn)
                {
                    return;
                }
            }
            else if(!isLoggedIn)
            {
                try
                {
                    await accountManager.Authorize(userInfo.Login, userInfo.Password);
                }
                catch (Exception ex)
                {
                    await NavigationService.Navigate<InitialLoginViewModel>();
                    return;
                }
            }

            var currentShift = await ExecuteSafe(shiftManager.GetCurrentShift);
            if (currentShift != null)
            {
                await NavigationService.Navigate<MainViewModel, Shift>(currentShift);
            }
            else
            {
                await NavigationService.Navigate<LoginViewModel>();
            }

        }
    }
}
