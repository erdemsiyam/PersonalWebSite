using ErdemYeniWeb.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace ErdemYeniWeb.App_Classes
{
    /*
     Blog'a yorum yapan kişi blog'a abonelik tikini tıklarsa, bloga her cevap döndüğünde ona mail gider. 
     Giden mailde de aboneliği kapatmak isterse diye mail içinde https://www.erdemsiyam.com/abonelikiptal/{...id...} şeklinde bir kapatma bileti gönderilir.
     Bu id değeri cache'te 7 gün saklanır. Aşağıdaki Class bu cache'leme işlemini yapar.
         */
    public class MyCacheBlogFollowingCancel
    {
        public Guid commentId { get; set; }//yorumun id'si
        public bool isOnlyOneBlogCancel { get; set; } // sadece bir blog için mi abonelik kesilecek.
        private MyCacheBlogFollowingCancel() { } // private yapıcı sayesinde sadece kontrolümüz altında nesne oluşturulabilir. (Aşağıda)

        public static Guid createBlogFollowCancelCacheAndGetTicket(Cache cache, Comment comment, bool isOnlyOneBlogCancel){ // oluştur ve Cache'e aç
            MyCacheBlogFollowingCancel cacheObject = new MyCacheBlogFollowingCancel() { commentId = comment.Id, isOnlyOneBlogCancel = isOnlyOneBlogCancel };
            Guid specificId = Guid.NewGuid();
            //adding to cache
            cache.Add(
                    key: specificId.ToString(),
                    value: cacheObject,
                    dependencies: null,
                    absoluteExpiration: System.Web.Caching.Cache.NoAbsoluteExpiration,
                    slidingExpiration: new TimeSpan(168,0,0), // 7 gün 
                    priority: System.Web.Caching.CacheItemPriority.Normal,
                    onRemoveCallback: null
            );
            return specificId;
        }
        public static MyCacheBlogFollowingCancel resolveTheCacheTicket(Cache cache, string ticket) { // kişi aboneliği iptal etmek istediyse Cache'ten veri alınıp işlem yapılacak yere geri gönderilir.
            return (MyCacheBlogFollowingCancel)cache[ticket];
        }

    }
}