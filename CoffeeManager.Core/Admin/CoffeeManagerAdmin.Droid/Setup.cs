using Android.Content;
using MvvmCross.Core.ViewModels;
using MobileCore.Droid;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;

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
        
        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new MvxAppCompatViewPresenter(AndroidViewAssemblies);
        }

    }
}
