using System;
using System.Web.Http.Filters;



namespace CoffeeManager.Api
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Log.Error(actionExecutedContext.Exception, $"{actionExecutedContext.Request.RequestUri}");
        }
    }
}