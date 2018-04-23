using System;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.Core.ViewModels.Settings;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using MobileCore.iOS.ViewControllers;
using UIKit;
using MvvmCross.Binding.BindingContext;

namespace CoffeeManagerAdmin.iOS
{
    public partial class CreateClientView : ViewControllerBase<CreateClientViewModel>
    {
        public CreateClientView() : base("CreateClientView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        protected override void DoBind()
        {
            var set = this.CreateBindingSet<CreateClientView, CreateClientViewModel>();
            set.Bind(EmailTextField).To(vm => vm.Email);
            set.Bind(PasswordTextField).To(vm => vm.Password);
            set.Bind(ConfirmPasswordTextField).To(vm => vm.ConfirmPassword);
            set.Bind(ApiUrlTextField).To(vm => vm.ApiUrl);
            set.Bind(RegisterButton).To(vm => vm.RegisterUserCommand);
            set.Apply();
        }

    }
}

