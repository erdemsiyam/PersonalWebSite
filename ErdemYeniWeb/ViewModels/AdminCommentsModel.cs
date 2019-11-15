using ErdemYeniWeb.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.ViewModels
{
    public class AdminCommentsModel : MyPagingAlgorithm
    {
        public List<Comment> comments { get; set; }
    }
}