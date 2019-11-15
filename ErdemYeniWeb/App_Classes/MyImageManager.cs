using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
namespace ErdemYeniWeb.App_Classes
{
    /* Server'a resim ekleme ve silme. */
    public class MyImageManager
    {
        public static string addImage(HttpPostedFileBase image,MyImageType imageType)
        {
            string serverPath = "C:\\Inetpub\\vhosts\\erdemsiyam.net\\httpdocs\\";
            string path = Path.GetExtension(image.FileName);

            if (!(path == ".jpg" || path == ".jpeg" || path == ".png"))
            {
                throw new MyException("Resim Uzantısı .jpg , .jpeg yada .png olmalıdır.");
            }

            if (image.ContentLength > 100000000)
            {
                throw new MyException("Resim Boyutu 10MB'den fazla olamaz.");
            }
            string imageName = Guid.NewGuid().ToString() + path;
            using (Bitmap res = new Bitmap(Image.FromStream(image.InputStream)))
            {
                if (imageType == MyImageType.User)
                    res.Save(serverPath+ "Images\\User\\"+imageName);//HttpContext.Current.Server.MapPath("/Images/User/" + imageName)
                else if (imageType == MyImageType.Blog)
                    res.Save(serverPath+ "Images\\Blog\\"+imageName); //HttpContext.Current.Server.MapPath("/Images/Blog/" + imageName)
            }
            return imageName;
        }

        public static string addImage(HttpPostedFileBase image, MyImageType imageType, string name)
        {
            string serverPath = "C:\\Inetpub\\vhosts\\erdemsiyam.net\\httpdocs\\";
            string path = Path.GetExtension(image.FileName);
            if (!(path == ".jpg" || path == ".png" || path == ".jpeg"))
            {
                throw new MyException("Resim Uzantısı .jpg , .jpeg yada .png olmalıdır.");
            }
            if (image.ContentLength > 100000000)
            {
                throw new MyException("Resim Boyutu 10MB'den fazla olamaz.");
            }
            string imageName = name + path;
            using (Bitmap res = new Bitmap(Image.FromStream(image.InputStream)))
            {
                if (imageType == MyImageType.User)
                    res.Save(serverPath + "Images\\User\\" + imageName); // HttpContext.Current.Server.MapPath("/Images/User/" + imageName)
                else if (imageType == MyImageType.Blog)
                    res.Save(serverPath + "Images\\Blog\\" + imageName); // HttpContext.Current.Server.MapPath("/Images/Blog/" + imageName)
            }
            return imageName;
        }

        public static string deleteImage(string imageName, MyImageType imageType)
        {
            string path = String.Empty;
            switch (imageType)
            {
                case MyImageType.User:
                    path = HttpContext.Current.Server.MapPath("/Images/User/" + imageName);
                    break;
                case MyImageType.Blog:
                    path = HttpContext.Current.Server.MapPath("/Images/Blog/" + imageName);
                    break;
                case MyImageType.BlogContent:
                    path = HttpContext.Current.Server.MapPath("/Images/Blog/ContentImages/" + imageName);
                    break;
                default:
                    new MyException("Resim Tipi Belirtilmemiş.");
                    break;
            }

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            else
            {
                new MyException("Silmeye Çalışılan Resim Bulunamadı.");
            }
            return "Deleted";
        }

        //özel : blog silinirse, içindeki resimleri de sileriz.
        public static void blogContentImagesDelete(string content)
        {
            List<string> images = new List<string>();
            string copy1 = content;
            while (copy1.IndexOf("src=\"") != -1)
            {
                copy1 = copy1.Substring(copy1.IndexOf("src=\"") + 32);
                images.Add(copy1.Substring(0, copy1.IndexOf("\" style")));
            }
            foreach (string item in images)
            {
                deleteImage(item, MyImageType.BlogContent);
            }
        }
    }
    public enum MyImageType
    {
        User,Blog,BlogContent
    }

}