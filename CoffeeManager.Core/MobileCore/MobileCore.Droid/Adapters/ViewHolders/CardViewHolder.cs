using Android.Views;
using MobileCore.Droid.Bindings.CustomAtts;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace MobileCore.Droid.Adapters.ViewHolders
{
    public class CardViewHolder : MvxRecyclerViewHolder
    {
        private Unbinder unbinder;

        public CardViewHolder(View view, IMvxAndroidBindingContext context)
            : base(view, context)
        {
            Init(view);
            this.DelayBind(BindData);
        }

        private void Init(View view)
        {
            unbinder = ViewBinder.Bind(view, this);
            DoInit(view);
            SetData(DataContext);
        }

        protected virtual void DoInit(View view)
        {
        }

        public virtual void ClearAnimation()
        {
            ItemView.ClearAnimation();
        }

        public override void OnViewRecycled()
        {
            base.OnViewRecycled();
            ClearAnimation();
        }

        public virtual void BindData()
        {
        }

        public virtual void SetData(object dataContext)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            unbinder.Unbind();
        }
    }
}