using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ErdemYeniWeb.Models.Site;
namespace ErdemYeniWeb.ViewModels
{
    public class AdminLogsModel : MyPagingAlgorithm
    {
        public List<Log> logs { get; set; }
    }
}