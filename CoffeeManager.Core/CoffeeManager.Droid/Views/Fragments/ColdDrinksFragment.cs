using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using CoffeeManager.Core.ViewModels;
using CoffeeManager.Droid.Views.Abstract;
using MvvmCross.Droid.Support.V4;

namespace CoffeeManager.Droid.Views.Fragments
{
    class ColdDrinksFragment : LazyViewModelLoadingFragment<ColdDrinksViewModel>
    {
    }
}