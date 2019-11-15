using ErdemYeniWeb.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErdemYeniWeb.Filters
{
    public class SecurityFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            User kullanici = (User)HttpContext.Current.Session["Kullanici"];
            if (kullanici == null) {
                filterContext.Result = new RedirectResult("/Index");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}