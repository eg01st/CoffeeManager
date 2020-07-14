using System;
using System.ComponentModel;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using MobileCore.Droid.Bindings.CustomAtts;
using MobileCore.Droid.Extensions;
using MobileCore.Droid.Listeners;
using MobileCore.ViewModels;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;

namespace MobileCore.Droid.Fragments
{
    public class FragmentBase<TViewModel> : MvxFragment<TViewModel>, Toolbar.IOnMenuItemClickListener
		where TViewModel : PageViewModel
	{
		public const int DefaultResourceId = -1;

		private readonly int contentResourceId = DefaultResourceId;
		private Unbinder viewUnbinder;

		#region Constructor

		protected FragmentBase()
		{
		}

		protected FragmentBase(int contentResourceId)
		{
			this.contentResourceId = contentResourceId;
		}

		public FragmentBase(IntPtr jRef, JniHandleOwnership jho) : base(jRef, jho)
		{
		}

		#endregion

		#region Overrides

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = base.OnCreateView(inflater, container, savedInstanceState);

			if (contentResourceId > DefaultResourceId)
			{
				view = this.BindingInflate(contentResourceId, null);
				viewUnbinder = ViewBinder.Bind(view, this);
			}


			InitViewProperties(view, savedInstanceState);
			InitTransitionNames(view, this.Activity);

			if (savedInstanceState != null)
			{
				DoOnRestoreInstanceState(savedInstanceState);
			}

			var toolbarId = GetToolbarId();
			Toolbar = view.FindViewById<Toolbar>(toolbarId);

			if(Toolbar != null)
			{
				SetupToolbar();
				
			}

			DoBind();

			return view;
		}

		void SetupToolbar()
		{
			HasOptionsMenu = GetMenuResourceId() != DefaultResourceId;
			
			var toolbar = Toolbar;
			toolbar.SetOnMenuItemClickListener(this);
			toolbar.SetNavigationOnClickListener(new ViewOnClickListener(v => OnNavigationIconClick()));

			if (ToolbarHasElevation() == false)
			{
				toolbar.Elevation = 0;
			}

			if (HasNavigationIcon() == true)
			{
				toolbar.SetNavigationIcon(GetNavigationIconId());
			}

			SetToolbarTitle();
		}

		public override void OnDestroyView()
		{
			viewUnbinder?.Unbind();
			base.OnDestroyView();
		}

		public override void OnDestroy()
		{
			ViewModel?.Unsubscribe();
			base.OnDestroy();
		}

		#endregion

		#region Fragment config methods

		protected virtual void InitFieldsFromContainer(IContainer container)
		{
		}

		protected virtual void InitViewProperties(View view, Bundle bundle)
		{
		}

		protected virtual void InitTransitionNames(View view, Context context)
		{
		}

		protected virtual void InitPropertiesFromViewModelBundle(Bundle bundle)
		{
		}

		protected virtual void DoOnRestoreInstanceState(Bundle savedInstanceState)
		{
		}

		protected virtual void DoBind()
		{
		}

		#endregion

		private void OnViewFakeClick(View v)
		{
		}

		public void CloseKeyboard()
		{
			ActivityExtensions.CloseKeyboard(Activity);
		}

		public void OpenKeyboad(View view)
		{
			ActivityExtensions.OpenKeyboard(Activity, view);
		}

		public const int HomeButtonId = Android.Resource.Id.Home;

		public Toolbar Toolbar { get; private set; }

		public string ToolbarTitle
		{
			get { return Toolbar.Title; }
			set { Toolbar.Title = value; }
		}

		protected virtual int GetToolbarId() => DefaultResourceId;

		protected virtual bool ToolbarHasElevation() => false;

		protected virtual bool HasNavigationIcon() => true;

		protected virtual int GetNavigationIconId()	=> DefaultResourceId;

		public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
		{
			int menuResoruceId;
			if (GetMenuResourceId(out menuResoruceId) == false)
			{
				return;
			}

			inflater.Inflate(menuResoruceId, menu);
		}

		protected virtual bool GetMenuResourceId(out int menuResosurceId)
		{
			menuResosurceId = GetMenuResourceId();
			var isValidResourceId = menuResosurceId != DefaultResourceId;

			return isValidResourceId;
		}

		protected virtual int GetMenuResourceId() => DefaultResourceId;

		protected virtual void SetToolbarTitle()
		{
			var titleStringResourceId = GetToolbarTitleStringId();
			string toolbarTitle;
			if (titleStringResourceId == DefaultResourceId)
			{
				toolbarTitle = GetToolbarTitle();
			}
			else
			{
				toolbarTitle = GetString(titleStringResourceId);
			}

			ToolbarTitle = toolbarTitle;
		}

		protected virtual int GetToolbarTitleStringId() => DefaultResourceId;

		protected virtual string GetToolbarTitle() => string.Empty;

		public virtual bool OnMenuItemClick(IMenuItem item)
		{
			var viewModel = ViewModel;
			if (item.ItemId == HomeButtonId && viewModel != null)
			{
				viewModel.CloseCommand.Execute(null);

				return true;
			}

			return false;
		}

		protected virtual void OnNavigationIconClick()
		{
			ViewModel.CloseCommand.Execute(null);
		}
	}
}