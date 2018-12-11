using Acr.UserDialogs;
using CoffeManager.Common.Managers;
using CoffeManager.Common.Providers;
using MobileCore;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.File;
using MvvmCross.Plugins.Sqlite;
using MobileCore.Connection;
using MvvmCross.Core.Navigation;

namespace CoffeManager.Common
{
    public class CoffeManagerApp : MvxApplicationBase
    {

        public override void DoRegisterInjections()
        {
            Mvx.RegisterSingleton<ILocalStorage>(new LocalStorage(Mvx.Resolve<IMvxFileStore>()));
            RegisterProviders();
            RegisterManagers();
        }

        public virtual void RegisterProviders()
        {
            Mvx.RegisterSingleton<IUserServiceProvider>(new UserServiceProvider());
            Mvx.RegisterSingleton<IShiftServiceProvider>(new ShiftServiceProvider());
            Mvx.RegisterSingleton<IPaymentServiceProvider>(new PaymentServiceProvider());
            Mvx.RegisterSingleton<IProductProvider>(new ProductProvider());
            Mvx.RegisterSingleton<IStatisticProvider>(new StatisticProvider());
            Mvx.RegisterSingleton<ISuplyOrderProvider>(new SuplyOrderProvider());
            Mvx.RegisterSingleton<ISuplyProductsProvider>(new SuplyProductsProvider());
            Mvx.RegisterSingleton<IDataBaseProvider>(new DataBaseProvider(Mvx.Resolve<IMvxSqliteConnectionFactory>()));
            Mvx.RegisterSingleton<IInventoryProvider>(new InventoryProvider());
            Mvx.RegisterSingleton<IAdminProvider>(new AdminProvider());
            Mvx.RegisterSingleton<IAccountProvider>(new AccountProvider());
            Mvx.RegisterSingleton<ICategoryProvider>(new CategoryProvider());
            Mvx.RegisterSingleton<ICoffeeCounterProvider>(new CoffeeCounterProvider());
            Mvx.RegisterSingleton<IMotivationProvider>(new MotivationProvider());
            Mvx.RegisterSingleton<IAutoOrderProvider>(new AutoOrderProvider());
        }

        public virtual void RegisterManagers()
        {
            Mvx.RegisterSingleton<ISyncManager>(new SyncManager(Mvx.Resolve<IDataBaseProvider>(),Mvx.Resolve<IProductProvider>(), Mvx.Resolve<IConnectivity>()));
            Mvx.RegisterSingleton<IUserManager>(new UserManager(Mvx.Resolve<IUserServiceProvider>()));
            Mvx.RegisterSingleton<IShiftManager>(new ShiftManager(Mvx.Resolve<IShiftServiceProvider>(), Mvx.Resolve<ISyncManager>(), Mvx.Resolve<IConnectivity>()));
            Mvx.RegisterSingleton<IPaymentManager>(new PaymentManager(Mvx.Resolve<IPaymentServiceProvider>()));
            Mvx.RegisterSingleton<IProductManager>(new ProductManager(Mvx.Resolve<IProductProvider>(), Mvx.Resolve<ISyncManager>(), Mvx.Resolve<IConnectivity>()));
            Mvx.RegisterSingleton<IStatisticManager>(new StatisticManager(Mvx.Resolve<IStatisticProvider>()));
            Mvx.RegisterSingleton<ISuplyOrderManager>(new SuplyOrderManager(Mvx.Resolve<ISuplyOrderProvider>()));
            Mvx.RegisterSingleton<ISuplyProductsManager>(new SuplyProductsManager(Mvx.Resolve<ISuplyProductsProvider>()));
            Mvx.RegisterSingleton<IInventoryManager>(new InventoryManager(Mvx.Resolve<IInventoryProvider>(), Mvx.Resolve<IDataBaseProvider>(), Mvx.Resolve<IMvxNavigationService>()));
            Mvx.RegisterSingleton<IAdminManager>(new AdminManager(Mvx.Resolve<IAdminProvider>()));
            Mvx.RegisterSingleton<IAccountManager>(new AccountManager(Mvx.Resolve<IAccountProvider>()));
            Mvx.RegisterSingleton<ICategoryManager>(new CategoryManager(Mvx.Resolve<ICategoryProvider>()));
            Mvx.RegisterSingleton<ICoffeeCounterManager>(new CoffeeCounterManager(Mvx.Resolve<ICoffeeCounterProvider>()));
            Mvx.RegisterSingleton<IMotivationManager>(new MotivationManager(Mvx.Resolve<IMotivationProvider>()));
            Mvx.RegisterSingleton<IAutoOrderManager>(new AutoOrderManager(Mvx.Resolve<IAutoOrderProvider>()));
        }
    }
}