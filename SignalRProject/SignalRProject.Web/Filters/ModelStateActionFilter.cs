using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SignalRProject.BusinessLogic.Common.Extensions;
using System;

namespace SignalRProject.Web.Filters
{
    public class ModelStateActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var message = context.ModelState.GetFirstError();
                context.Result = new BadRequestObjectResult(message);
            }
        }

    }
}
