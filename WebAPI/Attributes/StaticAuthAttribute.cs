using Entities.Model.AnyClasses;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TSTUWebAPI.Attributes
{
    public class StaticAuthAttribute : ActionFilterAttribute
    {

        private readonly string tokencheck = SessionClass.tokenCheck;
        private readonly string token = SessionClass.token;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                context.HttpContext.Response.StatusCode = 401;
                return;
            }

            var token1 = context.HttpContext.Request.Headers["Authorization"];

            if (token1 != tokencheck || token1 != token)
            {
                context.HttpContext.Response.StatusCode = 401;
                return;
            }
        }
    }
}
