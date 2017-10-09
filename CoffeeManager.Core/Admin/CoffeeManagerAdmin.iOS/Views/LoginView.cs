using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core.ViewModels;
using Foundation;

namespace CoffeeManagerAdmin.iOS
{
    public partial class LoginView : ViewControllerBase<LoginViewModel>
	{
		public LoginView () : base ("LoginView", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            NavigationController.NavigationBar.Translucent = false;

			var set = this.CreateBindingSet<LoginView, LoginViewModel> ();
			set.Bind(LoginText).To(vm => vm.Name);
			set.Bind(PasswordText).To(vm => vm.Password);
			set.Bind(LoginButton).To(vm => vm.LoginCommand);
			set.Apply();

			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

