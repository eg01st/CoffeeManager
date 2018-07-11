using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeManager.Api.Helper
{
    public static class Extenstions
    {
        public static decimal GetPercentValueOf(this decimal body, decimal percent)
        {
            return body / 100 * percent;
        }
    }
}