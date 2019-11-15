using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ErdemYeniWeb.Models.Site
{

    [Table("Skill")]
    public class Skill : BaseModel
    {
        public string Name { get; set; }
        public int Percent { get; set; }
    }
}