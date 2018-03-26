using Acr.UserDialogs;
using CoffeManager.Common;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;

namespace CoffeeManagerAdmin.Core
{
    public class App : CoffeManagerApp
    {
        public override void Initialize()
        {
            base.Initialize();

            RegisterNavigationServiceAppStart<ViewModels.LoginViewModel>();
        }

        public override void DoRegisterInjections()
        {
            base.DoRegisterInjections();
            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
        }

    }
}
