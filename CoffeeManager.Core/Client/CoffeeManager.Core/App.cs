using Acr.UserDialogs;
using CoffeeManager.Common;
using CoffeeManager.Core.ViewModels;
using CoffeeManager.Models;
using CoffeManager.Common;
using MvvmCross.Platform;
using MvvmCross.Plugins.Sqlite;

namespace CoffeeManager.Core
{
    public class App : CoffeManagerApp
    {      
       //private static Timer timer = new Timer(t => RequestExecutor.Run(), null, 0, 30000);
        public override void Initialize()
        {
            base.Initialize();
            RegisterAppStart<LoginViewModel>();

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
