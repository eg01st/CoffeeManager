using System;
using Android.Views;
using MvvmCross.Binding;
using MvvmCross.Binding.Droid.Target;
using MvvmCross.Core.ViewModels;

namespace CoffeeManager.Droid.Bindings
{
    public class LongPressEventBinding : MvxAndroidTargetBinding
    {
        private readonly View _view;
        private IMvxCommand _command;

        public LongPressEventBinding(View view) : base(view)
        {
            _view = view;
            _view.LongClick += ViewOnLongClick;
        }

        private void ViewOnLongClick(object sender, View.LongClickEventArgs eventArgs)
        {
            if (_command != null)
            {
                _command.Execute();
            }
        }

        protected override void SetValueImpl(object target, object value)
        {
            _command = (IMvxCommand)value;
        }

        public override void SetValue(object value)
        {
            _command = (IMvxCommand)value;
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _view.LongClick -= ViewOnLongClick;
            }
            base.Dispose(isDisposing);
        }

        public override Type TargetType
        {
            get { return typeof(IMvxCommand); }
        }

        public override MvxBindingMode DefaultMode
        {
            get { return MvxBindingMode.OneWay; }
        }
    }
}