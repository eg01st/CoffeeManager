using System.ComponentModel;
using Android.Graphics;
using Android.OS;
using Android.Views;
using MobileCore.Droid.Bindings.CustomAtts;
using MobileCore.Droid.Extensions;
using MobileCore.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace MobileCore.Droid.Activities
{
      public abstract class ActivityBase<TViewModel> : MvxAppCompatActivity<TViewModel>
        where TViewModel : PageViewModel
    {
        public const int DefaultResourceId = -1;

        private readonly int contentResourceId = DefaultResourceId;
        private Color statusBarColor;
        private Unbinder viewUnbinder;

        protected ActivityBase()
        {
        }

        protected ActivityBase(int contentResourceId)
        {
            this.contentResourceId = contentResourceId;
        }

        public Color StatusBarColor
        {
            get
            {
                return statusBarColor;
            }

            set
            {
                statusBarColor = value;
                UpdateStatusBarColor();
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            if (contentResourceId > DefaultResourceId)
            {
                SetContentView(contentResourceId);
                viewUnbinder = ViewBinder.Bind(this);
            }

            InitViewPropertiesPrivate(bundle);

            if (bundle != null)
            {
                DoOnRestoreInstanceState(bundle);
            }

            DoOnCreate(bundle);

            DoBind();
        }

        protected override void OnStart()
        {
            base.OnStart();

            UnsubscribeFromEvents();
            SubscribeToEvents();
            DoOnStart();
        }

        protected override void OnPause()
        {
            base.OnPause();

            DoOnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            UnsubscribeFromEvents();
            SubscribeToEvents();
            DoOnResume();
        }

        protected override void OnStop()
        {
            base.OnStop();

            DoOnStop();
            UnsubscribeFromEvents();
        }

        protected override void OnDestroy()
        {
            DoOnDestroy();
            ViewModel?.Unsubscribe();
            base.OnDestroy();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            DoOnSaveInstanceState(outState);

            base.OnSaveInstanceState(outState);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);

            DoOnRestoreInstanceState(savedInstanceState);
        }

        public override void OnBackPressed()
        {
            ViewModel?.CloseCommand.Execute(null);
            DoOnBackPressed();
        }

        public override void Finish()
        {
            base.Finish();
            DoOnFinish();
            ViewModel.Unsubscribe();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                DoDispose();
                viewUnbinder?.Unbind();
            }

            base.Dispose(disposing);
        }

        protected virtual void InitFieldsFromContainer(IContainer container)
        {
        }

        protected virtual void DoBind()
        {
        }

        private void InitViewPropertiesPrivate(Bundle bundle)
        {
            InitViewProperties(bundle);
        }

        /// <summary>
        /// Link all your variables with controls from view in this function
        /// </summary>
        /// <param name="bundle">Bundle with saved states</param>
        protected virtual void InitViewProperties(Bundle bundle)
        {

        }

        /// <summary>
        /// Get some data passed from PageViewModel here
        /// </summary>
        /// <param name="bundle">Bundle with saved states</param>
        protected virtual void InitPropertiesFromViewModelBundle(Bundle bundle)
        {
        }

        protected virtual void DoOnCreate(Bundle bundle)
        {
        }

        protected virtual void DoOnStart()
        {
        }

        protected virtual void DoOnPause()
        {
        }

        protected virtual void DoOnResume()
        {
        }

        protected virtual void DoOnStop()
        {
        }

        protected virtual void DoOnDestroy()
        {
        }

        protected virtual void DoOnSaveInstanceState(Bundle outState)
        {
        }

        protected virtual void DoOnRestoreInstanceState(Bundle savedInstanceState)
        {
        }

        protected virtual void DoOnFinish()
        {
        }

        private void SubscribeToEvents()
        {
            SubscribeToLayoutEvents();
            var viewModel = ViewModel;
            if (viewModel != null)
            {
                SubscribeToViewModelEvents(viewModel);
            }
        }

        private void UnsubscribeFromEvents()
        {
            UnSubscribeFromLayoutEvents();
            var viewModel = ViewModel;
            if (viewModel != null)
            {
                UnSubscribeFromViewModelEvents(viewModel);
            }
        }

        protected virtual void SubscribeToLayoutEvents()
        {
        }

        protected virtual void SubscribeToViewModelEvents(TViewModel viewModel)
        {
        }

        protected virtual void UnSubscribeFromLayoutEvents()
        {
        }

        protected virtual void UnSubscribeFromViewModelEvents(TViewModel viewModel)
        {
        }

        protected virtual void DoOnBackPressed()
        {
        }

        protected virtual void DoDispose()
        {
        }

        public void CloseKeyboard()
        {
            ActivityExtensions.CloseKeyboard(this);
        }

        public void OpenKeyboad(View view)
        {
            ActivityExtensions.OpenKeyboard(this, view);
        }

        public TService GetSystemService<TService>(string name)
            where TService : Java.Lang.Object
        {
            var service = ContextExtensions.GetSystemService<TService>(this, name);

            return service;
        }

        private void UpdateStatusBarColor()
        {
            // clear FLAG_TRANSLUCENT_STATUS flag:
            Window.ClearFlags(WindowManagerFlags.TranslucentStatus);

            // add FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS flag to the window
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            // finally change the color
            Window.SetStatusBarColor(statusBarColor);
        }
    }
}