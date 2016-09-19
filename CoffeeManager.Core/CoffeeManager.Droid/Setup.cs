using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CoffeeManager.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;

namespace CoffeeManager.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
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
    }
}