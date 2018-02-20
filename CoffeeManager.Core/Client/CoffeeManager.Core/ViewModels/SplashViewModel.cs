using System;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Core.ViewModels;
using CoffeeManager.Models;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using MobileCore.Connection;
using MobileCore.Extensions;

namespace CoffeeManager.Core
{
    public class SplashViewModel : ViewModelBase
    {
        readonly IAccountManager accountManager;
        readonly ILocalStorage localStorage;
        readonly IShiftManager shiftManager;

        private readonly IConnectivity connectivity;

        public SplashViewModel(IAccountManager accountManager, ILocalStorage localStorage, IShiftManager shiftManager, IConnectivity connectivity)
        {
            this.connectivity = connectivity;
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
                if(!await connectivity.HasInternetConnectionAsync && userInfo.AccessToken.IsNotNull())
                {
                    BaseServiceProvider.SetAccessToken(userInfo.AccessToken, userInfo.ApiUrl);
                    Config.ApiUrl = userInfo.ApiUrl;
                }
                else
                {
                    try
                    {
                        var token = await accountManager.Authorize(userInfo.Login, userInfo.Password);
                        userInfo.AccessToken = token;
                        userInfo.ApiUrl = Config.ApiUrl;
                        localStorage.SetUserInfo(userInfo);
                    }
                    catch (Exception ex)
                    {
                        await NavigationService.Navigate<InitialLoginViewModel>();
                        return;
                    }
                }
            }

            var currentShift = await ExecuteSafe(shiftManager.GetCurrentShift, null, null, false);
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
