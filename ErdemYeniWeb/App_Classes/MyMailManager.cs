using ErdemYeniWeb.Models;
using ErdemYeniWeb.Models.Blog;
using ErdemYeniWeb.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Caching;

namespace ErdemYeniWeb.App_Classes
{
    /*
        Otomatik Mail atma için oluşturulan Class.
         */
    public class MyMailManager
    {
        private static string MAIL;
        private static string PASS;
        private static string HOST;
        private static int PORT;
        private static bool ENABLE_SSL; // true idi

        private static bool sendMail(string targetMail, string content, string subject)
        {
            try
            {
                getInformationFromAdmin();

                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(targetMail));
                message.From = new MailAddress(MAIL);
                message.Subject = subject;
                message.Body = content;
                message.IsBodyHtml = true;
                NetworkCredential credential = new NetworkCredential()
                {
                    UserName = MAIL,
                    Password = PASS
                };
                SmtpClient smtp = new SmtpClient()
                {
                    Credentials = credential,
                    Host = HOST,
                    Port = PORT,
                    EnableSsl = ENABLE_SSL
                };
                smtp.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //blog'a yeni gelen mesaj admin tarafından onaylanırsa o Blog'a başka cevap atmış ve abone olmuş kişilere bildiri mail'i gider.
        public static bool reportNewReplyToFollowerUser(Cache cache,Comment comment) {

            //fist check , is admin open auto mail ?
            User user = new Context().Users.FirstOrDefault();
            if (!user.IsAllowAutoMail)
                return false;

            Blog blog = comment.Blog;
            string targetMail, content, subject, domainName;

            domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority); // site adresi.
            targetMail = comment.PersonMail;


            Guid oneBlogCancelCode = MyCacheBlogFollowingCancel.createBlogFollowCancelCacheAndGetTicket(cache, comment,true);
            Guid allBlogsCancelCode = MyCacheBlogFollowingCancel.createBlogFollowCancelCacheAndGetTicket(cache, comment,false);

            subject = "Yeni Cevap : \"" + blog.Title+"\" - Erdem Siyam BLOG";
            content = "Yorum yaptığınız konuya cevap gelmiştir...( Yorum Tarihiniz : " + comment.CreateDate.ToString() + ")</br>";
            content += "<a href='"+ domainName + "/blog/entry/" + blog.SeoUrl + "' target='_blank'>BURAYA</a> Tıklayarak Cevabı Görebilirsiniz.</br></br>";
            content += "Bu Konu Aboneliğinden Çıkmak İçin <a href='"+ domainName + "/abonelikiptal/" + oneBlogCancelCode + "' target='_blank'>BURAYA</a> Tıklayınız (7 gün sonra bağlantı deaktif olacaktır)</br>";
            content += "Tüm Konu Aboneliklerinden Çıkmak İçin <a href='"+ domainName + "/abonelikiptal/" + allBlogsCancelCode + "' target='_blank'>BURAYA</a> Tıklayınız (7 gün sonra bağlantı deaktif olacaktır)";

            return sendMail(targetMail, content, subject);
        }
        public static void testMail(){
            sendMail("erdemsiyam@gmail.com", "içerik", "Deneme");
        }
        private static void getInformationFromAdmin(){
            Context ctx = new Context();
            User user = ctx.Users.FirstOrDefault();
            MAIL = user.AutoMail;
            PASS = user.AutoMailPass;
            HOST = user.AutoMailHost;
            PORT = Convert.ToInt16(user.AutoMailPort);
            ENABLE_SSL = user.IsAutoMailSslEnable;
        }
    }
}
