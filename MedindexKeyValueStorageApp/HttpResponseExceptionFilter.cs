using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MedindexKeyValueStorageApp.Api
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        // This allows other filters to run at the end of the pipeline.
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is not null)
            {
                context.Result = context.Exception switch
                {
                    KeyNotFoundException => new NotFoundResult(),
                    _ => new StatusCodeResult(500)
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
