using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Sample.ASP.Web.Filters
{
    public class ErrorHandlerFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
