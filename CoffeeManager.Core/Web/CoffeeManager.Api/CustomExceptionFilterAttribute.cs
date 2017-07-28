using System;
using System.Web.Http.Filters;



namespace CoffeeManager.Api
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            try
            {
                var entities = new CoffeeRoomEntities();
                var error = new Error()
                {
                    Date = DateTime.Now,
                    Method = actionExecutedContext.Request.Method.Method,
                    Url = actionExecutedContext.Request.RequestUri.ToString(),
                    Exception = actionExecutedContext.Exception.ToString().Substring(0, 1023)
                };
                entities.Errors.Add(error);
                entities.SaveChanges();
            }
            catch (Exception ex)
            {

            }

        }
    }
}