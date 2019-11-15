using ErdemYeniWeb.Filters;
using ErdemYeniWeb.Models;
using ErdemYeniWeb.Models.Site;
using ErdemYeniWeb.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ErdemYeniWeb.Controllers
{
    [ExceptionFilter]
    [LogFilter]
    public class IndexController : Controller
    {
        Context ctx = new Context();

        public ActionResult Index()
        {
            IndexModel model = new IndexModel()
            {
                user = ctx.Users.FirstOrDefault(),
                skills = ctx.Skills.OrderByDescending(x=>x.Percent).ToList(),
                educations = ctx.Educations.ToList(),
                experiences = ctx.Experiences.ToList(),
                projects = ctx.Projects.ToList()
            };
            return View(model);
        }
        public ActionResult ErrorPage(){
            return View(TempData["ExceptionContext"]);
        }

        [HttpGet]
        public ActionResult Giris() {
            User userSession = (User)Session["Kullanici"];
            if (userSession != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
        [HttpPost]
        [ActionName("Giris")]
        public ActionResult GirisKontrol(string id, string pass){
            User user;
            try
            {
                user = ctx.Users.Single(x => x.Nickname == id && x.Password == pass);
                if (user != null)
                {
                    Session["Kullanici"] = user;
                    return RedirectToAction("Index", "Admin");
                }
            }
            catch (Exception) {
                ViewBag.Mesaj = "Hatalı Giriş";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Cikis(){

            User userSession = (User)Session["Kullanici"];
            if (userSession != null)
            {
                User user = ctx.Users.FirstOrDefault();
                user.LastConnectTime = DateTime.Now;
                ctx.SaveChanges();
                Session.Remove("Kullanici");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public FileResult DownloadCv(string lang)
        {
            if (lang != "en" && lang != "tr"){
                lang = "en";
            }
            string filePath = Server.MapPath("~/Cv/erdemsiyam_cv_" + lang + ".pdf");
            return new FilePathResult(filePath, "application/pdf"); // bu dosya client'e geri döndürülür
        }
    }
}