using Android.Content;
using MvvmCross.Core.ViewModels;
using MobileCore.Droid;

namespace CoffeeManagerAdmin.Droid
{
    public class Setup : MvxAndroidSetupBase
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

    }
}
