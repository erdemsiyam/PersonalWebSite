using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.Models.Blog
{
    [Table("BlogTag")]
    public class BlogTag : BaseModel
    {

        public virtual Tag Tag { get; set; }
        public virtual Blog Blog { get; set; }

    }
}