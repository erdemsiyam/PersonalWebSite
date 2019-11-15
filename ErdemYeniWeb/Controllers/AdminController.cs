using ErdemYeniWeb.Models;
using ErdemYeniWeb.Models.Site;
using ErdemYeniWeb.Models.Blog;
using ErdemYeniWeb.ViewModels;
using ErdemYeniWeb.Filters;
using ErdemYeniWeb.App_Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Filters;
using System.Text;

namespace ErdemYeniWeb.Controllers
{
    [SecurityFilter]
    [ExceptionFilter]
    [LogFilter]
    public class AdminController : Controller
    {
        Context ctx = new Context();

        //Pages
        public ActionResult Index()
        {
            AdminIndexModel model = new AdminIndexModel();
            User user = ctx.Users.FirstOrDefault();
            List<Log> logs = ctx.Logs.Where(x => x.ExecutionTime > user.LastConnectTime).ToList();
            model.IndexClickCount = logs.Where(x => x.Direction == "/ - GET").Count();
            model.BlogEntryClickCount = logs.Where(x => x.Direction.Contains("/blog/entry")).Count();
            model.BlogClickCount = logs.Where(x => x.Direction.Contains("/blog")).Count() - model.BlogEntryClickCount;
            model.CvClickCount = logs.Where(x => x.Direction.Contains("/Index/DownloadCv")).Count();
            return View(model);
        }
        public ActionResult UserDetails()
        {
            return View(ctx.Users.FirstOrDefault()); // the model is User
        }
        public ActionResult UserInformation()
        {
            AdminUserInformationModel model = new AdminUserInformationModel()
            {
                skills = ctx.Skills.ToList(),
                educations = ctx.Educations.ToList(),
                experiences = ctx.Experiences.ToList(),
                projects = ctx.Projects.ToList()
            };

            return View(model); // the model is special ViewModel (We made it at Models/ViewModels)
        }

