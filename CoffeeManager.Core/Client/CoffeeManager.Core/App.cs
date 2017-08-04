using Acr.UserDialogs;
using CoffeeManager.Core.ViewModels;
using CoffeeManager.Models;
using CoffeManager.Common;
using MvvmCross.Platform;

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
            var database = Mvx.Resolve<IDataBaseProvider>();
            database.CreateTableIfNotExists(typeof(Sale));
        }

    }
}
