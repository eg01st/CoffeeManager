using CoffeManager.Common;
using MvvmCross.Platform.IoC;

namespace CoffeeManagerAdmin.Core
{
    public class App : CoffeManagerApp
    {
        public override void Initialize()
        {
            base.Initialize();

            RegisterAppStart<ViewModels.LoginViewModel>();
        }

    }
}
