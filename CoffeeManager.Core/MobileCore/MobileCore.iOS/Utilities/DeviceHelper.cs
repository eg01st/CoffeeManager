using System;
using UIKit;

namespace MobileCore.iOS
{
    public class DeviceHelper
    {
        public static bool IsIos11AndGreater => UIDevice.CurrentDevice.CheckSystemVersion(11, 0);
    }
}
