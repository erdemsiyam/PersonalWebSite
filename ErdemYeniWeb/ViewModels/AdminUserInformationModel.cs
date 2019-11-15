using ErdemYeniWeb.Models.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.ViewModels
{
    public class AdminUserInformationModel
    {
        public List<Skill> skills { get; set; }
        public List<Education> educations { get; set; }
        public List<Experience> experiences { get; set; }
        public List<Project> projects { get; set; }
    }
}