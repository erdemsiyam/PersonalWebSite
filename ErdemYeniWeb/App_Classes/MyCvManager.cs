using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.App_Classes
{
    /* CV'yi server'a kaydetme. */
    public class MyCvManager
    {
        public static void cvUpload(HttpPostedFileBase cvFile,string language)
        {
            if (language != "en" && language != "tr")
            {
                throw new MyException("Dil TR veya EN olmalıdır.");
            }
            if (cvFile.ContentType != "application/pdf")
            {
                throw new MyException("Dosya PDF olmalıdır.");
            }
            if (cvFile.ContentLength > 10000000)
            {
                throw new MyException("Dosya 10MB üzeri olamaz.");
            }
            string fileName = "erdemsiyam_cv_"+language;
            string fullPath = HttpContext.Current.Server.MapPath("~/Cv/"+ fileName + ".pdf");
            if (System.IO.File.Exists(fullPath)) // if there exists old one, delete.
            {
                System.IO.File.Delete(fullPath); // here deleting.
            }
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Cv/"), fileName + Path.GetExtension(cvFile.FileName));
            cvFile.SaveAs(path);
        }
    }
}