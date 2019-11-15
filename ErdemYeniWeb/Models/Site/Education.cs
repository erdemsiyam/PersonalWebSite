using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ErdemYeniWeb.Models.Site
{
    [Table("Education")]
    public class Education : BaseModel
    {
        public string SchoolName { get; set; }
        public string Degree { get; set; }
        public string Location { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}