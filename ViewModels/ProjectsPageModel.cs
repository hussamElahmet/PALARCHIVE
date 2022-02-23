using marble.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace marble.ViewModels
{
    public class ProjectsPageModel
    {
    public List<Project> projects { set; get; }
    public List<Category> projectCategories { set; get; }
    public List<Project_Gallery> projectGallery{ set; get; }
        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] files { get; set; }

    }


}