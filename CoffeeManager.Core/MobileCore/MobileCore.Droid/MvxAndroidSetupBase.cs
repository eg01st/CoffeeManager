using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Android.Content;
using MobileCore.Email;
using MobileCore.Email.Droid;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;
using MobileCore.Logging;
using MobileCore.Connection;
using MobileCore.Connection.Droid;
using MobileCore.AutoUpdate;
using MobileCore.Droid.AutoUpdate;
using MobileCore.Droid.Bindings;
using MobileCore.Droid.Common;
using MobileCore.Droid.Controls;
using MobileCore.Droid.Logging;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Droid.Support.V7.AppCompat;
using ConsoleLogger = MobileCore.Droid.Logging.ConsoleLogger;

namespace MobileCore.Droid
{
    public abstract class MvxAndroidSetupBase : MvxAppCompatSetup
    {
        public MvxAndroidSetupBase(Context applicationContext) : base(applicationContext)
        {
        }


        protected override void InitializeIoC()
        {
            base.InitializeIoC();
            RegisterInjections();
            DoRegisterInjections();
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies
        {
            get
            {
                var assemblies = base.AndroidViewAssemblies.ToList();
                assemblies.Add(typeof(EndlessRecyclerView).Assembly);
                return assemblies;
            }
        }


        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);
            registry.RegisterCustomBindingFactory<EndlessRecyclerView>(BindingConstants.LoadMore, view => new EndlessListLoadMoreTargetBinding(view));
        }

        private void RegisterInjections()
        {
            Mvx.RegisterSingleton<IUpdateProvider>(new UpdateProvider());
            Mvx.RegisterSingleton<IEmailService>(new EmailService());
            Mvx.RegisterSingleton<IDiagnosticLogger>(new DroidDiagnosticLogger("DS"));
            Mvx.RegisterSingleton<IConsoleLogger>(new ConsoleLogger("DS"));
            Mvx.RegisterSingleton<IConnectivity>(new Connectivity());
        }

        protected virtual void DoRegisterInjections()
        {
            
        }
    }
}
