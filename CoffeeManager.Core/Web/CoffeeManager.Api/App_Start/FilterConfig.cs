﻿using System.Web;
using System.Web.Mvc;

namespace CoffeeManager.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new CustomExceptionFilterAttribute());
        }
    }
}