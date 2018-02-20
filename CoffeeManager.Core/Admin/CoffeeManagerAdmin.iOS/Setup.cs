using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform.Platform;
using UIKit;
using MvvmCross.Platform;
using Acr.UserDialogs;
using MobileCore.iOS;

namespace CoffeeManagerAdmin.iOS
{
    public class Setup : MvxIosSetupBase
    {
        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        public Setup(MvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter)
            : base(applicationDelegate, presenter)
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

        protected override void DoRegisterInjections()
        {
            Mvx.RegisterSingleton<IUserDialogs>(new UserDialogsImpl());
        } 

        protected override IMvxIosViewPresenter CreatePresenter()
        {
            return new CoffeeManagerAdminPresenter(ApplicationDelegate, Window);
        }
    }
}
