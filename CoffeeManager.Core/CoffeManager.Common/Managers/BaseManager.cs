using System;
using MobileCore.Email;
using MvvmCross.Platform;

namespace CoffeManager.Common
{
    public class BaseManager
    {
        public static int ShiftNo { get; set; }

        protected IEmailService EmailService
        {
            get
            {
                if (Mvx.CanResolve<IEmailService>())
                {
                    return Mvx.Resolve<IEmailService>();
                }
                return null;
            }
        }
    }
}
