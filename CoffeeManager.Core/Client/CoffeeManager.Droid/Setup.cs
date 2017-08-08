using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Android.Content;
using Android.Views;
using CoffeeManager.Core;
using CoffeeManager.Droid.Bindings;
using CoffeManager.Common;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;

namespace CoffeeManager.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
            Mvx.RegisterSingleton<IEmailService>(new EmailService());
        }


        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies
        {
            get
            {
                var assemblies = base.AndroidViewAssemblies.ToList();
                assemblies.Add(typeof(Android.Support.V7.Widget.CardView).Assembly);
                assemblies.Add(typeof(Android.Support.V4.View.ViewPager).Assembly);
                assemblies.Add(typeof(Android.Support.Design.Widget.TabLayout).Assembly);
                assemblies.Add(GetType().Assembly);

                return assemblies;
            }
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);
            registry.RegisterFactory(new MvxCustomBindingFactory<View>("LongPress", view => new LongPressEventBinding(view)));
        }

    }
}