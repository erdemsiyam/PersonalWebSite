using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        private static Context context { get; set; }
        protected static Context ctx {
            get {
                return (context != null) ? context : context = new Context();
            }
        }
    }
}