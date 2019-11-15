using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ErdemYeniWeb.Models.Site
{
    [Table("Project")]
    public class Project : BaseModel
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        [DataType(DataType.Date)]
        public DateTime AddDate { get; set; }
        [DataType(DataType.Url)]
        public string GithubLink { get; set; }
    }
}