using System;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using MobileCore.Droid.Bindings.CustomAtts;
using MobileCore.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace MobileCore.Droid.Activities
{
    public abstract class ActivityWithToolbar<TViewModel> : ActivityBase<TViewModel>
        where TViewModel : PageViewModel
    {
        public const int HomeButtonId = Android.Resource.Id.Home;
        private Toolbar toolbar;
        private bool upNavigationEnabled = false;

        protected ActivityWithToolbar(int contentResourceId)
            : base(contentResourceId)
        {
        }

        public IMenu Menu { get; private set; }

        protected Toolbar Toolbar => toolbar;

        public TextView ToolBarTitleTextView 
        {
            get; set;
        }

        public string ToolbarTitle
        {
            get { return ToolBarTitleTextView.Text; }
            set
            {
                if (ToolBarTitleTextView != null)
                {
                    ToolBarTitleTextView.Text = value;
                }
                else
                {
                    SupportActionBar.Title = value;
                }
            }
        }

        protected override void DoOnCreate(Bundle bundle)
        {
            base.DoOnCreate(bundle);

            var toolbarId = GetToolbarId();
            toolbar = FindViewById<Toolbar>(toolbarId);

            if (toolbar == null)
            {
                throw new ArgumentOutOfRangeException(nameof(toolbarId), $"Toolbar with id {toolbarId} not found, toolbar is mandatory for this class");
            }

            SetSupportActionBar(toolbar);
            SupportActionBar.Title = string.Empty;

            var titleStringResourceId = GetToolbarTitleStringResourceId();

            string toolbarTitle;

            if (titleStringResourceId == DefaultResourceId)
            {
                toolbarTitle = GetToolbarTitle();
            }
            else
            {
                toolbarTitle = Resources.GetString(titleStringResourceId);
            }

            ToolBarTitleTextView = FindViewById<TextView>(Resource.Id.toolbar_title);
            ToolbarTitle = toolbarTitle;

            var upNavigationIconId = GetUpNavigationIconId();

            upNavigationEnabled = upNavigationIconId != DefaultResourceId;
            var actionBar = SupportActionBar;

            if (upNavigationEnabled == true)
            {
                actionBar.SetDisplayHomeAsUpEnabled(true);
                actionBar.SetHomeAsUpIndicator(upNavigationIconId);
            }
            else
            {
                actionBar.SetDisplayHomeAsUpEnabled(false);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var menuResourceId = GetMenuResourceId();

            if (menuResourceId != DefaultResourceId)
            {
                base.MenuInflater.Inflate(menuResourceId, menu);
            }

            var flag = base.OnCreateOptionsMenu(menu);
            Menu = menu;
            return flag;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (upNavigationEnabled == true
                && item.ItemId == HomeButtonId)
            {
                OnBackPressed();

                if (ParentActivityIntent != null)
                {
                    NavUtils.NavigateUpFromSameTask(this);
                }

                return true;
            }

            return true;
        }

        protected virtual int GetMenuResourceId() => DefaultResourceId;

        protected virtual int GetToolbarTitleStringResourceId() => DefaultResourceId;

        protected virtual string GetToolbarTitle() => string.Empty;

        protected virtual int GetUpNavigationIconId() => DefaultResourceId;

        protected virtual int GetToolbarId() => Resource.Id.toolbar;
    }
}