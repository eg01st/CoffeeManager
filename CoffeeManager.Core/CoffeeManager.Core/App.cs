using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeManager.Core.ViewModels;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            RegisterAppStart<LoginViewModel>();
        }
    }
}
