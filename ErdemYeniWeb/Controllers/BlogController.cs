using ErdemYeniWeb.Models;
using ErdemYeniWeb.Models.Blog;
using ErdemYeniWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Filters;
using System.Web.Mvc;
using ErdemYeniWeb.Filters;
using ErdemYeniWeb.App_Classes;
using ErdemYeniWeb.Models.Site;

namespace ErdemYeniWeb.Controllers
{
    [ExceptionFilter]
    [LogFilter]
    public class BlogController : Controller
    {
        Context ctx = new Context();
        public ActionResult Index(string search, string tag, int id=1 ){

            BlogAllBlogsModel model = new BlogAllBlogsModel();
            List<Blog> filteredBlogs = ctx.Blogs.ToList();

            if (search != null){ 
                filteredBlogs = filteredBlogs.Where(x => x.Title.ToLower().Contains(search.Trim().ToLower()) || x.Summary.Contains(search) || x.Content.Contains(search)).ToList();
                model.search = search;
            }
            if (tag != null){ 
                Tag tagObj = ctx.Tags.SingleOrDefault(x => x.SeoUrl == tag);
                if (tagObj == null) { // eğer istenilen tag yoksa, ana sayfa geri döndürülür
                    return Index(search, null, 1);
                }
                filteredBlogs = filteredBlogs.Where(x => x.BlogTags.Any(y => y.Tag.SeoUrl == tag)).ToList();
                model.tag = tag;
            }
            model.itemCount = filteredBlogs.Count();
            int max = 5;
            if (filteredBlogs.Count() < ((id - 1) * 5) + 5){ // istenilen sayfadaki(id) blogları getirebilmek için max indeks bulunur. min zaten id*5 'tir.
                max = filteredBlogs.Count() - ((id - 1) * 5);
            }
            try
            {
                model.blogs = filteredBlogs.OrderByDescending(x => x.CreateDate).ToList().GetRange((id - 1) * 5, max);
            }
            catch (Exception)
            {
                model.blogs = new List<Blog>();//eğer hiç blog dönmezse boş göndeririz.
            }
            model.totalPage = Convert.ToInt16(Math.Ceiling(filteredBlogs.Count() / 5.0f));
            model.applyMax5Paging(model.totalPage, id);// blog sayfasında aşağıda sayfa numaralaması yapan algoritma.(Benim yazdığım algoritma.)

            //sayfanın geri kalanındaki bölümlere gerekli veriler eklenir
            model.menuModel = new BlogMenuModel(){
                user = ctx.Users.FirstOrDefault(),
                populerBlogs = ctx.Blogs.OrderByDescending(x => x.Views).Take(4).ToList(),
                allTags = ctx.Tags.Where(x=> x.BlogTags.Count>0).ToList() // bloğa sahip olan taglar alınır sadece
            };

            return View(model);
        }
        public ActionResult Entry(string id)
        {
            BlogEntryModel model = new BlogEntryModel();
            model.blog = ctx.Blogs.SingleOrDefault(x => x.SeoUrl == id);
            if (model.blog == null)
            {
                throw new MyException("Aradığınız Blog Bulunamamıştır.");
            }

            //sayfanın geri kalanındaki bölümlere gerekli veriler eklenir
            model.menuModel = new BlogMenuModel()
            {
                user = ctx.Users.FirstOrDefault(),
                populerBlogs = ctx.Blogs.OrderByDescending(x => x.Views).Take(4).ToList(),
                allTags = ctx.Tags.Where(x => x.BlogTags.Count > 0).ToList() // bloğa sahip olan taglar alınır sadece
            };

            List<Blog> copyOfBlogs = ctx.Blogs.OrderBy(x => x.CreateDate).ToList();

            //gösterilen blog'un altına bir sonraki ve önceki blogları göstermek için aşağıdakiler yapılır
            int selectedBlogIndex = copyOfBlogs.IndexOf(model.blog); 
            if (selectedBlogIndex == 0)//ilk blogsa
                model.beforeBlog = null;
            else
                model.beforeBlog = copyOfBlogs[selectedBlogIndex - 1 ];

            if (ctx.Blogs.Count() - 1 == selectedBlogIndex)// son blogsa
                model.afterBlog = null;
            else
                model.afterBlog = copyOfBlogs[selectedBlogIndex + 1 ];

            //blog'a bakıldığı için blog gösterim sayısı artırılır. Ama bu kişi son 5dk içinde tekrar bakmışsa artırılmaz. Cache' kontrol edilir.
            if (MyCacheRegistration.checkAndAddRecord(HttpContext.Cache, HttpContext.Request.UserHostAddress, model.blog.Id.ToString(), RegistrationType.View)) // if client before view this blog , than we not increast view.
            {
                model.blog.Views += 1;
            }

            return View(model);
        }
        public ActionResult CancelFollowingBlog(string id){

            if (id != null || id.Trim() != "") // gelen url parametresi boş değilse.
            {
                MyCacheBlogFollowingCancel cancelObject = MyCacheBlogFollowingCancel.resolveTheCacheTicket(HttpContext.Cache, id);// Cache'i kontrol et mail ile ona blog abonelik iptal bileti atmış mıyız.
                if (cancelObject != null)// bilet atmışsak
                {
                    if (cancelObject.isOnlyOneBlogCancel)//sadece o blog için aboneliği kapatmak istiyorsa onu kapat.
                    {
                        Comment comment = ctx.Comments.FirstOrDefault(x => x.Id == cancelObject.commentId);
                        List<Comment> comments = ctx.Comments.Where(x => x.Blog.Id == comment.Blog.Id && x.PersonMail == comment.PersonMail).ToList();
                        foreach (Comment cm in comments) // o blogtaki tüm yorumları için abonelik deaktif olur.
                        {
                            cm.IsAllowFollowing = false;
                        }
                    }
                    else//tüm bloglar için aboneliği kapat ise
                    {
                        Comment comment = ctx.Comments.FirstOrDefault(x => x.Id == cancelObject.commentId);
                        List<Comment> comments = ctx.Comments.Where(x => x.PersonMail == comment.PersonMail).ToList();
                        foreach (Comment cm in comments) // tüm blog taki tüm yorumları için aboneliği deaktif yap.
                        {
                            cm.IsAllowFollowing = false;
                        }
                    }
                    ctx.SaveChanges();
                }
                else {
                    throw new MyException("Gönderilen İstek Hatalı Yada Zaman Aşımına Uğramıştır.");
                }
            }
            else {
                throw new MyException("Bu Alana URL Parametresiz Girilemez.");
            }
            id = "";
            return RedirectToAction("Index");
        }

