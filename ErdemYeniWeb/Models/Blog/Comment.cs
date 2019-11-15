using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.Models.Blog
{
    [Table("Comment")]
    public class Comment : BaseModel
    {
        public string PersonName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string PersonMail { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
        public string Content { get; set; }
        public bool Verifery { get; set; }
        public bool IsAllowFollowing { get; set; }
        public bool IsAdmin { get; set; }

        public virtual Blog Blog { get; set; }
    }
}