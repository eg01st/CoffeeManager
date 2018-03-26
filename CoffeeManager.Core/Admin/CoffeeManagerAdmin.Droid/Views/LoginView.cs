using System;
using CoffeeManagerAdmin.Core.ViewModels;
using MobileCore.Droid.Activities;

namespace CoffeeManagerAdmin.Droid
{
    public class LoginView : ActivityBase<LoginViewModel>
    {
        public LoginView() : base(Resource.Layout.login)
        {
        }
    }
}
