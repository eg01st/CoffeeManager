using System;
using System.Globalization;
using System.Text.RegularExpressions;
using CoreGraphics;
using UIKit;

namespace CoffeeManagerAdmin.iOS.Extensions
{
    public static class ColorExtensions
    {
        public static UIImage ImageFromColor(UIColor color, CGSize imageSize)
        {
            UIImage resultingImage = null;
            var rect = new CGRect(0, 0, imageSize.Width, imageSize.Height);

            UIGraphics.BeginImageContext(imageSize);

            using(var context = UIGraphics.GetCurrentContext())
            {
                color.SetFill();
                context.FillRect(rect);

                resultingImage = UIGraphics.GetImageFromCurrentImageContext();
            }

            UIGraphics.EndImageContext();

            return resultingImage;
        }

        public static UIColor ParseColorFromHex(this string hexColor)
        {
            var hexRegex = new Regex("^#([A-Fa-f0-9]{6})$");

            if(hexColor.Length != 6 && !hexRegex.IsMatch(hexColor))
            {
                throw new ArgumentException("Hex is not correct.", nameof(hexColor));
            }

            var rChannel = hexColor.Substring(1, 2);
            var gChannel = hexColor.Substring(3, 2);
            var bChannel = hexColor.Substring(5, 2);

            var rVal = byte.Parse(rChannel, NumberStyles.HexNumber);
            var gVal = byte.Parse(gChannel, NumberStyles.HexNumber);
            var bVal = byte.Parse(bChannel, NumberStyles.HexNumber);

            return UIColor.FromRGB(rVal, gVal, bVal);
        }
    }
}