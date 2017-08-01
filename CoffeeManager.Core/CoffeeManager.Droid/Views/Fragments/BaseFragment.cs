using Android.OS;
using Android.Views;
using CoffeeManager.Core.ViewModels;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platform;

namespace CoffeeManager.Droid.Views.Fragments
{
    public class BaseFragment<T> : MvxFragment<T> where T: ViewModelBase
    {
        private readonly IMvxViewModelLoader _mvxViewModelLoader = Mvx.Resolve<IMvxViewModelLoader>();
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet(savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.product_list, null);
            return view;
        }

        public void LoadVm()
        {
            if (ViewModel == null)
            {
                var request = new MvxViewModelRequest<T>(null, null, MvxRequestedBy.UserAction);
                var viewModel = (T)_mvxViewModelLoader.LoadViewModel(request, null);
                ViewModel = viewModel;
            }
        }
    }
}