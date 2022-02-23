using marble.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace marble.ViewModels
{
    public class DesignsPageModel
    {
    public List<Design> Designs { set; get; }
    public List<Category> designCategories { set; get; }
    public List<Design_Gallery> designGallery{ set; get; }
        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] files { get; set; }

    }

 
}