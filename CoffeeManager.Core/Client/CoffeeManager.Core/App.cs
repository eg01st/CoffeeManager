using Acr.UserDialogs;
using CoffeeManager.Core.ViewModels;
using CoffeManager.Common;
using MvvmCross.Platform;

namespace CoffeeManager.Core
{
    public class App : CoffeManagerApp
    {      
        public override void Initialize()
        {
            base.Initialize();
            RegisterNavigationServiceAppStart<SplashViewModel>();
        }

        public override void DoRegisterInjections()
        {
            base.DoRegisterInjections();
            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            InitDataBase();
        }

        private void InitDataBase()
        {
            var manager = Mvx.Resolve<ISyncManager>();
            manager.InitDataBaseConnection();
            manager.CreateSyncTables();
        }

    }
}
