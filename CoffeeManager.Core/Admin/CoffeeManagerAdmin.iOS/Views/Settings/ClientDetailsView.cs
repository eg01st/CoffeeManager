using System;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.iOS.Views.Abstract;
using UIKit;
using MvvmCross.Binding.BindingContext;

namespace CoffeeManagerAdmin.iOS
{
    public partial class ClientDetailsView : ViewControllerBase<ClientDetailsViewModel>
    {
        public ClientDetailsView() : base("ClientDetailsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        protected override void DoBind()
        {
            var set = this.CreateBindingSet<ClientDetailsView, ClientDetailsViewModel>();
            set.Bind(ClientNameLabel).To(vm => vm.Email);
            set.Bind(ApiUrlLabel).To(vm => vm.ApiUrl);
            set.Bind(DeleteUserButton).To(vm => vm.DeleteUserCommand);
            set.Apply();
        }
    }
}

