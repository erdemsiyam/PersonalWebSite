using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.App_Classes
{
    /*
        Mantıksal Hatalar İçin Oluşturulan Exception
        ExceptionFilter'de yakalanan hata eğer bu hata ise ExceptionLog'a kaydedilmez. Orada Sadece Sistem Hataları Kaydedilir.
         */
    public class MyException : Exception 
    {
        public MyException(string errorMessage) : base(errorMessage){}
    }
}