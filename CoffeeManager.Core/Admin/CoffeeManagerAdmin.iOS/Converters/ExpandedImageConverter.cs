using MobileCore.Droid;
using UIKit;
namespace CoffeeManagerAdmin.iOS
{
    public class ExpandedImageConverter : GenericSingletonConverter<ExpandedImageConverter, bool, UIImage>
    {
        public ExpandedImageConverter() : base(Convert)
        {
        }

        public static UIImage Convert(bool isExpanded)
        {
            return UIImage.FromBundle(isExpanded ? "ic_arrow_up.png" : "ic_arrow_down.png");
        }
    }
}
