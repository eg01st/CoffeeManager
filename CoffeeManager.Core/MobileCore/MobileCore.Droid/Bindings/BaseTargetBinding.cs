using System;
using MvvmCross.Binding;
using MvvmCross.Binding.Droid.Target;

namespace MobileCore.Droid.Bindings
{
    public abstract class BaseTargetBinding<TValue, TTarget> : MvxAndroidTargetBinding
        where TTarget : class
    {
        private TTarget targetObject;

        protected BaseTargetBinding(TTarget targetObject)
            : base(targetObject)
        {
            TargetTyped = targetObject;
        }

        public override Type TargetType => typeof(TValue);

        public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

        protected TTarget TargetTyped
        {
            get { return targetObject; }
            set
            {
                targetObject = value;
                SubscribeToTargetEvents(targetObject);
            }
        }

        protected override void SetValueImpl(object target, object value)
        {
            var obj = ConvertValue(value);
            var convertedTarget = ConvertTarget(target);
            if (convertedTarget == null)
            {
                return;
            }

            DoSetValue(convertedTarget, obj);
        }

        protected virtual TValue ConvertValue(object value)
            => (TValue)value;

        protected virtual TTarget ConvertTarget(object target)
            => target as TTarget;

        protected virtual void DoSetValue(TTarget target, TValue value)
        {
        }

        protected override void Dispose(bool isDisposing)
        {
            UnsubscribeFromTargetEvents(TargetTyped);
            DoOnDispose();

            base.Dispose(isDisposing);
        }

        protected virtual void DoOnDispose()
        {
        }

        protected virtual void SubscribeToTargetEvents(TTarget target)
        {
        }

        protected virtual void UnsubscribeFromTargetEvents(TTarget target)
        {
        }
    }
}