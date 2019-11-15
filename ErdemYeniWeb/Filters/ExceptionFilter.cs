using ErdemYeniWeb.App_Classes;
using ErdemYeniWeb.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErdemYeniWeb.Filters
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            if (!(exceptionContext.Exception is MyException)) { // eğer Mantıksal hata değilse Sistem Hataları olan ExceptionLog'a kaydet. Değilse kaydetme.
                new ExceptionLog(exceptionContext.Exception.Message, exceptionContext.Exception.StackTrace, exceptionContext.HttpContext.Timestamp); // Exception log kaydı (Sistem Hataları İçin.)
                exceptionContext.Controller.TempData["ExceptionContext"] = "Bir Sorunla Karşılaşıldı. Bildirim Bize Geldi. En Kısa Zamanda Düzeltilecektir.";
            }
            else {
                exceptionContext.Controller.TempData["ExceptionContext"] = exceptionContext.Exception.Message;
            }
            exceptionContext.ExceptionHandled = true;
            exceptionContext.Result = new RedirectResult("/Index/ErrorPage");
        }
    }
}