﻿using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Sqlite;

namespace CoffeManager.Common
{
    public class CoffeManagerApp : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            RegisterInjections();
        }

        public virtual void RegisterInjections()
        {
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
        }

        public virtual void RegisterManagers()
        {
            Mvx.RegisterSingleton<ISyncManager>(new SyncManager(Mvx.Resolve<IDataBaseProvider>(),Mvx.Resolve<IProductProvider>()));
            Mvx.RegisterSingleton<IUserManager>(new UserManager(Mvx.Resolve<IUserServiceProvider>()));
            Mvx.RegisterSingleton<IShiftManager>(new ShiftManager(Mvx.Resolve<IShiftServiceProvider>(), Mvx.Resolve<ISyncManager>()));
            Mvx.RegisterSingleton<IPaymentManager>(new PaymentManager(Mvx.Resolve<IPaymentServiceProvider>()));
            Mvx.RegisterSingleton<IProductManager>(new ProductManager(Mvx.Resolve<IProductProvider>(), Mvx.Resolve<ISyncManager>()));
            Mvx.RegisterSingleton<IStatisticManager>(new StatisticManager(Mvx.Resolve<IStatisticProvider>()));
            Mvx.RegisterSingleton<ISuplyOrderManager>(new SuplyOrderManager(Mvx.Resolve<ISuplyOrderProvider>()));
            Mvx.RegisterSingleton<ISuplyProductsManager>(new SuplyProductsManager(Mvx.Resolve<ISuplyProductsProvider>()));
        }
    }
}