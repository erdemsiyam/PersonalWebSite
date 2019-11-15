using ErdemYeniWeb.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.ViewModels
{
    public class AdminExceptionLogsModel : MyPagingAlgorithm
    {
        public List<ExceptionLog> logs { get; set; }
    }
}