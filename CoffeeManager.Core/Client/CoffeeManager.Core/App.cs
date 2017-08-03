using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using CoffeeManager.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Plugins;

namespace CoffeeManager.Core
{
    public class App : MvxApplication
    {      
        private static Timer timer = new Timer(t => RequestExecutor.Run(), null, 0, 30000);
        public override void Initialize()
        {
            base.Initialize();
            RegisterAppStart<LoginViewModel>();

            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
        }

    }
}
