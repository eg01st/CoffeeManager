using Android.Views;

namespace MobileCore.Droid
{
    public class BoolToHiddenConverter : GenericSingletonConverter<BoolToHiddenConverter, bool, ViewStates>
    {
        public BoolToHiddenConverter() : base(Convert)
        {
        }

        private static ViewStates Convert(bool arg)
        {
            return arg ? ViewStates.Visible : ViewStates.Invisible;
        }
    }
}
