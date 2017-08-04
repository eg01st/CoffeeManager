using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;

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
            //CreatableTypes()
                //.EndingWith("Provider")
                //.AsInterfaces()
                //.RegisterAsLazySingleton();
    
        }

        public virtual void RegisterManagers()
        {
            Mvx.RegisterSingleton<IUserManager>(new UserManager(Mvx.Resolve<IUserServiceProvider>()));
            Mvx.RegisterSingleton<IShiftManager>(new ShiftManager(Mvx.Resolve<IShiftServiceProvider>()));
            Mvx.RegisterSingleton<IPaymentManager>(new PaymentManager(Mvx.Resolve<IPaymentServiceProvider>()));
            Mvx.RegisterSingleton<IProductManager>(new ProductManager(Mvx.Resolve<IProductProvider>()));
            //CreatableTypes()
            //.EndingWith("Manager")
            //.AsInterfaces()
            //.RegisterAsLazySingleton();
        }
    }
}
