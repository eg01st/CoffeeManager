using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;



namespace CoffeeManager.Api
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var entities = new CoffeeRoomEntities();
            var error = new Error()
            {
                Date = DateTime.Now,
                Method = actionExecutedContext.Request.Method.Method,
                Url = actionExecutedContext.Request.RequestUri.ToString(),
                Exception = actionExecutedContext.Exception.ToString()
            };
            entities.Errors.Add(error);
            entities.SaveChanges();

        }
    }
}