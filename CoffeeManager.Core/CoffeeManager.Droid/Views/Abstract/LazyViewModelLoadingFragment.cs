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
using MvvmCross.Droid.Support.V4;

namespace CoffeeManager.Droid.Views.Abstract
{
    public abstract class LazyViewModelLoadingFragment<TViewModel> : MvxFragment<TViewModel> where TViewModel : ViewModelBase
    {
       
        protected LazyViewModelLoadingFragment()
        {
        }


        public Func<TViewModel> ViewModelLoadingFactory { get; set; }

        //NOTE: doing viewmodel loading here because viewpager loads more than 1 fragment by default
        public override bool UserVisibleHint
        {
            get { return base.UserVisibleHint; }
            set
            {
                base.UserVisibleHint = value;
                if (value && ViewModel == null)
                {
                    ViewModel = ViewModelLoadingFactory();
                }
            }
        }
    }
}