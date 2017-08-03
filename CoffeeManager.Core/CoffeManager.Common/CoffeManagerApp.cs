using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

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


        }

        public virtual void RegisterManagers()
        {
            
        }
    }
}
