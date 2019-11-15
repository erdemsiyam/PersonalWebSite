using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.Models.Blog
{
    [Table("Blog")]
    public class Blog : BaseModel
    {
        public string SeoUrl { get; set; }
        public string Title { get; set; }
        public string TitlePicturePath { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        public int Views { get; set; }

        public virtual Tag UsePicOfThisTag { get; set; }
        public virtual List<BlogTag> BlogTags { get; set; }
        public virtual List<Comment> Comments { get; set; }


        public static Dictionary<string, float> getBlogsCountPerMonth(){
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;
            Dictionary<string, float> counts = new Dictionary<string, float>();
            for (int i = 1; i <= 12; i++){
                DateTime startOfMonth, endOfMonth;
                if (month <= 0)
                {
                    startOfMonth = new DateTime(year, 12 + month, 1);
                    endOfMonth = new DateTime(year, 12 + month, DateTime.DaysInMonth(year, 12 + month));
                }
                else
                {
                    startOfMonth = new DateTime(year, month, 1);
                    endOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                }
                float f = ctx.Blogs.Where(x => x.CreateDate >= startOfMonth && x.CreateDate <= endOfMonth).Count();
                counts.Add(startOfMonth.ToString("MMM"),f*2 );
                if (--month == 0) {
                    year--;
                }
            }
            counts.Reverse();
            return counts;
        }
    }
}