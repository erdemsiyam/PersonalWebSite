using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ErdemYeniWeb.Models.Site;
using ErdemYeniWeb.Models.Blog;

namespace ErdemYeniWeb.ViewModels
{
    public class BlogMenuModel
    {
        public User user { get; set; }
        public List<Blog> populerBlogs { get; set; }
        public List<Tag> allTags { get; set; }
    }
}