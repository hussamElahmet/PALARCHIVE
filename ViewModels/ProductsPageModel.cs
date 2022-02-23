using marble.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace marble.ViewModels
{
    public class ProductsPageModel
    {
//test
    public List<Product> products { set; get; }
    public List<Category> productCategories { set; get; }
    public List<Product_Gallery> productGallery{ set; get; }
        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] files { get; set; }
        
    }

    public class paramAdd
    {
        public string productName { set; get; }
         public string productNameEn { set; get; }
       public string productDesc { set; get; }
        public string productDescEn { set; get; }
        public int categoryNo { set; get; }
        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] files { get; set; }

    }
}