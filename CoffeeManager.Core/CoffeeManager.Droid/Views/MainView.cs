using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CoffeeManager.Core.ViewModels;

namespace CoffeeManager.Droid.Views
{
    [Activity(Label = "MainView")]
    public class MainView : ActivityBase<MainViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main);
            // Create your application here
        }
    }
}