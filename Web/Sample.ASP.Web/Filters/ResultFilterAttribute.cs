using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Sample.ASP.Web.Filters
{
    public class ResultFilterAttribute : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
