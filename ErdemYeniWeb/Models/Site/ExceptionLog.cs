using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.Models.Site
{
    [Table("ExceptionLog")]
    public class ExceptionLog : BaseModel
    {
        public string Content { get; set; }
        public string ClassPath { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ExceptionTime { get; set; }

        public ExceptionLog(){}
        public ExceptionLog(string content, string classPath, DateTime exceptionTime){
            Content = content;
            ClassPath = classPath;
            ExceptionTime = exceptionTime;
            Id = Guid.NewGuid();
            ctx.ExceptionLogs.Add(this);
            ctx.SaveChanges();
        }
    }
}