        public ActionResult Tags()
        {
            List<Tag> model = ctx.Tags.ToList();
            return View(model);
        }
        public ActionResult NewBlog()
        {
            List<Tag> model = ctx.Tags.ToList();
            return View(model); // model = tags
        }
        public ActionResult EditBlog(string id)
        {
            AdminEditBlogModel model = new AdminEditBlogModel();
            model.blog = ctx.Blogs.SingleOrDefault(x => x.Id == new Guid(id));
            if (model.blog == null)
            {
                throw new MyException("Blog Bulunamadı");
            }
            model.selectedTags = new List<Tag>();
            foreach (BlogTag item in model.blog.BlogTags)
            {
                model.selectedTags.Add(item.Tag);
            }
            model.tags = ctx.Tags.ToList();

            return View(model);
        }
        public ActionResult AllBlogs(int id = 1)
        {
            AdminAllBlogsModel model = new AdminAllBlogsModel();

            model.itemCount = ctx.Blogs.Count();
            int max = 20;
            if (ctx.Blogs.Count() < ((id - 1) * 20) + 20)
            {
                max = ctx.Blogs.Count() - ((id - 1) * 20);
            }
            try
            {
                model.blogs = ctx.Blogs.OrderByDescending(x => x.CreateDate).ToList().GetRange((id - 1) * 20, max);
            }
            catch (Exception)
            {
                model.blogs = new List<Blog>();// hiç blog yoksa boş liste geri gönder.
            }
            model.selectPage = id;

            model.totalPage = Convert.ToInt16(Math.Ceiling(ctx.Blogs.Count() / 20.0f));

            // page count sizing. only 5 length. (My Algorithm)
            model.applyMax5Paging(model.totalPage, id);

            return View(model);
        }
        public ActionResult Comments(int id = 1)
        {
            AdminCommentsModel model = new AdminCommentsModel();
            model.itemCount = ctx.Comments.Count();
            int max = 20;
            if (ctx.Comments.Count() < ((id - 1) * 20) + 20)
            {
                max = ctx.Comments.Count() - ((id - 1) * 20);
            }
            try
            {
                model.comments = ctx.Comments.OrderBy(x => x.Verifery).ThenByDescending(x => x.CreateDate).ToList().GetRange((id - 1) * 20, max);
            }
            catch (Exception)
            {
                model.comments = new List<Comment>();// hiç yorum yoksa boş liste geri gönder.
            }
            model.selectPage = id;

            model.totalPage = Convert.ToInt16(Math.Ceiling(ctx.Comments.Count() / 20.0f));

            // page count sizing. only 5 length. (My Algorithm)
            model.applyMax5Paging(model.totalPage, id);

            return View(model);
        }
        public ActionResult Logs(int id = 1)
        {
            AdminLogsModel model = new AdminLogsModel();

            model.itemCount = ctx.Logs.Count();
            int max = 20;
            if (model.itemCount < ((id - 1) * 20) + 20)
            {
                max = model.itemCount - ((id - 1) * 20);
            }
            try
            {
                model.logs = ctx.Logs.OrderByDescending(x => x.ExecutionTime).ToList().GetRange((id - 1) * 20, max);
            }
            catch (Exception)
            {
                model.logs = new List<Log>();// hiç log yoksa boş liste geri gönder.
            }
            model.selectPage = id;

            model.totalPage = Convert.ToInt16(Math.Ceiling(ctx.Logs.Count() / 20.0f));

            // page count sizing. only 5 length. (My Algorithm)
            model.applyMax5Paging(model.totalPage, id);

            return View(model);
        }
        public ActionResult ExceptionLogs(int id = 1)
        {
            AdminExceptionLogsModel model = new AdminExceptionLogsModel();

            model.itemCount = ctx.ExceptionLogs.Count();
            int max = 20;
            if (model.itemCount < ((id - 1) * 20) + 20)
            {
                max = model.itemCount - ((id - 1) * 20);
            }
            try
            {
                model.logs = ctx.ExceptionLogs.OrderByDescending(x => x.ExceptionTime).ToList().GetRange((id - 1) * 20, max);
            }
            catch (Exception)
            {
                model.logs = new List<ExceptionLog>();// hiç log yoksa boş liste geri gönder.
            }

            model.selectPage = id;

            model.totalPage = Convert.ToInt16(Math.Ceiling(ctx.ExceptionLogs.Count() / 20.0f));

            // page count sizing. only 5 length. (My Algorithm)
            model.applyMax5Paging(model.totalPage, id);

            return View(model);
        }

