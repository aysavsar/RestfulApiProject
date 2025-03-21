using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RestfulApiProject.Attributes
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Fake bir kullanıcı kontrolü
            var isAuthenticated = context.HttpContext.Request.Headers["Authorization"] == "fake-token";

            if (!isAuthenticated)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}