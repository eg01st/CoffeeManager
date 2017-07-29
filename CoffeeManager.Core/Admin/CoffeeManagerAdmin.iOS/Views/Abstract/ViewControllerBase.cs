using System;
using Foundation;
using MvvmCross.iOS.Views;
namespace CoffeeManagerAdmin.iOS
{
    public abstract class ViewControllerBase : MvxViewController
    {
       protected ViewControllerBase()
            :base()
        {
        }

        protected ViewControllerBase(string nibName)
            : base(nibName, null)
        {
        }

        protected ViewControllerBase(string nibName, NSBundle bundle)
            : base(nibName, bundle)
        {
        }

        protected ViewControllerBase(IntPtr ptr)
            : base(ptr)
        {
        }

        public override void ViewDidLoad()
        {
  
            base.ViewDidLoad();
            DoBind();
        }

        protected virtual void DoBind()
        {
        
        }
    }
}
