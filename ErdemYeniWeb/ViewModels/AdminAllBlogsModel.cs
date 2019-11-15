using ErdemYeniWeb.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.ViewModels
{
    public class AdminAllBlogsModel : MyPagingAlgorithm
    {
        public List<Blog> blogs { get; set; }
    }
}