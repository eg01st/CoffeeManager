using System;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using UIKit;
using MvvmCross.Platform;
using MobileCore.Email;
using MobileCore.Logging;
using MobileCore.Connection;
using MobileCore.Connection.iOS;
using MobileCore.Email.iOS;

namespace MobileCore.iOS
{
    public abstract class MvxIosSetupBase : MvxIosSetup
    {
        public MvxIosSetupBase(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        public MvxIosSetupBase(MvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter)
            : base(applicationDelegate, presenter)
        {
        }

        protected override void InitializeIoC()
        {
            base.InitializeIoC();
            RegisterInjections();
            DoRegisterInjections();
        }

        private void RegisterInjections()
        {
            Mvx.RegisterSingleton<IEmailService>(new EmailService());
            Mvx.RegisterSingleton<IDiagnosticLogger>(new IosDiagnosticLogger("DS"));
            Mvx.RegisterSingleton<IConsoleLogger>(new ConsoleLogger("DS"));
            Mvx.RegisterSingleton<IConnectivity>(new Connectivity());
        }

        protected virtual void DoRegisterInjections()
        {

        }
    }
}
