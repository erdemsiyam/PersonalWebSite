using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.Models.Blog
{
    [Table("Tag")]
    public class Tag : BaseModel
    {
        public string Name { get; set; }
        public string SeoUrl { get; set; }
        public string PicturePath { get; set; }

        public virtual List<BlogTag> BlogTags { get; set; }
    }
}