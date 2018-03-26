using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Binding.Bindings.Target.Construction;

namespace CoffeeManagerAdmin.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
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
            MvxAppCompatSetupHelper.FillTargetFactories(registry);  
        }
    }
}
