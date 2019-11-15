using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ErdemYeniWeb.ViewModels;
using ErdemYeniWeb.Models.Blog;

namespace ErdemYeniWeb.ViewModels
{
    public class BlogEntryModel
    {
        public Blog blog { get; set; }
        public BlogMenuModel menuModel { get; set; }
        public Blog afterBlog { get; set; }
        public Blog beforeBlog{ get; set; }
    }
}