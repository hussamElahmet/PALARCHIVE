using marble.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace marble.ViewModels
{
    public class CategoryProductPageModel
    {
    public List<Product> products { set; get; }
    public List<Category> productCategories { set; get; }
    public List<Product_Gallery> productGallery{ set; get; }
        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] files { get; set; }
        
    }

    public class paramCategoryAdd
    {
        public string CategoryName { set; get; }
        public string CategoryNameEn { set; get; }
        public  int? Order { set; get; }
        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] files { get; set; }

    }
}