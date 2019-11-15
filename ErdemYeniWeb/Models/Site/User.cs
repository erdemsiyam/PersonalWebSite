using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.Models.Site
{
    [Table("User")]
    public class User : BaseModel
    {
        public string Nickname { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Degree { get; set; }
        public string About1 { get; set; }
        public string About2 { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Address { get; set; }
        [DataType(DataType.Url)]
        public string Facebook { get; set; }
        [DataType(DataType.Url)]
        public string Twitter { get; set; }
        [DataType(DataType.Url)]
        public string Instagram { get; set; }
        [DataType(DataType.Url)]
        public string Linkedin { get; set; }
        [DataType(DataType.Url)]
        public string Github { get; set; }

        public string BigPicturePath { get; set; }
        public string SmallPicturePath { get; set; }
        public string BlogSaying { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastConnectTime { get; set; }
        public bool IsAllowAutoMail { get; set; }
        public string AutoMail { get; set; }
        public string AutoMailPass { get; set; }
        public string AutoMailHost { get; set; }
        public string AutoMailPort { get; set; }
        public bool IsAutoMailSslEnable { get; set; }


        public static int getNewLogsCountSinceLastLogin() {
            return ctx.Logs.Where(x => x.ExecutionTime >= ctx.Users.FirstOrDefault().LastConnectTime).Count();
        }
        public static int getNewCommentCountSinceLastLogin() {
            return ctx.Comments.Where(x => x.CreateDate >= ctx.Users.FirstOrDefault().LastConnectTime).Count();
        }
        public static string getNewExceptionLogsCountSinceLastLogin() {
            return ctx.ExceptionLogs.Where(x => x.ExceptionTime >= ctx.Users.FirstOrDefault().LastConnectTime).Count().ToString();
        }

        public static string getUserCurrentBlogPicture()
        {
            Context ctx = new Context();
            User user = ctx.Users.FirstOrDefault();
            return user.SmallPicturePath;
        }
    }
}