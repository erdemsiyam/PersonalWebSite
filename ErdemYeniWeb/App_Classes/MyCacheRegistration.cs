using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace ErdemYeniWeb.App_Classes
{
    /*
        Kişi bir Blog'a baktığında O Blog'un View sayısı artırılır. Ama kötüye kullanıl engellenmesi için kullanıcının ip'si Cache'e kaydedilir 5 dakika süresince bakma işlemi View sayısına etki ettirilmez.
        Kişi bir Blog'a yorum yaptığında 5dakikalığında Cache'e ip'si kaydedilir bu sürece tekrar yorum yapması engellenir.

         */
    public class MyCacheRegistration
    {
        /*
             View ve Comment için aynı Cache'leme yapısı kullanılmıştır. tipleri Aşağıdaki oluşturulan Enum Class ile belirtilir.
             istemci adresi - blog numarası - İşlem tipi : şeklinde Cache kayıdı yapılmaktadır
             eylemin daha önce yapılıp yapılmadığı kontrolü veya eğlem yapıldı isteğini tek fonksiyon ile halletmekteyiz.
             Örnek : yeni bir eylem oldu müşteri yorum yaptı, kontrol et böyle bir eylemin aynısı var mı yoksa kaydet ve true gönder, eğer aynı eylem varsa false gönder.
             */
        public static bool checkAndAddRecord(Cache cache, string clientIp, string blogId, RegistrationType registrationType)
        {

            string registration = (string)cache[clientIp + "_" + blogId + "_" + registrationType];
            if (registration != null)
                return false;
            cache.Add(
                    key: clientIp + "_" + blogId + "_" + registrationType,
                    value: "1",
                    dependencies: null,
                    absoluteExpiration: System.Web.Caching.Cache.NoAbsoluteExpiration,
                    slidingExpiration: new TimeSpan(0, 5, 0), // 5dk 
                    priority: System.Web.Caching.CacheItemPriority.Low,
                    onRemoveCallback: null
            );
            return true;

        }
    }

    public enum RegistrationType
    {
        Comment, View
    }
}