        //User-JS-Service
        public JsonResult UserPictureUpload(HttpPostedFileBase image, string type)
        {
            if (image == null)
            {
                return Json(new { hasError = true, message = "Resim Seçip Tekrar Deneyiniz." });
            }
            string imageName;
            User user = ctx.Users.FirstOrDefault();
            if (type == "big")
            {
                try
                {
                    imageName = MyImageManager.addImage(image, MyImageType.User);
                }
                catch (MyException e)
                {
                    return Json(new { hasError = true, message = e.Message });
                }
                user.BigPicturePath = imageName;
            }
            else if (type == "small")
            {
                
                try
                {
                    imageName = MyImageManager.addImage(image, MyImageType.User);
                }
                catch (MyException e)
                {
                    return Json(new { hasError = true, message = e.Message });
                }
                user.SmallPicturePath = imageName;
            }
            else
            {
                return Json(new { hasError = true, message = "Resim Tipi Seçilmedi. Seçip Tekrar Deneyiniz." });
            }
            ctx.SaveChanges();
            return Json(imageName, JsonRequestBehavior.AllowGet);
            //return Json(new { hasError = false, image = new FileStreamResult(new FileStream(Server.MapPath("/Images/User/" + imageName), FileMode.Open), "image/jpeg") });
        }
        public JsonResult UserDetailUpload(string which, string newValue)
        {
            try
            {
                User user = ctx.Users.FirstOrDefault();
                switch (which)
                {
                    case "Name":
                        user.Name = newValue;
                        break;
                    case "Degree":
                        user.Degree = newValue;
                        break;
                    case "About1":
                        user.About1 = newValue;
                        break;
                    case "About2":
                        user.About2 = newValue;
                        break;
                    case "BirthDate":
                        user.BirthDate = DateTime.ParseExact(newValue, "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "Phone":
                        user.Phone = newValue;
                        break;
                    case "Email":
                        user.Email = newValue;
                        break;
                    case "Address":
                        user.Address = newValue;
                        break;
                    case "Facebook":
                        user.Facebook = newValue;
                        break;
                    case "Twitter":
                        user.Twitter = newValue;
                        break;
                    case "Instagram":
                        user.Instagram = newValue;
                        break;
                    case "Linkedin":
                        user.Linkedin = newValue;
                        break;
                    case "Github":
                        user.Github = newValue;
                        break;
                    case "BlogSaying":
                        user.BlogSaying = newValue;
                        break;
                    case "IsAllowAutoMail":
                        user.IsAllowAutoMail = Convert.ToBoolean(newValue);
                        break;
                    case "AutoMail":
                        user.AutoMail = newValue;
                        break;
                    case "AutoMailPass":
                        user.AutoMailPass = newValue;
                        break;
                    case "AutoMailHost":
                        user.AutoMailHost = newValue;
                        break;
                    case "AutoMailPort":
                        user.AutoMailPort = newValue;
                        break; 
                    case "IsAutoMailSslEnable":
                        user.IsAutoMailSslEnable = Convert.ToBoolean(newValue);
                        break;
                }
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { hasError = true, message = e.Message });
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult UserCvUpload(HttpPostedFileBase cvFile, string language)
        {
            try
            {
                MyCvManager.cvUpload(cvFile, language);
            }
            catch (Exception e)
            {
                new ExceptionLog(e.Message, e.StackTrace, DateTime.Now); // Exception log kaydı (Sistem Hataları İçin.)
                return Json(new { hasError = true, message = e.Message });
            }
            return new JsonResult();
        }
        public JsonResult UserInformationAdd(string which, string jsonObject)
        {
            Guid responseId = new Guid();
            try
            {
                switch (which)
                {
                    case "Skill":
                        Skill skill = JsonConvert.DeserializeObject<Skill>(jsonObject);
                        skill.Id = responseId = Guid.NewGuid();
                        ctx.Skills.Add(skill);
                        break;
                    case "Education":
                        Education education = JsonConvert.DeserializeObject<Education>(jsonObject);
                        education.Id = responseId = Guid.NewGuid();
                        ctx.Educations.Add(education);
                        break;
                    case "Experience":
                        Experience experience = JsonConvert.DeserializeObject<Experience>(jsonObject);
                        experience.Id = responseId = Guid.NewGuid();
                        ctx.Experiences.Add(experience);
                        break;
                    case "Project":
                        Project project = JsonConvert.DeserializeObject<Project>(jsonObject);
                        project.Id = responseId = Guid.NewGuid();
                        ctx.Projects.Add(project);
                        break;
                }
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { hasError = true, message = e.Message });
            }
            return Json(new { Id = responseId.ToString() },JsonRequestBehavior.AllowGet); // we return the new items id.
        }
        public JsonResult UserInformationDelete(string which, string id)
        {
            try
            {
                switch (which)
                {
                    case "Skill":
                        Skill skill = ctx.Skills.SingleOrDefault(x => x.Id == new Guid(id));
                        if(skill != null)
                            ctx.Skills.Remove(skill);
                        break;
                    case "Education":
                        Education education = ctx.Educations.SingleOrDefault(x => x.Id == new Guid(id));
                        if (education != null)
                            ctx.Educations.Remove(education);
                        break;
                    case "Experience":
                        Experience experience = ctx.Experiences.SingleOrDefault(x => x.Id == new Guid(id));
                        if (experience != null)
                            ctx.Experiences.Remove(experience);
                        break;
                    case "Project":
                        Project project = ctx.Projects.SingleOrDefault(x => x.Id == new Guid(id));
                        if (project != null)
                            ctx.Projects.Remove(project);
                        break;
                }
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { hasError = true, message = e.Message });
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        //Blog-JS-Service
        public JsonResult TagAdd(string tagName, HttpPostedFileBase tagPic)
        {
            try
            {
                if(tagName == String.Empty)
                    throw new Exception("Tag Name Must Filled");

                if (ctx.Tags.Where(x => x.Name.ToLower() == tagName.ToLower()).Count() > 0)
                {
                        throw new Exception("There is already a Tag with that name.");
                }
                Tag tag = new Tag();
                tag.Id = Guid.NewGuid();
                tag.Name = tagName;
                tag.SeoUrl = MySeoUrl.getUrl(tag.Name, false);
                if (tagPic != null)
                {
                    tag.PicturePath = MyImageManager.addImage(tagPic, MyImageType.Blog, tag.SeoUrl);
                }
                ctx.Tags.Add(tag);
                ctx.SaveChanges();
                return Json(new { Id = tag.Id.ToString() , Name = tag.Name, PicturePath = tag.PicturePath }, JsonRequestBehavior.AllowGet); // we'll return the new tag's id.
            }
            catch (Exception e)
            {
                return Json(new { hasError = true, message = e.Message });
            }
        }
        public JsonResult TagDelete(string tagId)
        {
            try
            {
                Tag tag = ctx.Tags.SingleOrDefault(x => x.Id == new Guid(tagId));
                if (tag == null)
                {
                    throw new Exception("Tag Seçilemedi.");
                }
                if (tag.BlogTags.Count > 0)
                {
                    throw new Exception("Silinmeye Çalışılan Tag'a Bağlı Bloglar Mevcut. İlk Onlar Silinmeli.");
                }
                MyImageManager.deleteImage(tag.PicturePath, MyImageType.Blog);
                ctx.Tags.Remove(tag);
                ctx.SaveChanges();
                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { hasError = true, message = e.Message });
            }
        }
        public JsonResult TagEdit(string tagId, string tagNewName , HttpPostedFileBase tagNewPic)
        {
            try
            {
                Tag tag = ctx.Tags.SingleOrDefault(x => x.Id == new Guid(tagId));
                if(tagNewName != null || tagNewName.Trim() != "") // boşsa ismi değiştirme
                    tag.Name = tagNewName;
                tag.SeoUrl = MySeoUrl.getUrl(tag.Name, false);
                if (tagNewPic != null)
                {
                    if (tag.PicturePath != String.Empty)
                        MyImageManager.deleteImage(tag.PicturePath, MyImageType.Blog);
                    tag.PicturePath = MyImageManager.addImage(tagNewPic, MyImageType.Blog, tag.SeoUrl);
                }
                ctx.SaveChanges();
                return Json(new { Name = tag.Name, PicturePath = tag.PicturePath },JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { hasError = true, message = e.Message });
            }
        }
        public JsonResult BlogAdd(HttpPostedFileBase titlePicture, string selectedPicTagId, string title, string summary, string blogContentEncrypt, string selectedTags)
        {
            try
            {
                if(selectedTags == null || selectedTags.Trim() == "")
                    throw new Exception("En Az Bir Tag Seçilmeli.");
                string blogContent = Encoding.UTF8.GetString(Convert.FromBase64String(blogContentEncrypt));
                Blog blog = new Blog();
                blog.Id = Guid.NewGuid();
                blog.Title = title;
                blog.Summary = summary;
                blog.Views = 0;
                blog.CreateDate = DateTime.Now;
                blog.SeoUrl = MySeoUrl.getUrl(title,true);
                if (ctx.Blogs.Where(x => x.SeoUrl == blog.SeoUrl).Count() > 0)
                {
                    throw new Exception("Aynı Başlıkta Blog Bulunmaktadır. Tekrar Deneyiniz.");
                }
                blog.Content = blogContent;
                blog.BlogTags = new List<BlogTag>();
                foreach (string item in selectedTags.Split(','))
                {
                    Tag tag = ctx.Tags.SingleOrDefault(x => x.Id == new Guid(item));
                    if (tag != null)
                    {
                        BlogTag blogTag = new BlogTag() { Blog = blog, Tag = tag, Id = Guid.NewGuid() };
                        ctx.BlogTags.Add(blogTag);
                        blog.BlogTags.Add(blogTag);
                    }
                    else
                    {
                        throw new Exception("Seçilen Bir Tag Bulunamamıştır.");
                    }
                }
                //resim al, yoksa tag resmi seçildiyse onu al yoksa hata ver
                if (titlePicture != null)
                {
                    blog.TitlePicturePath = MyImageManager.addImage(titlePicture, MyImageType.Blog, blog.SeoUrl);
                    blog.UsePicOfThisTag = null;
                }
                else if (selectedPicTagId != String.Empty)
                {
                    blog.TitlePicturePath = "";
                    blog.UsePicOfThisTag = ctx.Tags.FirstOrDefault(x => x.Id == new Guid(selectedPicTagId));
                }
                else
                {
                    throw new Exception("Blog İçin Bir Resim Yüklemeli Veya Bir Tag Resmi Seçilmelidir.");
                }
                ctx.Blogs.Add(blog);
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { hasError = true, message = e.Message });
            }
            return Json(JsonRequestBehavior.DenyGet);
        }
        public JsonResult BlogEdit(HttpPostedFileBase newTitlePicture, string selectedPicTagId, string id,string title, string summary, string blogContentEncrypt, string selectedTags)
        {
            try
            {
                string blogContent = Encoding.UTF8.GetString(Convert.FromBase64String(blogContentEncrypt));
                Blog blog = ctx.Blogs.SingleOrDefault(x => x.Id == new Guid(id));


                if (blog.UsePicOfThisTag == null) // tag resmi değilse 
                {
                    if (newTitlePicture != null) // yeni resim varsa eskisini sil
                    {
                        MyImageManager.deleteImage(blog.TitlePicturePath, MyImageType.Blog);
                    }
                    else if (selectedPicTagId != String.Empty) // tag resmi seçildiyse eskisini sil
                    {
                        MyImageManager.deleteImage(blog.TitlePicturePath, MyImageType.Blog);
                    }
                }

                blog.Title = title;
                blog.Summary = summary;
                blog.SeoUrl = MySeoUrl.getUrl(title,true);
                blog.Content = blogContent;
                blog.BlogTags = new List<BlogTag>();
                foreach (BlogTag item in blog.BlogTags.ToList())
                {
                    ctx.BlogTags.Remove(item);
                }
                foreach (string item in selectedTags.Split(','))
                {
                    Tag tag = ctx.Tags.SingleOrDefault(x => x.Id == new Guid(item));
                    if (tag != null)
                    {
                        BlogTag blogTag = new BlogTag() { Blog = blog, Tag = tag, Id = Guid.NewGuid() };
                        ctx.BlogTags.Add(blogTag);
                        blog.BlogTags.Add(blogTag);
                    }
                    else
                    {
                        throw new Exception("Seçilen Bir Tag Bulunamamıştır.");
                    }
                }
                if (newTitlePicture != null)
                {
                    blog.TitlePicturePath = MyImageManager.addImage(newTitlePicture, MyImageType.Blog, blog.SeoUrl);
                    blog.UsePicOfThisTag = null;
                }
                else if (selectedPicTagId != String.Empty)
                {
                    blog.TitlePicturePath = "";
                    blog.UsePicOfThisTag = ctx.Tags.FirstOrDefault(x => x.Id == new Guid(selectedPicTagId));
                }
                ctx.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { hasError = true, message = e.Message });
            }
            return Json(new { isRedirect = true , redirectUrl = Url.Action("AllBlogs", "Admin") });
        }
        public JsonResult BlogDelete(string blogId)
        {
            try
            {
                Blog blog = ctx.Blogs.SingleOrDefault(x => x.Id == new Guid(blogId));
                if (blog == null)
                {
                    throw new Exception("Blog Bulunamadı.");
                }
                foreach (BlogTag item in blog.BlogTags.ToList())
                {
                    ctx.BlogTags.Remove(item);
                }
                foreach (Comment item in blog.Comments.ToList())
                {
                    ctx.Comments.Remove(item);
                }

                if (blog.UsePicOfThisTag == null) // kullanılan resim tag resmi değilse sil
                    MyImageManager.deleteImage(blog.TitlePicturePath, MyImageType.Blog);

                MyImageManager.blogContentImagesDelete(blog.Content);
                ctx.Blogs.Remove(blog);
                ctx.SaveChanges();
                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { hasError = true, message = e.Message });
            }
        }

        public JsonResult CommentEdit(string commentId, string newPersonName, string newContent)
        {
            try
            {
                Comment comment = ctx.Comments.SingleOrDefault(x => x.Id == new Guid(commentId));
                if (newPersonName != null || newPersonName.Trim() != "")
                {
                    comment.PersonName = newPersonName;
                }
                if (newContent != null || newContent.Trim() != "")
                {
                    comment.Content = newContent;
                }
                ctx.SaveChanges();

                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { hasError = true, message = e.Message });
            }
        }
        public JsonResult CommentVerifery(string commentId, bool decision)
        {
            try
            {
                Comment comment = ctx.Comments.SingleOrDefault(x => x.Id == new Guid(commentId));
                comment.Verifery = decision;
                ctx.SaveChanges();
                if (decision)
                    sendInformationMailToFollowersForNewReplyWhenAccept(HttpContext.Cache,comment.Blog,comment); // yorum açıldıysa ilgili kişilere mail at
                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { hasError = true, message = e.Message });
            }
        }
        public JsonResult CommentDelete(string commentId)
        {
            try
            {
                Comment comment = ctx.Comments.SingleOrDefault(x => x.Id == new Guid(commentId));
                if (comment == null)
                    throw new Exception("Yorum Bulunamadı.");
                ctx.Comments.Remove(comment);
                ctx.SaveChanges();

                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { hasError = true, message = e.Message });
            }
        }
        public JsonResult DeleteLogs(string which) {
            string response = "No Action.";
            DateTime dt = DateTime.Now.AddMonths(-1);
            switch (which) {
                case "info":
                    List<Log> logs = ctx.Logs.Where(x => DateTime.Compare(x.ExecutionTime, dt) < 0 ).ToList();
                    int countLog = logs.Count;
                    ctx.Logs.RemoveRange(logs);
                    ctx.SaveChanges();
                    response = countLog + " Logs Deleted.";
                    break;
                case "exception":
                    List<ExceptionLog> exLogs = ctx.ExceptionLogs.Where(x => DateTime.Compare(x.ExceptionTime, dt) < 0).ToList();
                    int countExLog = exLogs.Count;
                    ctx.ExceptionLogs.RemoveRange(exLogs);
                    ctx.SaveChanges();
                    response = countExLog + " ExceptionLogs Deleted.";
                    break;

            }
            return Json(response);
        }

        //AutoMail
        public JsonResult MailTest(){
            try
            {
                MyMailManager.testMail();
                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { hasError = true, message = e.Message });
            }
        }

        // other
        public static void sendInformationMailToFollowersForNewReplyWhenAccept(System.Web.Caching.Cache cache , Blog blog,Comment whosComment)
        {
            Context ctx = new Context();
            //fist check , is admin open auto mail ?
            User user = ctx.Users.FirstOrDefault();
            if (!user.IsAllowAutoMail)
                return;

            List<string> hasSendMails = new List<string>(); // mail atılanlar
            List<Comment> comments = blog.Comments.Where(x=>x.Blog == blog).ToList();
            foreach (Comment comment in comments)
            {
                if (comment.PersonMail == whosComment.PersonMail) // cevabı onay görülen kişiye mail atılmaz, kendi cevabını başkasının cevabı gibi mail atmayız.
                    continue;
                if (!hasSendMails.Contains(comment.PersonMail))
                {
                    if (comment.IsAllowFollowing && comment.Verifery)
                    {
                        MyMailManager.reportNewReplyToFollowerUser(cache, comment);
                        hasSendMails.Add(comment.PersonMail);
                    }
                }
            }
        }
    }
}