using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.ViewModels
{
    public class AdminIndexModel
    {
        public int IndexClickCount { get; set; }
        public int BlogClickCount { get; set; }
        public int BlogEntryClickCount { get; set; }
        public int CvClickCount { get; set; }
    }
}