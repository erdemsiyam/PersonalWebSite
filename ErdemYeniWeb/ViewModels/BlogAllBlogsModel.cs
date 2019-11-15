using ErdemYeniWeb.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.ViewModels
{
    public class BlogAllBlogsModel : MyPagingAlgorithm
    {
        public List<Blog> blogs { get; set; }

        public BlogMenuModel menuModel { get; set; }

        public string tag { get; set; } // searching tag
        public string search { get; set; } // searching word
    }
}