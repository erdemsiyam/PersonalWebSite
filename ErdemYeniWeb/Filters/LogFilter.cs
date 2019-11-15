using ErdemYeniWeb.Models;
using ErdemYeniWeb.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ErdemYeniWeb.Filters
{
    public class LogFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {}
        public void OnActionExecuted(ActionExecutedContext filterContext) 
        {
            string requestUrl = filterContext.HttpContext.Request.FilePath;
            string clientIP = HttpContext.Current.Request.UserHostAddress;
            string requestHttpMethod = filterContext.HttpContext.Request.HttpMethod;
            requestUrl += " - " + requestHttpMethod;
            using (Context ctx = new Context())
            {
                ctx.Logs.Add(new Log() { Id = Guid.NewGuid(), ClientIP = clientIP, Direction = requestUrl , ExecutionTime = DateTime.Now });
                ctx.SaveChanges();
            }
        }
        /*
        async void addLogRecord(ActionExecutedContext filterContext)
        {
            addLogRecord(filterContext);// who request the page then after we calling this async function to log. its fastter then normal.
        }
        */

    }
}