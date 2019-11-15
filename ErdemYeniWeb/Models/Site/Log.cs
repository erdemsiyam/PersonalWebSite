using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.Models.Site
{
    [Table("Log")]
    public class Log : BaseModel
    {
        public string ClientIP { get; set; }
        public string Direction { get; set; }
        public DateTime ExecutionTime { get; set; }
    }
}