using Android.Views;
using Android.Widget;
using CoffeeManager.Core.ViewModels;
using CoffeeManager.Core.ViewModels.Motivation;
using CoffeeManager.Droid.Converters;
using MobileCore.Droid.Adapters.ViewHolders;
using MobileCore.Droid.Bindings.CustomAtts;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Binding.Droid;

namespace CoffeeManager.Droid
{
    public class ProductItemViewHolder : CardViewHolder
    {
        public ProductItemViewHolder(View view, IMvxAndroidBindingContext context) : base(view, context)
        {
        }

        [FindById(Resource.Id.name_text_view)] private TextView Name { get; set; }

        //[FindById(Resource.Id.price_text_view)] private TextView Price { get; set; }

        //[FindById(Resource.Id.cup_name_text_view)] private TextView CupName { get; set; }

        //[FindById(Resource.Id.discount_image)] private ImageView DiscountImage { get; set; }

        //[FindById(Resource.Id.credit_card_image)] private ImageView CreditCardImage { get; set; }

        //[FindById(Resource.Id.background_layout)] private LinearLayout BackgroundLayout { get; set; }

        //[FindById(Resource.Id.bottom_layout)] private LinearLayout BottomLayout { get; set; }

        public override void BindData()
        {
            //var bindingSet = this.CreateBindingSet<ProductItemViewHolder, ProductItemViewModel>();
            //bindingSet.Bind(Name).To(vm => vm.Name);
            //bindingSet.Bind(Price).To(vm => vm.Price).WithConversion(new DecimalToPriceConverter());
            //bindingSet.Bind(CupName).To(vm => vm.CupType).WithConversion(new CupTypeToNameConverter());
            //bindingSet.Bind(DiscountImage).For(i => i.BindVisible()).To(vm => vm.IsPoliceSale);
            //bindingSet.Bind(CreditCardImage).For(i => i.BindVisible()).To(vm => vm.IsCreditCardSale);
            //bindingSet.Bind(BackgroundLayout).For(i => i.Background).To(vm => vm.CupType).WithConversion(new CupTypeToColorConverter());
            //bindingSet.Bind(BottomLayout).For(i => i.Background).To(vm => vm.CupType).WithConversion(new CupTypeToColorConverter());
            //bindingSet.Bind(CreditCardImage).For(i => i.Visibility).To(vm => vm.CupType).WithConversion(new CupTypeToVisibility());
            //bindingSet.Apply();
        }
    }
}
