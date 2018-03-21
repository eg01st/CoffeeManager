using System;
using System.Threading.Tasks;
using CoffeeManager.Common;
using CoffeeManager.Models;
using CoffeManager.Common;
using CoffeManager.Common.Managers;
using MobileCore.AutoUpdate;
using MobileCore.Connection;
using MobileCore.Extensions;
using MobileCore.Logging;

namespace CoffeeManager.Core.ViewModels
{
    public class SplashViewModel : ViewModelBase
    {
        private readonly IAccountManager accountManager;
        private readonly ILocalStorage localStorage;
        private readonly IShiftManager shiftManager;

        private readonly IConnectivity connectivity;
        readonly IUpdateAppWorker updateWorker;

        public SplashViewModel(IAccountManager accountManager,
                               ILocalStorage localStorage,
                               IShiftManager shiftManager,
                               IConnectivity connectivity,
                               IUpdateAppWorker updateWorker)
        {
            this.updateWorker = updateWorker;
            this.connectivity = connectivity;
            this.shiftManager = shiftManager;
            this.localStorage = localStorage;
            this.accountManager = accountManager;
        }

        public override async Task Initialize()
        { 
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
                bool isLoggedIn = await NavigationService.Navigate<InitialLoginViewModel, bool>();
                if(!isLoggedIn)
                {
                    return;
                }
            }
            else 
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
                        
                        updateWorker.ConfigureEndpoints(Config.ApiUrl, RoutesConstants.GetCurrentAdnroidVersion, RoutesConstants.GetAndroidPackage);
                        if(await updateWorker.IsNewVersionAvailable())
                        {
                            if(await UserDialogs.ConfirmAsync("Вышла новая версия программы, рекомендуется обновление", null, "Обновить", "Отмена"))
                            {
                                await ExecuteSafe(updateWorker.Update);
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await EmailService.SendErrorEmail($"CoffeeRoomNo {Config.CoffeeRoomNo}", ex.ToDiagnosticString());
                        ConsoleLogger.Exception(ex);
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
