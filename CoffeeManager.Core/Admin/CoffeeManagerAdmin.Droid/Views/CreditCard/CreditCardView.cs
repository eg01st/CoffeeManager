using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using CoffeeManagerAdmin.Core;
using CoffeeManagerAdmin.Core.ViewModels.CreditCard;
using CoffeeManagerAdmin.Core.ViewModels.Home;
using CoffeeManagerAdmin.Droid.Adapters.ViewHolders;
using CoffeeManagerAdmin.Droid.Views.Users;
using MobileCore.Droid.Activities;
using MobileCore.Droid.Adapters;
using MobileCore.Droid.Adapters.TemplateSelectors;
using MobileCore.Droid.Bindings.CustomAtts;
using MobileCore.Droid.Controls;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Views.Attributes;

namespace CoffeeManagerAdmin.Droid.Views.CreditCard
{
    [MvxActivityPresentation]
    [Activity(WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Portrait)]
    public class CreditCardView : ActivityWithToolbar<CreditCardViewModel>
    {
        protected override int GetToolbarTitleStringResourceId() => Resource.String.card;

        protected override int GetUpNavigationIconId() => Resource.Drawable.ic_arrow_back_white_24dp;

        [FindById(Resource.Id.recyclerview_cashout)]
        private EndlessRecyclerView cashOutRecyclerView;

        
        public CreditCardView() : base(Resource.Layout.credit_card)
        {
        }
        
        protected override void DoOnCreate(Bundle bundle)
        {
            base.DoOnCreate(bundle);
            InitRecyclerView();
        }

        
        protected override void DoBind()
        {
            var bindingSet = this.CreateBindingSet<CreditCardView, CreditCardViewModel>();
            bindingSet.Bind(cashOutRecyclerView).For(v => v.ItemsSource).To(vm => vm.CashoutItems);
            bindingSet.Bind(cashOutRecyclerView.Adapter).For(v => v.ItemClick).To(vm => vm.ItemSelectedCommand);
            bindingSet.Apply();
        }
        
        private void InitRecyclerView()
        {
            cashOutRecyclerView.Adapter = new RecycleViewBindableAdapter((IMvxAndroidBindingContext)BindingContext);

            var templateItem = TemplateSelectorItem.Produce<CashoutHistoryItemViewModel, CashoutHistoryItemViewHolder>(Resource.Layout.credit_card_cashout_item);
            cashOutRecyclerView.Adapter.ItemTemplateSelector = new TemplateSelector(templateItem);
        }
    }
}