        public JsonResult SendReply(string seoUrl, string name, string email, string message, bool isWantFollow)
        {
            try
            {
                User userSession = (User)Session["Kullanici"]; // admin açık mı, kullanıcıyı session'dan al

                Blog blog = ctx.Blogs.SingleOrDefault(x => x.SeoUrl == seoUrl);
                if (blog == null)
                {
                    return Json(new { hasError = true, message = "İşlem Yaptığınız Blog Bulunamamıştır." });
                }
                //cache control
                if (userSession == null) { // eğer session'da kullanıcı varsa , admin ise yorum cache'leme yapma. sınırsız defa yorum atabilir.
                    if (MyCacheRegistration.checkAndAddRecord(HttpContext.Cache, HttpContext.Request.UserHostAddress, blog.Id.ToString(), RegistrationType.Comment) == false) // if client before comment this blog , than we record. if he comment yakın zamanda, than we block this comment.
                    {
                        return Json(new { hasError = true, message = "Tekrar Yorum Yapabilmeniz İçin Son Yorumdan Sonra 5 Dakika Beklemelisiniz." });
                    }
                }

                Comment comment = new Comment();
                comment.Id = Guid.NewGuid();
                comment.PersonName = name;
                comment.PersonMail = email;
                comment.Content = message;
                comment.CreateDate = DateTime.Now;
                //admin mi kontrol
                if (userSession != null) { 
                    comment.IsAdmin = true;
                    comment.PersonName = userSession.Name;
                    comment.PersonMail = userSession.Email;
                }
                else
                    comment.IsAdmin = false;
                comment.Verifery = false || comment.IsAdmin; // if is admin , so true;
                comment.IsAllowFollowing = isWantFollow;
                comment.Blog = blog;
                ctx.Comments.Add(comment);
                ctx.SaveChanges();

                if (comment.IsAdmin) { // adminse yorum direkt kabul edileceğinden, ilgili kişilere yorum at.
                    AdminController.sendInformationMailToFollowersForNewReplyWhenAccept(HttpContext.Cache,comment.Blog, comment);
                }

                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { hasError = true, message = e.Message });
            }
        }
    }
}