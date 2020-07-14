using System;
using MobileCore.Logging;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MobileCore.AutoUpdate;

namespace MobileCore
{
    public class MvxApplicationBase : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            RegisterInjections();
            DoRegisterInjections();
        }

        private void RegisterInjections()
        {
            Mvx.RegisterType<IUpdateAppWorker, UpdateAppWorker>();
        }

        public virtual void DoRegisterInjections()
        {
            
        }
    }
}
