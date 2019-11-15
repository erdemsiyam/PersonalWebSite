using ErdemYeniWeb.Models.Site;
using ErdemYeniWeb.Models.Blog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.Models
{
    public class Context : DbContext
    {
        //Site
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        //Blog
        public DbSet<Blog.Blog> Blogs { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public Context():base("name=RemoteDB")
        {
            Database.SetInitializer(new CreateDB());
        }
    }

    public class CreateDB : CreateDatabaseIfNotExists<Context>
    {
        protected override void Seed(Context context)
        {
            User user = new User();
            user.Id = Guid.NewGuid();
            user.Nickname = "erdem";
            user.Password = "123";
            user.Name = "Muhammed Erdem Siyam";
            user.Degree = "Junior Dev.";
            user.About1 = "simple1";
            user.About2 = "simple2";
            user.BirthDate = new DateTime(1996, 08, 26);
            user.Phone = "551-132-75-35";
            user.Email = "erdemsiyam@gmail.com";
            user.Address = "Düzce/Akçakoca";
            user.Facebook = "https://www.facebook.com/MSiyam81";
            user.Twitter = "https://twitter.com/erdem_siyam";
            user.Instagram = "https://www.instagram.com/erdemsiyam";
            user.Linkedin = "https://www.linkedin.com/in/erdemsiyam/";
            user.Github = "https://github.com/erdemsiyam";
            user.BigPicturePath = "";
            user.SmallPicturePath = "";
            user.BlogSaying = "simple3";
            user.IsAllowAutoMail = false;
            user.AutoMail = "";
            user.AutoMailPass = "";
            user.AutoMailHost = "";
            user.AutoMailPort = "";
            user.IsAutoMailSslEnable = true;
            user.LastConnectTime = DateTime.Now;

            context.Users.Add(user);
            
            context.Skills.Add(new Skill() { Id = Guid.NewGuid(), Name = "Spring Boot", Percent = 80 });
            context.Skills.Add(new Skill() { Id = Guid.NewGuid(), Name = "Android", Percent = 80 });
            context.Skills.Add(new Skill() { Id = Guid.NewGuid(), Name = ".Net MVC / API", Percent = 80 });
            context.Skills.Add(new Skill() { Id = Guid.NewGuid(), Name = "Angular", Percent = 70 });
            context.Skills.Add(new Skill() { Id = Guid.NewGuid(), Name = "Python", Percent = 70 });
            context.Skills.Add(new Skill() { Id = Guid.NewGuid(), Name = "Linux", Percent = 60 });

            context.Educations.Add(new Education() { Id = Guid.NewGuid(), Degree = "Math" , SchoolName = "Barbaros Anadolu Highschool" , Location = "Düzce", StartDate = new DateTime(2009, 09, 15) , EndDate = new DateTime(2014, 06, 15) });
            context.Educations.Add(new Education() { Id = Guid.NewGuid(), Degree = "Computer Engineering" , SchoolName = "Düzce University" , Location = "Düzce", StartDate = new DateTime(2015, 09, 15) , EndDate = new DateTime(2019, 06, 15) });

            context.Experiences.Add(new Experience() { Id = Guid.NewGuid(), Degree = "Computer Engineering", CompanyName = "Albaraka Türk Bank - Headquarters", Location = "İstanbul", StartDate = new DateTime(2018, 08, 06), EndDate = new DateTime(2019, 09, 15) });

            context.Projects.Add(new Project() { Id = Guid.NewGuid(), Title = "My Web Site", Summary = "My own site.", AddDate = DateTime.Now, GithubLink = "" });

            context.Tags.Add(new Tag() { Id = Guid.NewGuid(), Name = "C#" , SeoUrl = "csharp"});
            context.Tags.Add(new Tag() { Id = Guid.NewGuid(), Name = "MVC", SeoUrl = "mvc" });
            context.Tags.Add(new Tag() { Id = Guid.NewGuid(), Name = "Java", SeoUrl = "java" });
            context.Tags.Add(new Tag() { Id = Guid.NewGuid(), Name = "SpringBoot", SeoUrl = "springboot" });
            context.Tags.Add(new Tag() { Id = Guid.NewGuid(), Name = "Android", SeoUrl = "android" });
            context.Tags.Add(new Tag() { Id = Guid.NewGuid(), Name = "Python", SeoUrl = "python" });
            context.Tags.Add(new Tag() { Id = Guid.NewGuid(), Name = "Angular", SeoUrl = "angular" });
            context.Tags.Add(new Tag() { Id = Guid.NewGuid(), Name = "Linux", SeoUrl = "linux" });

            context.SaveChanges();

        }
    }
}