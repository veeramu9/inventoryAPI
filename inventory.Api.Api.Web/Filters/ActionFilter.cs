using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace inventory.Api.Api.Web.Filters
{
    public class ActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            var modelState = context.ModelState;
             
            if (!modelState.IsValid)
                context.Response = context.Request
                     .CreateErrorResponse(HttpStatusCode.BadRequest, modelState);
        }
    }
}
