using ErdemYeniWeb.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.ViewModels
{
    public class AdminEditBlogModel
    {
        public List<Tag> tags { get; set; }
        public List<Tag> selectedTags { get; set; }
        public Blog blog { get; set; }
    }
}